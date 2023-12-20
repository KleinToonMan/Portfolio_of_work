using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIL_DesktopApp.Services.KryptonUserServices;

namespace WIL_DesktopApp.Models
{
    public class UserRepository
    {
        private readonly IKryptonUserCreator _kuserCreator;
        private readonly IKryptonUserProvider _kuserProvider;
        private readonly IKryptonUserRemover _kuserRemover;
        private readonly IKryptonUserUpdator _kuserUpdator;
        public UserRepository(IKryptonUserCreator userCreator,IKryptonUserProvider userProvider, IKryptonUserRemover userRemover, IKryptonUserUpdator userUpdator)
        {
            _kuserCreator = userCreator;
            _kuserProvider = userProvider;
            _kuserRemover = userRemover;
            _kuserUpdator = userUpdator;
        }

        public void UpdateUser(KryptonUser oldUser, KryptonUser updatedUser)
        {
            _kuserUpdator.UpdateUser(oldUser, updatedUser);
        }
        /// <summary>
        /// Async and sync methods for getting all users from the database
        /// Async method to be added
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KryptonUser> GetAllUsers() 
        {
            return _kuserProvider.GetAllKryptonUsers();
        }

        /// <summary>
        /// Async and sync methods for adding users
        /// Async method to be added
        /// </summary>
        /// <returns></returns>
        public void AddUser(KryptonUser user) 
        {
            _kuserCreator.CreateUser(user);
        }
        /// <summary>
        /// Async and sync methods for removing users
        /// Async method to be added
        /// </summary>
        /// <returns></returns>
        public void RemoveUser(KryptonUser user) 
        {
            _kuserRemover.RemoveUser(user);
        }
        /// <summary>
        /// Async and sync methods for updating users, method to be implemented
        /// Async method to be added
        /// </summary>
        /// <returns></returns>
    }
}
