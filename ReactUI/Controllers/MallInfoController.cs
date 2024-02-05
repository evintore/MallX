using Business.Abstract;
using Core.Dto;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactUI.Controllers.Base;

namespace ReactUI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MallInfoController : CustomBaseController
    {
        private readonly IMallInfoService _mallInfoService;

        public MallInfoController(IMallInfoService mallInfoService)
        {
            _mallInfoService = mallInfoService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMallInfo(int id, bool? isActive = true)
        {
            var result = await _mallInfoService.GetMallInfoByIdAsync(id, isActive);

            return ActionResultInstance(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMallInfos([FromQuery] PageQueryDto parameter)
        {
            var result = await _mallInfoService.GetMallInfosAsync(parameter);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddMallInfo(MallInfoDto mallInfoDto)
        {
            var result = await _mallInfoService.CreateMallInfoAsync(mallInfoDto);

            return ActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMallInfo(MallInfoDto mallInfoDto)
        {
            var result = await _mallInfoService.UpdateMallInfoAsync(mallInfoDto);

            return ActionResultInstance(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMallInfo(int id)
        {
            var result = await _mallInfoService.DeleteMallInfoAsync(id);

            return ActionResultInstance(result);
        }
    }
}
