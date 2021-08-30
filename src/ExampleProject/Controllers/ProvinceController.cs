namespace ExampleProject.Controllers
{
    using ExampleProject.Entities;
    using ExampleProject.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceRepository provinceRepository;
        private readonly ILogger<ProvinceController> logger;

        public ProvinceController(
            IProvinceRepository provinceRepository,
            ILogger<ProvinceController> logger)
        {
            this.provinceRepository = provinceRepository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvinces(CancellationToken cancellationToken = default)
        {
            var provinces = await provinceRepository.GetProvincesAsync(cancellationToken);

            return this.Ok(provinces);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvinceById(Guid id, CancellationToken cancellationToken = default)
        {
            var province = await provinceRepository.GetProvinceByIdAsync(id, cancellationToken);

            if (province is null)
            {
                return this.NotFound();
            }

            return this.Ok(province);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvince([FromBody] Province province, CancellationToken cancellationToken = default)
        {
            return this.Ok(await provinceRepository.CreateProvinceAsync(province, cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProvince([FromBody] Province province, CancellationToken cancellationToken = default)
        {
            return this.Ok(await provinceRepository.UpdateProvinceAsync(province, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvinceById(Guid id, CancellationToken cancellationToken = default)
        {
            return this.Ok(await provinceRepository.DeleteProvinceByIdAsync(id, cancellationToken));
        }
    }
}
