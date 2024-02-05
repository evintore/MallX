using Core.Dto;
using Core.Results;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IAddressService
    {
        Response<List<GetDropdownDto>> GetCountries(PageQueryDto parameter);

        Response<string> GetCountryName(string countryCode);

        Response<List<GetDropdownDto>> GetCities(string countryName, PageQueryDto parameter);

        Response<string> GetCityName(string cityCode);

        Response<List<GetDropdownDto>> GetTowns(string cityName, PageQueryDto parameter);

        Response<string> GetTownName(string townCode);
    }
}
