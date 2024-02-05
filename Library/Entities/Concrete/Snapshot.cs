using Core.Entities.Base;

namespace Entities.Concrete
{
    public class Snapshot : BaseEntity, IEntity
    {
        public int StoreId { get; set; }

        public Store Store { get; set; }

        public DateTime SnapshotDate { get; set; }

        public int CustomerCount { get; set; }

        public int CustomerInSalesCount { get; set; }

        public int WorkerCount { get; set; }
    }
}
