using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services;
using WIL_DesktopApp.ViewModels;

namespace WIL_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    /// 
    public partial class Dashboard : UserControl
    {

        public Dashboard(User _user)
        {
            InitializeComponent();
            MenuBar _menuBar = new MenuBar(_user.Type);
            MenuBar.Children.Add(_menuBar);
            LoadRequests();
        }

        public void LoadRequests(List<Request> searched = null)
        {
            if (searched == null)
            {
                searched = new List<Request>();
            }
            else
            {
                RequestPanel1.Children.Clear();
                RequestPanel2.Children.Clear();
            }
            DashboardViewModel dashboardViewModel = new();

            List<UserControl> requests = new List<UserControl>();
            if (searched.Count > 0)
            {
                foreach(Request req in searched)
                {
                    requests.Add(dashboardViewModel.LoadUserControl(req));
                }
            }
            else { 
                requests = dashboardViewModel._requestsToLoad!;
            }
            
            int pos = 0;
            foreach (var item in requests)
            {
                Console.WriteLine(requests.Count);
                if (pos == 0)
                {
                    pos++;
                    RequestPanel1.Children.Add(item);
                }
                else
                {
                    pos--;
                    RequestPanel2.Children.Add(item);
                }

            }

        }
    }
}

