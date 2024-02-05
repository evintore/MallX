using Business.Abstract;
using Core.Dto;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using ReactUI.Controllers.Base;

namespace ReactUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoryController : CustomBaseController
    {
        private readonly ISubcategoryService _subcategoryService;

        public SubcategoryController(ISubcategoryService subcategoryService)
        {
            _subcategoryService = subcategoryService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSubcategory(int id, bool? isActive = true)
        {
            var result = await _subcategoryService.GetSubcategoryByIdAsync(id, isActive);

            return ActionResultInstance(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubcategories([FromQuery] SubcategoryQueryDto parameter)
        {
            var result = await _subcategoryService.GetSubcategoriesAsync(parameter);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubcategory(SubcategoryDto subcategoryDto)
        {
            var result = await _subcategoryService.CreateSubcategoryAsync(subcategoryDto);

            return ActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubcategory(SubcategoryDto subcategoryDto)
        {
            var result = await _subcategoryService.UpdateSubcategoryAsync(subcategoryDto);

            return ActionResultInstance(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletesubcategory(int id)
        {
            var result = await _subcategoryService.DeleteSubcategoryAsync(id);

            return ActionResultInstance(result);
        }
    }
}