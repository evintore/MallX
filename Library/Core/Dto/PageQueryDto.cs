namespace Core.Dto
{
    public class PageQueryDto
    {
        public bool IsActive { get; set; } = true;

        public int PageId { get; set; } = 1;

        public int PageSize { get; set; } = 20;
        public string OrderBy { get; set; } = string.Empty;
        public string SearchKey { get; set; } = string.Empty;
    }
}
