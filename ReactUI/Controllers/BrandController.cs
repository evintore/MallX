using Business.Abstract;
using Core.Dto;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using ReactUI.Controllers.Base;

namespace ReactUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : CustomBaseController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBrand(int id, bool? isActive = true)
        {
            var result = await _brandService.GetBrandByIdAsync(id, isActive);

            return ActionResultInstance(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands([FromQuery] PageQueryDto parameter)
        {
            var result = await _brandService.GetBrandsAsync(parameter);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandDto brandDto)
        {
            var result = await _brandService.CreateBrandAsync(brandDto);

            return ActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(BrandDto brandDto)
        {
            var result = await _brandService.UpdateBrandAsync(brandDto);

            return ActionResultInstance(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var result = await _brandService.DeleteBrandAsync(id);

            return ActionResultInstance(result);
        }
    }
}