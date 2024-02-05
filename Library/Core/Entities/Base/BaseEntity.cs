namespace Core.Entities.Base
{
    public partial class BaseEntity
    {
        public int PkId { get; set; }

        public int CreatedUserId { get; set; }

        public User? CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserId { get; set; }

        public User? ModifiedUser { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
