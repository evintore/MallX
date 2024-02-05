using Business.Abstract;
using Business.Helpers;
using Business.ModelMapping.AutoMapper;
using Core.Configuration;
using Core.Dto;
using Core.Results;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace Business.Concrete
{
    public class AddressManager : IAddressService
    {
        private readonly List<AddressConfiguration> _addresses;

        public AddressManager(IOptions<List<AddressConfiguration>> addresses)
        {
            _addresses = addresses.Value;
        }

        public Response<List<GetDropdownDto>> GetCountries(PageQueryDto parameter)
        {
            var countriesQuery = _addresses.AsQueryable();

            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                countriesQuery = countriesQuery.Where(x => x.Name.Contains(parameter.SearchKey));
            }

            if (string.IsNullOrEmpty(parameter.OrderBy))
            {
                countriesQuery = countriesQuery.OrderBy(x => x.Name);
            }
            else
            {
                countriesQuery = countriesQuery.OrderBy(parameter.OrderBy);
            }
            var countriesQueryLast = countriesQuery.Select(x => ObjectMapper.Mapper.Map<GetDropdownDto>(x));

            var countryList = PagedList<GetDropdownDto>.ToPagedList(countriesQueryLast.AsNoTracking(), parameter.PageId, parameter.PageSize);

            return countryList is not null
                ? Response<List<GetDropdownDto>>.Success(countryList, (int)HttpStatusCode.OK, parameter.PageId, countryList.TotalCount)
                : Response<List<GetDropdownDto>>.Fail("Ülke bulunamadı", (int)HttpStatusCode.NotFound, true);
        }

        public Response<string> GetCountryName(string countryCode)
        {
            var country = _addresses.FirstOrDefault(country => country.Code == countryCode);

            return country is not null
                ? Response<string>.Success(country.Name, (int)HttpStatusCode.OK)
                : Response<string>.Fail("Ülke bulunamadı", (int)HttpStatusCode.NotFound, true);
        }

        public Response<List<GetDropdownDto>> GetCities(string countryCode, PageQueryDto parameter)
        {
            var citiesQuery = _addresses.AsQueryable().Where(country => country.Code == countryCode).SelectMany(x => x.Cities);

            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                citiesQuery = citiesQuery.Where(x => x.Name.Contains(parameter.SearchKey));
            }

            if (string.IsNullOrEmpty(parameter.OrderBy))
            {
                citiesQuery = citiesQuery.OrderBy(x => x.Name);
            }
            else
            {
                citiesQuery = citiesQuery.OrderBy(parameter.OrderBy);
            }

            var citiesQueryLast = citiesQuery.Select(city => ObjectMapper.Mapper.Map<GetDropdownDto>(city));

            var cityList = PagedList<GetDropdownDto>.ToPagedList(citiesQueryLast.AsNoTracking(), parameter.PageId, parameter.PageSize);

            return cityList is not null
                ? Response<List<GetDropdownDto>>.Success(cityList, (int)HttpStatusCode.OK, cityList.CurrentPage, cityList.TotalCount)
                : Response<List<GetDropdownDto>>.Fail("Şehir bulunamadı", (int)HttpStatusCode.NotFound, true);
        }

        public Response<string> GetCityName(string cityCode)
        {
            var city = _addresses.SelectMany(county => county.Cities).Where(city => city.Code == cityCode).FirstOrDefault();

            return city is not null
                ? Response<string>.Success(city.Name, (int)HttpStatusCode.OK)
                : Response<string>.Fail("Şehir bulunamadı", (int)HttpStatusCode.NotFound, true);
        }

        public Response<List<GetDropdownDto>> GetTowns(string cityCode, PageQueryDto parameter)
        {
            var townsQuery = _addresses.AsQueryable().SelectMany(country => country.Cities).Where(city => city.Code == cityCode).SelectMany(x => x.Towns);

            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                townsQuery = townsQuery.Where(x => x.Name.Contains(parameter.SearchKey));
            }

            if (string.IsNullOrEmpty(parameter.OrderBy))
            {
                townsQuery = townsQuery.OrderBy(x => x.Name);
            }
            else
            {
                townsQuery = townsQuery.OrderBy(parameter.OrderBy);
            }

            var townsQueryLast = townsQuery.Select(town => ObjectMapper.Mapper.Map<GetDropdownDto>(town));

            var townList = PagedList<GetDropdownDto>.ToPagedList(townsQueryLast.AsNoTracking(), parameter.PageId, parameter.PageSize);

            return townList is not null
                ? Response<List<GetDropdownDto>>.Success(townList, (int)HttpStatusCode.OK, townList.CurrentPage, townList.TotalCount)
                : Response<List<GetDropdownDto>>.Fail("İlçe bulunamadı", (int)HttpStatusCode.NotFound, true);
        }

        public Response<string> GetTownName(string townCode)
        {
            var town = _addresses.SelectMany(country => country.Cities).SelectMany(city => city.Towns).Where(town => town.Code == townCode).FirstOrDefault();

            return town is not null
                ? Response<string>.Success(town.Name, (int)HttpStatusCode.OK)
                : Response<string>.Fail("İlçe bulunamadı", (int)HttpStatusCode.NotFound, true);
        }
    }
}
