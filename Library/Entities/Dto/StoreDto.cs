﻿using Core.Dto;
using Entities.Concrete;

namespace Entities.Dto
{
    public class StoreDto : BaseDto
    {
        public string StoreName { get; set; }

        public int BrandId { get; set; }

        public string? BrandName { get; set; }

        public int Floor { get; set; }

        public int MallInfoId { get; set; }

        public string? MallInfoName { get; set; }

        public virtual MallInfo? MallInfo { get; set; }

        public virtual Brand? Brand { get; set; }

        public virtual ICollection<Snapshot>? Snapshots { get; set; }
    }
}
