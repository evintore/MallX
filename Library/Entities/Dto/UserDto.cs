using Core.Dto;
using Core.Enums;

namespace Entities.Dto
{
    public class UserDto
    {
        public int PkId { get; set; }

        public string? FullName { get; set; }

        public string? Mail { get; set; }

        public string? Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsActive { get; set; }

        public UserStatus Status { get; set; }
    }
}
