using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArandaSoft.Model.OutputModels
{
    public class AppUserOutput
    {
        public int Id { get; set; }
        [Display(Name = "Nombre Usuario")]
        public string Name { get; set; }
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Display(Name = "Nombre completo")]
        public string FullName { get; set; }
        [Display(Name = "Correo")]
        public string EmailAddress { get; set; }
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Edad")]
        public int Age { get; set; }
        public int RoleId { get; set; }
        [Display(Name = "Rol")]
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }
    }
}