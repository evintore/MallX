using Core.Dto;
using Entities.Concrete;

namespace Entities.Dto
{
    public class SubcategoryDto: BaseDto
    {
        public int SubategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public virtual Category? Category { get; set; }
        public ICollection<Brand>? Brands { get; set; }
    }
}
