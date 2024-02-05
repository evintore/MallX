using Core.Dto;
using Entities.Concrete;

namespace Entities.Dto
{
    public class SnapshotDto : BaseDto
    {
        public string? StoreName { get; set; }

        public int StoreId { get; set; }

        public Store? Store { get; set; }

        public int MallInfoId { get; set; }

        public string? MallInfoName { get; set; }

        public DateTime SnapshotDate { get; set; }

        public int CustomerCount { get; set; }

        public int CustomerInSalesCount { get; set; }

        public int WorkerCount { get; set; }
    }
}
