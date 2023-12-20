using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services;
using WIL_DesktopApp.Services.MaterialServices;
using WIL_DesktopApp.Services.RequestServices;
using WIL_DesktopApp.Stores;
using WIL_DesktopApp.Views;

namespace WIL_DesktopApp.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly UserStore? _userStore;
        private readonly IEnumerable<Material>? _materials;
        private readonly List<Request>? _requests;
        public readonly List<UserControl>? _requestsToLoad;

        public DashboardViewModel()
        {

            _requests = NavService._UserStore.Requests.ToList();

            _requestsToLoad = new List<UserControl>();

            foreach (var item in _requests)
            {
                _requestsToLoad.Add(LoadUserControl(item));
            }
        }
        public UserControl LoadUserControl(Request req)
        {
            return new DashboardRequestItem(req);
        }
    }
}
