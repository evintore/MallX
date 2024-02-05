namespace Core.Configuration
{
    public class AddressConfiguration
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public List<CityConfiguration> Cities { get; set; }
    }
}
