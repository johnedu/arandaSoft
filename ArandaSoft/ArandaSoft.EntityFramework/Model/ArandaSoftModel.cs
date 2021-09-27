namespace ArandaSoft.EntityFramework.Model
{
    using System.Data.Entity;

    public partial class ArandaSoftModel : DbContext
    {
        public ArandaSoftModel()
            : base("name=ArandaSoftModel")
        {
        }

        public virtual DbSet<AppPermission> AppPermission { get; set; }
        public virtual DbSet<AppRole> AppRole { get; set; }
        public virtual DbSet<AppRolePermission> AppRolePermission { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppPermission>()
                .HasMany(e => e.AppRolePermission)
                .WithRequired(e => e.AppPermission)
                .HasForeignKey(e => e.PermissionId);

            modelBuilder.Entity<AppRole>()
                .HasMany(e => e.AppRolePermission)
                .WithRequired(e => e.AppRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AppRole>()
                .HasMany(e => e.AppUser)
                .WithRequired(e => e.AppRole)
                .HasForeignKey(e => e.RoleId);
        }
    }
}
