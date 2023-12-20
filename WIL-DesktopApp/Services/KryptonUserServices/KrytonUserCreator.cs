using System.Threading.Tasks;
using WIL_DesktopApp.DataModels;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using KryptonUserDTO = WIL_DesktopApp.DataModels.KryptonUser;
using KryptonUser = WIL_DesktopApp.Models.KryptonUser;
namespace WIL_DesktopApp.Services.KryptonUserServices
{
    public class KrytonUserCreator : IKryptonUserCreator
    {
        private readonly IKryptonDbContextFactory _kryptonDbContextFactory;
        /// <summary>
        /// Service for adding users to a database
        /// </summary>
        /// <param name="kryptonDbContextFactory"></param>
        public KrytonUserCreator(IKryptonDbContextFactory kryptonDbContextFactory)
        {
            _kryptonDbContextFactory = kryptonDbContextFactory;
        }

        /// <summary>
        /// Adds a krypton user to the database given a user model
        /// </summary>
        /// <param name="user"></param>
        public void CreateUser(KryptonUser user)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                UserInfo userInfo = new UserInfo();
                userInfo.FirstName = user.FirstName;
                userInfo.LastName = user.LastName;
                userInfo.Email = user.Email;
                context.Add(userInfo);
                context.SaveChanges();
                KryptonUserDTO kUserDTO = KryptonUserDTOConverter.ToKryptonUserDTO(user);
                kUserDTO.InfoId = userInfo.InfoId;
                context.KryptonUsers.Add(kUserDTO);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Asynchronously adds a krypton user to the database given a user model
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public async Task CreateUserAsync(KryptonUser user)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                UserInfo userInfo = new UserInfo();
                userInfo.FirstName = user.FirstName;
                userInfo.LastName = user.LastName;
                userInfo.Email = user.Email;
                context.Add(userInfo);
                await context.SaveChangesAsync();
                KryptonUserDTO kUserDTO = KryptonUserDTOConverter.ToKryptonUserDTO(user);
                kUserDTO.InfoId = userInfo.InfoId;
                context.KryptonUsers.Add(kUserDTO);
                await context.SaveChangesAsync();
            }
        }
    }
}
