namespace ExampleProject
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Npgsql;

    public static class MigrationExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetSection("DatabaseSettings:ConnectionString").Value;
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating postgresql database.");

                    using var connection = new NpgsqlConnection(connectionString);

                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Province";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Province(
                                            	ID uuid PRIMARY KEY NOT NULL,
                                            	Name VARCHAR(500) NOT NULL,
                                            	ZipCode VARCHAR(50));";
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('9a628222-5ddc-481d-be10-d0bb8a2be972', 'กรุงเทพมหานคร', '10000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('1c6755fa-f3c4-4f1f-8280-a5bdf84f7a2d', 'สมุทรปราการ', '10270');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('fb0b5aaa-589f-43e6-b127-8e28d24293c3', 'นนทบุรี', '11000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('7cc7a349-e11a-453d-9586-fc7f178bd977', 'ปทุมธานี', '12000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('2cb90c64-5f54-462a-bffe-d0d66db54849', 'พระนครศรีอยุธยา', '13000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('51b44a71-dc6f-4319-879e-4e1fae88cbbd', 'อ่างทอง', '14000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('431f2a6c-209d-4b43-bc37-4d4bcb7182e4', 'ลพบุรี', '15000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('fae2aaee-6f2d-469b-a27e-f15e926c9dcd', 'สิงห์บุรี', '16000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('94b9c69e-8c75-4a91-9d3a-e113451e7b8f', 'ชัยนาท', '17000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('eeb357f0-c9cf-4b72-838a-8bff48ce5b08', 'สระบุรี', '18000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('526cddce-6289-46ad-b4e4-5d28ff96ad12', 'ชลบุรี', '20000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('4d075e3b-7e3b-4df7-b6b9-8d79a46bf53c', 'ระยอง', '21150');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('74c58bfa-e6c7-4381-a3da-4921e14d2660', 'จันทบุรี', '22000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('0035e330-eb11-4881-9524-1082a9328167', 'ตราด', '23000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('d7fcb615-e047-4521-b1ce-8fa725e017bd', 'ฉะเชิงเทรา', '24000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('928e42ce-f01e-4d7e-8f4d-91e45b410ca6', 'ปราจีนบุรี', '25230');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('d8916e84-de1f-429f-9ebc-b127bc73a1cc', 'นครนายก', '26000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('8c1633dd-04f2-45c9-88f0-4628b8961b9f', 'สระแก้ว', '27000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('e7c139ec-e618-45cf-ac84-755821cb98c0', 'นครราชสีมา', '30000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('46e1ddbd-822e-4c76-a05e-16d079d1d330', 'บุรีรัมย์', '31000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('0b4dbc11-aa65-4860-84f1-a124c3146fde', 'สุรินทร์', '32000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('80002f88-f02d-4215-9f30-501b9ec96592', 'ศรีสะเกษ', '33000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('3eb5130e-0332-41b2-b2f8-b4145f6da26f', 'อุบลราชธานี', '34000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('10ef6608-b5c9-434a-b055-daeb7a397bab', 'ยโสธร', '35000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('d823b384-c6ce-4136-8c09-a6ce7ab8fa06', 'ชัยภูมิ', '36000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('8b2a8d36-54db-47f2-9d10-d9885bc092c3', 'อำนาจเจริญ', '37000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('aa2ad8e7-d45d-4882-9b2b-fc6a8393b3b2', 'บึงกาฬ', '38000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('0a5b4fec-e9d9-4ff4-9444-f38ece83d092', 'หนองบัวลำภู', '39000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('9be6e7be-a36c-4189-94e6-626bf13871a1', 'ขอนแก่น', '40000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('bd18f391-de9f-4633-a271-be24bef8ac03', 'อุดรธานี', '41000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('3e16b676-e8be-48b9-96a6-fa8315796b8a', 'เลย', '42000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('63deae73-53ec-4a2b-aef2-baf709d98506', 'หนองคาย', '43000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('831f2993-ab56-4ec5-851e-8f55e22a1a22', 'มหาสารคาม', '44000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('3c3f5319-b7c7-4ac4-8885-c294dcc8fb45', 'ร้อยเอ็ด', '45000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('fb6c2f67-59d8-467f-bbed-fbc2e2f1937e', 'กาฬสินธุ์', '46000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('20bd5ae3-5374-47a1-8829-877f03248461', 'สกลนคร', '47000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('cee0c528-bbc0-48c7-bb21-7365b58e28b1', 'นครพนม', '48000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('8afebc35-8b4a-42cd-a41a-58ad9a549158', 'มุกดาหาร', '49000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('e1ec37ed-41a8-41d3-a0bb-46454a39ae25', 'เชียงใหม่', '50300');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('96ced79e-95d8-4668-9276-4c06afe0a287', 'ลำพูน', '51000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('91dd216f-4b64-4ca2-b6a9-7e4997f4443a', 'ลำปาง', '52000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('30e790c8-c42f-4f32-b931-85f1c6b221f1', 'อุตรดิตถ์', '53000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('90cc264c-0f56-48ae-abf0-75eef2bd11a6', 'แพร่', '54000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('87bd23ab-8761-4b6d-b134-3db325158594', 'น่าน', '55000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('9bdfab34-8258-45df-8967-0e6494897393', 'พะเยา', '56000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('29b19a37-4e29-44bb-9736-335d725108a0', 'เชียงราย', '57100');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('a1b60b19-2220-4510-bc1d-c0c61f44fd7c', 'แม่ฮ่องสอน', '58000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('f42095db-2a64-4ac7-9928-5a8bf7af19df', 'นครสวรรค์', '60000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('3290570a-b041-46b6-a28d-5d746fdbfc36', 'อุทัยธานี', '61000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('b3a64f69-9044-404b-b3a7-bbcd324dd8e9', 'กำแพงเพชร', '62000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('c34ce6cf-a6b3-42f5-98b5-08f2297a3ffe', 'ตาก', '63000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('c372663b-a9a9-430d-9b15-205d44446818', 'สุโขทัย', '64000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('dc54c5cc-bffb-42b8-a743-d84af621b01a', 'พิษณุโลก', '65000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('1b84596d-aca2-4a02-a892-8ae684b25dff', 'พิจิตร', '66000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('fe054c9a-c0af-46c9-8b8a-19d0089decf6', 'เพชรบูรณ์', '67000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('7a4fab0d-a866-42eb-bd3b-bbc56a5c5d1d', 'ราชบุรี', '70000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('b5524825-9e54-42ae-b7b2-fa90db075d79', 'กาญจนบุรี', '71000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('a3924778-92e4-423c-979f-0898677489f7', 'สุพรรณบุรี', '72000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('463b4c84-1210-4316-ac8c-2a401220f6cf', 'นครปฐม', '73000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('befb8c49-b3ae-4b02-a6a9-84fb8186506f', 'สมุทรสาคร', '74000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('b9536f82-5102-4bf1-bbaa-305252668e27', 'สมุทรสงคราม', '75000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('1cfe739b-e3e0-4e15-b92d-e20b52de7959', 'เพชรบุรี', '76000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('b3757373-cb9a-4640-bd88-4ba275f858a0', 'ประจวบคีรีขันธ์', '77000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('d22b2290-ca46-4243-bd4f-2318c39fd5b9', 'นครศรีธรรมราช', '80000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('8cfffb4c-9272-4071-83c0-83264cd0b00f', 'กระบี่', '81000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('197ae641-d863-45e9-820e-66cd96bb05dd', 'พังงา', '82000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('796eb394-0889-4199-bbe2-d03d53df2f9e', 'ภูเก็ต', '83000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('34ea4a83-b6fa-4c73-9711-a245545a4c26', 'สุราษฎร์ธานี', '84000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('a7ba8a5b-6770-4cf3-9376-ec31e9635a21', 'ระนอง', '85000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('14f726d9-0197-4e6b-aa6f-81d961e14cdd', 'ชุมพร', '86000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('d9af4f19-9de6-4d70-a28e-0f58528ac72d', 'สงขลา', '90000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('a5eee757-a053-44f7-9830-9b13f712ca40', 'สตูล', '91000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('f032921c-9ae8-4b2f-8396-de7ae8f9bfa1', 'ตรัง', '92000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('eae97219-c43d-470f-92dc-ed57e9a9ac37', 'พัทลุง', '93000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('57dda824-4b5c-44b0-ac84-b1e92830205e', 'ปัตตานี', '94000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('f516b356-a5f4-48c5-abc6-65352d12ccf6', 'ยะลา', '95000');
                        INSERT INTO Province (Id, Name, ZipCode) VALUES ('6395b894-6c71-4605-8073-f58fa2b0d741', 'นราธิวาส', '96000');
                    ";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postgresql database.");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postgresql database.");
                }

                return host;
            }
        }
    }
}
