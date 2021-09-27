namespace ArandaSoft.EntityFramework.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AppRolePermission")]
    public partial class AppRolePermission
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        public virtual AppPermission AppPermission { get; set; }

        public virtual AppRole AppRole { get; set; }
    }
}
