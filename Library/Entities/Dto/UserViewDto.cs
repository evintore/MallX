namespace Entities.Dto
{
    public class UserViewDto
    {
        public int PkId { get; set; }

        public string? FullName { get; set; }

        public string? Mail { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
