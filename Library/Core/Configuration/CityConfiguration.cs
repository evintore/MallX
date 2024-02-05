namespace Core.Configuration
{
    public class CityConfiguration
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public List<TownConfiguration> Towns { get; set; }
    }
}
