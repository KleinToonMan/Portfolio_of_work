using System.Linq;
using System.Threading.Tasks;
using WIL_DesktopApp.DataModels;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using KryptonUserDTO = WIL_DesktopApp.DataModels.KryptonUser;
using KryptonUser = WIL_DesktopApp.Models.KryptonUser;

namespace WIL_DesktopApp.Services.KryptonUserServices
{
    public class KryptonUserRemover : IKryptonUserRemover
    {
        private readonly IKryptonDbContextFactory _kryptonDbContextFactory;

        /// <summary>
        /// Service for deleting users from the database
        /// </summary>
        /// <param name="kryptonDbContextFactory"></param>
        public KryptonUserRemover(IKryptonDbContextFactory kryptonDbContextFactory)
        {
            _kryptonDbContextFactory = kryptonDbContextFactory;
        }

        /// <summary>
        /// Removes a given user model from the database
        /// </summary>
        /// <param name="user"></param>
        public void RemoveUser(KryptonUser user)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                KryptonUserDTO kUserDTO = context.KryptonUsers.Single<KryptonUserDTO>(u => u.Username == user.Username);
                UserInfo userInfo = context.UserInfo.Single<UserInfo>(u => u.InfoId == kUserDTO.InfoId);
                context.KryptonUsers.Remove(kUserDTO);
                context.UserInfo.Remove(userInfo);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Asynchronously removes a given user model from the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task RemoveUserAsync(KryptonUser user)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                KryptonUserDTO kUserDTO = KryptonUserDTOConverter.ToKryptonUserDTO(user);
                UserInfo userInfo = new UserInfo();
                userInfo = kUserDTO.Info;
                context.KryptonUsers.Remove(kUserDTO);
                context.UserInfo.Remove(userInfo);
                await context.SaveChangesAsync();
            }
        }
    }
}
