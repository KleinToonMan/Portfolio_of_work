namespace WIL_DesktopApp.DataModels.DbContexts
{
    public interface IKryptonDbContextFactory
    {
        KryptonDbContext CreateKryptonDbContext();
    }
}
