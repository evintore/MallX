using Business.Abstract;
using Core.Dto;
using Microsoft.AspNetCore.Mvc;
using ReactUI.Controllers.Base;

namespace ReactUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : CustomBaseController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("countries")]
        public IActionResult GetCountries([FromQuery] PageQueryDto parameter)
        {
            var result = _addressService.GetCountries(parameter);

            return ActionResultInstance(result);
        }

        [HttpGet("countryCode")]
        public IActionResult GetCountryName(string countryCode)
        {
            var result = _addressService.GetCountryName(countryCode);

            return ActionResultInstance(result);
        }

        [HttpGet("cities")]
        public IActionResult GetCities(string countryCode, [FromQuery] PageQueryDto parameter)
        {
            var result = _addressService.GetCities(countryCode, parameter);

            return ActionResultInstance(result);
        }

        [HttpGet("cityCode")]
        public IActionResult GetCityName(string cityCode)
        {
            var result = _addressService.GetCityName(cityCode);

            return ActionResultInstance(result);
        }

        [HttpGet("towns")]
        public IActionResult GetTowns(string cityCode, [FromQuery] PageQueryDto parameter)
        {
            var result = _addressService.GetTowns(cityCode, parameter);

            return ActionResultInstance(result);
        }

        [HttpGet("townCode")]
        public IActionResult GetTownName(string townCode)
        {
            var result = _addressService.GetTownName(townCode);

            return ActionResultInstance(result);
        }
    }
}
