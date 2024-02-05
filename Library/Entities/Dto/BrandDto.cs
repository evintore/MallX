using Core.Dto;
using Entities.Concrete;

namespace Entities.Dto
{
    public class BrandDto : BaseDto
    {
        public string BrandName { get; set; }

        public virtual Category? Category { get; set; }
        public int CategoryId { get; set; }

        public virtual Subcategory? SubCategory { get; set; }
        public int SubCategoryId { get; set; }

        public ICollection<Store>? Stores { get; set; }
    }
}
