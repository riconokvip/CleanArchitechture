namespace CleanArchitechture.Application.DbContexts.SQLServers
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Model base
            base.OnModelCreating(builder);

            // Build entities
            builder.Entity<UserEntities>().HasIndex(u => u.Id);
            builder.Entity<PermissionEntities>().HasIndex(u => u.Id);
            builder.Entity<UserTokenEntities>().HasIndex(u => u.Id);
            builder.Entity<UserPermissionEntities>().HasIndex(u => u.Id);

            builder.Entity<FileEntities>().HasIndex(u => u.Id);
        }

        public virtual DbSet<UserEntities> Users { get; set; }
        public virtual DbSet<PermissionEntities> Permissions { get; set; }
        public virtual DbSet<UserTokenEntities> UserTokens { get; set; }
        public virtual DbSet<UserPermissionEntities> UserPermissions { get; set; }

        public virtual DbSet<FileEntities> Files { get; set; }
    }
}
