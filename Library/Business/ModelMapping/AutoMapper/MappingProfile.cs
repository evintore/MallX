using AutoMapper;
using Core.Configuration;
using Core.Entities;
using Entities.Concrete;
using Entities.Dto;

namespace Business.ModelMapping.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserViewDto>().ReverseMap();
            CreateMap<MallInfo, MallInfoDto>().ReverseMap();
            CreateMap<Store, StoreDto>()
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.Brand.BrandName))
                .ForMember(x => x.MallInfoName, y => y.MapFrom(z => z.MallInfo.MallName));
            CreateMap<StoreDto, Store>();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Snapshot, SnapshotDto>()
                .ForMember(x => x.StoreName, y => y.MapFrom(z => z.Store.StoreName))
                .ForMember(x => x.MallInfoId, y => y.MapFrom(z => z.Store.MallInfoId))
                .ForMember(x => x.MallInfoName, y => y.MapFrom(z => z.Store.MallInfo.MallName));
            CreateMap<SnapshotDto, Snapshot>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.SubcategoryCount, y => y.MapFrom(z => z.Subcategories.Count));
            CreateMap<SubcategoryDto, Subcategory>().ReverseMap();
            CreateMap<AddressConfiguration, GetDropdownDto>();
            CreateMap<CityConfiguration, GetDropdownDto>();
            CreateMap<TownConfiguration, GetDropdownDto>();
        }
    }
}
