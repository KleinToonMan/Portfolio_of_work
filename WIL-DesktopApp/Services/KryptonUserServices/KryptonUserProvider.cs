using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using KrytonUserDTO = WIL_DesktopApp.DataModels.KryptonUser;
using KrytonUserInfoDTO = WIL_DesktopApp.DataModels.UserInfo;

namespace WIL_DesktopApp.Services.KryptonUserServices
{
    public class KryptonUserProvider : IKryptonUserProvider
    {
        private readonly IKryptonDbContextFactory _kryptonDbContextFactory;
        public KryptonUserProvider(IKryptonDbContextFactory kryptonDbContextFactory)
        {
            _kryptonDbContextFactory = kryptonDbContextFactory;
        }
        /// <summary>
        /// Asynchronously provides a list of krypton user models from the database
        /// </summary>
        /// <returns>List of material models</returns>
        public async Task<IEnumerable<KryptonUser>> GetAllKryptonUsersAsync()
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                IEnumerable<KrytonUserDTO> kUserdtos = await context.KryptonUsers.ToListAsync();

                //return kUserdtos.Select(r => KryptonUserDTOConverter.ToUserModel(r));
                //Tempary until DTOConverter is fully developed
                return new List<KryptonUser> { };
            }
        }

        /// <summary>
        /// Provides a list of krypton user models from the database
        /// </summary>
        /// <returns></returns>
        public List<KryptonUser> GetAllKryptonUsers()
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                var query = from user in context.KryptonUsers
                              join info in context.UserInfo
                              on user.InfoId equals info.InfoId
                              select new
                              {
                                  user.Username,
                                  user.UserType,
                                  info.FirstName,
                                  info.LastName,
                                  info.Email,
                              };

                return query.Select(r => new KryptonUser(r.Username, r.FirstName, r.LastName, r.Email, r.UserType)).ToList();
            }
        }
    }
}
