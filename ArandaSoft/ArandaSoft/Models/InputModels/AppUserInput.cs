using System.ComponentModel.DataAnnotations;

namespace ArandaSoft.Model.InputModels
{
    public class AppUserInput
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

        [Required]
        public int Age { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}