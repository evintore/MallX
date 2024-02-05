using Core.Dto;

namespace Entities.Dto
{
    public class SubcategoryQueryDto : PageQueryDto
    {
        public int CategoryId { get; set; }
    }
}
