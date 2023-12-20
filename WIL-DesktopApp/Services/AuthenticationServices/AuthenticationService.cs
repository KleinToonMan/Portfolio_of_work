using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services.KryptonUserServices;
using WIL_DesktopApp.Services.MaterialServices;
using WIL_DesktopApp.Services.RequestServices;
using WIL_DesktopApp.Stores;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using kryptonUserDM = WIL_DesktopApp.DataModels.KryptonUser;
using userInfo = WIL_DesktopApp.DataModels.UserInfo;

namespace WIL_DesktopApp.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly KryptonDbContextFactory _kryptonDbContextFactory;
        private readonly RequestRepository reqRepository;
        private readonly MaterialRepository matRepository;
        private readonly UserRepository userRepository;
        public AuthenticationService(KryptonDbContextFactory kryptonDbContextFactory)
        {
            try
            {
                _kryptonDbContextFactory = kryptonDbContextFactory;
                IRequestProvider requestProvider = new RequestProvider(kryptonDbContextFactory);
                IRequestRemover requestRemover = new RequestRemover(kryptonDbContextFactory);
                reqRepository = new RequestRepository(requestProvider, requestRemover);

                IMaterialCreator materialCreator = new MaterialCreator(kryptonDbContextFactory);
                IMaterialProvider materialProvider = new MaterialProvider(kryptonDbContextFactory);
                IMaterialRemover materialRemover = new MaterialRemover(kryptonDbContextFactory);
                IMaterialUpdater materialUpdater = new MaterialUpdater(kryptonDbContextFactory);
                matRepository = new MaterialRepository(materialCreator, materialUpdater, materialRemover, materialProvider);

                IKryptonUserProvider userProvider = new KryptonUserProvider(kryptonDbContextFactory);
                IKryptonUserCreator userCreator = new KrytonUserCreator(kryptonDbContextFactory);
                IKryptonUserRemover userRemover = new KryptonUserRemover(kryptonDbContextFactory);
                IKryptonUserUpdator userUpdator = new KryptonUserUpdator(kryptonDbContextFactory);
                userRepository = new UserRepository(userCreator, userProvider, userRemover, userUpdator);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No database connection. " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        public User? GetAuthenticatedUser(string username, string password)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                try 
                {
                    //Where(u => u.Username == username)
                    //Queries the database to check of the user exists
                    var query = (from Users in context.KryptonUsers
                                 join Info in context.UserInfo on Users.InfoId equals Info.InfoId
                                 select new
                                 {
                                     Users.Username,
                                     Users.Password,
                                     Users.UserType,
                                     Info.FirstName,
                                     Info.LastName,
                                     Info.Email
                                 })
                .SingleOrDefault(u => u.Username == username);
                

                    if (query != null)
                    {
                        //If the user exists the password the user entered gets verified
                        IPasswordHasher hasher = new PasswordHasher();
                        //returns null or authenticated user,
                        ///Checking of the password is valid
                        if(hasher.VerifyHash(password, query.Password))
                        {
                            if(query.UserType == 1)
                            {
                                return new User(query.Username, query.FirstName, query.LastName, query.Email, query.UserType,
                            matRepository, reqRepository, userRepository);
                            }
                            else
                            {
                                return new User(query.Username, query.FirstName, query.LastName, query.Email, query.UserType,
                            matRepository, reqRepository);
                            }
                            
                        }
                        else
                        {
                            return null;
                        }         
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("No database connection. " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                //If query is null the user does not exist

            }
        }

    }
}
