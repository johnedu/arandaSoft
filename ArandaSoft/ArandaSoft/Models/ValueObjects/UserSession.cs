using System.Collections.Generic;

namespace ArandaSoft.Model.ValueObjects
{
    public class UserSession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }
    }
}