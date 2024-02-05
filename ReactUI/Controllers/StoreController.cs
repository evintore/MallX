using Business.Abstract;
using Core.Dto;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactUI.Controllers.Base;

namespace ReactUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : CustomBaseController
    {

        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMallInfo(int id, bool? isActive = true)
        {
            var result = await _storeService.GetStoreByIdAsync(id,isActive);

            return ActionResultInstance(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetStores([FromQuery] PageQueryDto parameter)
        {          
            var result = await _storeService.GetStoresAsync(parameter);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStore(StoreDto mallInfoDto)
        {
            var result = await _storeService.CreateStoreAsync(mallInfoDto);

            return ActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStore(StoreDto mallInfoDto)
        {
            var result = await _storeService.UpdateStoreAsync(mallInfoDto);

            return ActionResultInstance(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var result = await _storeService.DeleteStoreAsync(id);

            return ActionResultInstance(result);
        }
    }
}