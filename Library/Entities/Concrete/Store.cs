using Core.Entities.Base;

namespace Entities.Concrete
{
    public class Store : BaseEntity, IEntity
    {
        public string? StoreName { get; set; }

        public int BrandId { get; set; }

        public int Floor { get; set; }

        public int MallInfoId { get; set; }

        public virtual MallInfo MallInfo { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<Snapshot>? Snapshots { get; set; }

    }
}
