using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.KryptonUserServices
{
    public interface IKryptonUserUpdator
    {
        void UpdateUser(KryptonUser oldUser, KryptonUser updatedUser);

    }
}
