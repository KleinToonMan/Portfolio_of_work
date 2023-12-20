using Microsoft.EntityFrameworkCore;

namespace WIL_DesktopApp.DataModels.DbContexts
{
    public class KryptonDbContextFactory : IKryptonDbContextFactory
    {
        private readonly string _connectionString;
        public KryptonDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public KryptonDbContext CreateKryptonDbContext()
        {
            DbContextOptionsBuilder<KryptonDbContext> optionsBuilder = new DbContextOptionsBuilder<KryptonDbContext>();
            optionsBuilder.UseMySQL(_connectionString);
            return new KryptonDbContext(optionsBuilder.Options);
        }
    }
}
