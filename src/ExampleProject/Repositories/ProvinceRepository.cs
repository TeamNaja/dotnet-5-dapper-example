namespace ExampleProject.Repositories
{
    using Dapper;
    using ExampleProject.Entities;
    using Microsoft.Extensions.Configuration;
    using Npgsql;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class ProvinceRepository : IProvinceRepository
    {
        private readonly string connectionString;
        public ProvinceRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetSection("DatabaseSettings:ConnectionString").Value;
        }

        public async Task<IEnumerable<Province>> GetProvincesAsync(CancellationToken cancellationToken = default)
        {
            using var connection = new NpgsqlConnection(this.connectionString);

            var provinces = await connection.QueryAsync<Province>("SELECT * FROM Province");

            return provinces;
        }

        public async Task<Province> GetProvinceByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using var connection = new NpgsqlConnection(this.connectionString);

            var province = await connection.QuerySingleOrDefaultAsync<Province>("SELECT * FROM Province WHERE Id = @Id", new { Id = id });

            return province;
        }

        public async Task<bool> CreateProvinceAsync(Province province, CancellationToken cancellationToken = default)
        {
            using var connection = new NpgsqlConnection(this.connectionString);

            var affected = await connection.ExecuteAsync(
                "INSERT INTO Province (Id, Name, ZipCode) VALUES (@Id, @Name, @ZipCode)",
                new { Id = Guid.NewGuid(), Name = province.Name, ZipCode = province.ZipCode });

            return affected > 0;
        }

        public async Task<bool> UpdateProvinceAsync(Province province, CancellationToken cancellationToken = default)
        {
            using var connection = new NpgsqlConnection(this.connectionString);

            var affected = await connection.ExecuteAsync(
                "UPDATE Province SET Name = @Name, ZipCode = @ZipCode WHERE Id = @Id",
                new { Name = province.Name, ZipCode = province.ZipCode, Id = province.Id });

            return affected > 0;
        }
        public async Task<bool> DeleteProvinceByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using var connection = new NpgsqlConnection(this.connectionString);

            var affected = await connection.ExecuteAsync(
                "DELETE FROM Province WHERE Id = @Id",
                new { Id = id });

            return affected > 0;
        }
    }
}
