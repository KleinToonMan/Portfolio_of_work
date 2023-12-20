using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIL_DesktopApp.DataModels;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using KryptonUser = WIL_DesktopApp.Models.KryptonUser;
using KryptonUserDTO = WIL_DesktopApp.DataModels.KryptonUser;

namespace WIL_DesktopApp.Services.KryptonUserServices
{
    public class KryptonUserUpdator : IKryptonUserUpdator
    {
        private IKryptonDbContextFactory _contextFactory;
        public KryptonUserUpdator(IKryptonDbContextFactory kryptonDbContextFactory)
        {
            _contextFactory = kryptonDbContextFactory;
        }
        public void UpdateUser(KryptonUser oldUser, KryptonUser updatedUser)
        {
            using (KryptonDbContext context = _contextFactory.CreateKryptonDbContext())
            {
                KryptonUserDTO kryptonUserDTO = context.KryptonUsers.Single<KryptonUserDTO>(u => u.Username == oldUser.Username);
                UserInfo userInfo = context.UserInfo.Single<UserInfo>(u => u.InfoId == kryptonUserDTO.InfoId);

                if (oldUser.Username != updatedUser.Username)
                {
                    context.KryptonUsers.Remove(kryptonUserDTO);
                    context.SaveChanges();
                    kryptonUserDTO.Username = updatedUser.Username;
                    kryptonUserDTO.UserType = updatedUser.Type;
                    userInfo.Email = updatedUser.Email;
                    userInfo.FirstName = updatedUser.FirstName;
                    userInfo.LastName = updatedUser.LastName;
                    kryptonUserDTO.InfoId = userInfo.InfoId;
                    context.KryptonUsers.Add(kryptonUserDTO);
                    context.SaveChanges();
                }
                else
                {
                    kryptonUserDTO.Username = updatedUser.Username;
                    kryptonUserDTO.UserType = updatedUser.Type;
                    userInfo.Email = updatedUser.Email;
                    userInfo.FirstName = updatedUser.FirstName;
                    userInfo.LastName = updatedUser.LastName;

                    context.SaveChanges();
                }
            }
        }
    }
}
