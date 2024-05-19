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
                _context.Users.Add(seedUser);
                _context.SaveChanges();
            }
        }

        private UserEntities Seeder()
        {
            UserEntities user = new UserEntities
            {
                Id = ApplicationExtensions.TIME_NOW.GenerateUserId(),
                DisplayName = "Super Admin",
                FullName = "Quản trị viên cấp cao",
                UserName = $"application{DateTime.UtcNow.Ticks}".ToLower(),
                Email = "superadmin@application.com",
                SecurityStamp = new Guid().ToString("D"),
                PasswordHash = ApplicationExtensions.PasswordGenerate().Hash()
            };
            return user;
        }
    }
}
