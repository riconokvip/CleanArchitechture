namespace CleanArchitechture.Application.DbContexts
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;

        public DbInitializer(ApplicationDbContext applicationDb)
        {
            _context = applicationDb;
        }

        public void Seed()
        {
            if (!_context.Users.Any())
            {
                var seedUser = Seeder();
                _context.Users.Add(seedUser.Item1);
                _context.Permissions.AddRange(seedUser.Item2);
                _context.UserPermissions.Add(seedUser.Item3);
                _context.SaveChanges();
            }
        }

        private (UserEntities, List<PermissionEntities>, UserPermissionEntities) Seeder()
        {
            UserEntities user = new UserEntities
            {
                Id = ApplicationExtensions.TIME_NOW.GenerateUserId("u"),
                DisplayName = "Super Admin",
                FullName = "Quản trị viên cấp cao",
                UserName = $"application{DateTime.UtcNow.Ticks}".ToLower(),
                Email = "superadmin@application.com",
                SecurityStamp = new Guid().ToString("D"),
                PasswordHash = ApplicationExtensions.PasswordGenerate().Hash(),
                CreatedAt = ApplicationExtensions.TIME_NOW
            };
            List<PermissionEntities> permissions = new List<PermissionEntities>
            {
                new PermissionEntities
                {
                    Id = ApplicationExtensions.TIME_NOW.GenerateUserId("p"),
                    Name = EnumHelper<PermissionEnumTypes>.GetDisplayValue(PermissionEnumTypes.SUPERADMIN),
                    Claim = PermissionEnumTypes.SUPERADMIN,
                    CreatedAt = ApplicationExtensions.TIME_NOW
                },
                new PermissionEntities
                {
                    Id = ApplicationExtensions.TIME_NOW.GenerateUserId("p"),
                    Name = EnumHelper<PermissionEnumTypes>.GetDisplayValue(PermissionEnumTypes.ADMIN),
                    Claim = PermissionEnumTypes.ADMIN,
                    CreatedAt = ApplicationExtensions.TIME_NOW
                }
            };
            UserPermissionEntities userPermission = new UserPermissionEntities
            {
                Id = Guid.NewGuid().ToString(),
                PermissionId = permissions[0].Id,
                UserId = user.Id,
                CreatedAt = ApplicationExtensions.TIME_NOW
            };
            return (user, permissions, userPermission);
        }
    }
}
