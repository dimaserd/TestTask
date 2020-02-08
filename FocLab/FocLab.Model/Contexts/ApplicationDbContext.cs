using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FocLab.Model.Contexts
{
    /// <summary>
    /// Контекст базы данных для приложения Химия
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public const string ServerConnection = "ServerConnection";

        public const string LocalConnection = "DefaultConnection";

#if DEBUG
        public static string ConnectionString => LocalConnection;

#else
        public static string ConnectionString => ServerConnection;
#endif

        public static ApplicationDbContext Create(IConfiguration configuration)
        {
            return Create(configuration.GetConnectionString(ConnectionString));
        }

        public static ApplicationDbContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}