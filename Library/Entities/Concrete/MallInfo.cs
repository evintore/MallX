using Core.Entities.Base;

namespace Entities.Concrete
{
    public class MallInfo : BaseEntity, IEntity
    {
        public string MallName { get; set; }

        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public string CityCode { get; set; }
        public string CityName { get; set; }

        public string TownCode { get; set; }
        public string TownName { get; set; }

        public int LeasableArea { get; set; }
        
        public int VehicleCapacity { get; set; }

        public ICollection<Store>? Stores { get; set; }
    }
}
