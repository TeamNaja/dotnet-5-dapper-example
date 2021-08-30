namespace ExampleProject.Repositories
{
    using ExampleProject.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IProvinceRepository
    {
        Task<IEnumerable<Province>> GetProvincesAsync(CancellationToken cancellationToken = default);

        Task<Province> GetProvinceByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> CreateProvinceAsync(Province province, CancellationToken cancellationToken = default);

        Task<bool> UpdateProvinceAsync(Province province, CancellationToken cancellationToken = default);

        Task<bool> DeleteProvinceByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
