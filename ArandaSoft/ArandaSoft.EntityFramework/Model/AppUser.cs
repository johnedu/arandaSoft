namespace ArandaSoft.EntityFramework.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AppUser")]
    public partial class AppUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        [Required]
        [StringLength(128)]
        public string FullName { get; set; }

        [Required]
        [StringLength(256)]
        public string EmailAddress { get; set; }

        [StringLength(512)]
        public string Address { get; set; }

        [StringLength(32)]
        public string PhoneNumber { get; set; }

        public int Age { get; set; }

        public int RoleId { get; set; }

        public virtual AppRole AppRole { get; set; }
    }
}
