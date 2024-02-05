using Business.Abstract;
using Core.Dto;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using ReactUI.Controllers.Base;

namespace ReactUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory(int id, bool? isActive = true)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id, isActive);

            return ActionResultInstance(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] PageQueryDto parameter)
        {
            var result = await _categoryService.GetCategoriesAsync(parameter);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            var result = await _categoryService.CreateCategoryAsync(categoryDto);

            return ActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto)
        {
            var result = await _categoryService.UpdateCategoryAsync(categoryDto);

            return ActionResultInstance(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            return ActionResultInstance(result);
        }
    }
}