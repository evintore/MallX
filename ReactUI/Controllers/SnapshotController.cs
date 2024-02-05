using Business.Abstract;
using Core.Dto;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using ReactUI.Controllers.Base;

namespace ReactUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnapshotController : CustomBaseController
    {
        private readonly ISnapshotService _snapshotService;

        public SnapshotController(ISnapshotService snapshotService)
        {
            _snapshotService = snapshotService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSnapshot(int id, bool? isActive = true)
        {
            var result = await _snapshotService.GetSnapshotByIdAsync(id, isActive);

            return ActionResultInstance(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSnapshots([FromQuery] PageQueryDto parameter)
        {
            var result = await _snapshotService.GetSnapshotsAsync(parameter);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddSnapshot(SnapshotDto snapshotDto)
        {
            var result = await _snapshotService.CreateSnapshotAsync(snapshotDto);

            return ActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSnapshot(SnapshotDto snapshotDto)
        {
            var result = await _snapshotService.UpdateSnapshotAsync(snapshotDto);

            return ActionResultInstance(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSnapshot(int id)
        {
            var result = await _snapshotService.DeleteSnapshotAsync(id);

            return ActionResultInstance(result);
        }
    }
}