using Core.Dto;
using Entities.Concrete;

namespace Entities.Dto
{
    public class CategoryDto : BaseDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubcategoryCount { get; set; }
        public ICollection<Subcategory>? Subcategories { get; set; }
        public ICollection<Brand>? Brands { get; set; }
    }
}
