using System.Collections.Generic;

namespace ArandaSoft.Core.Model.ValueObjects
{
    public class AppUserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }
    }
}