using AutoMapper;

namespace Business.ModelMapping.AutoMapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
