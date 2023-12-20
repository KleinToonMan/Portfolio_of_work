using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services;

namespace WIL_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for FinalQuote.xaml
    /// </summary>
    public partial class FinalQuote : Window
    {
        Request _request;
        public FinalQuote(Request rq)
        {
            InitializeComponent();
            _request = rq;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            FullRequest fr = new(_request);
            fr.Show();
            this.Close();
        }

        private void ContactClient(object sender, RoutedEventArgs e)
        {
            PdfGenerator pdf = new();
            pdf.GeneratePdfAndEmail(_request,"Krypton Signs","Here is your attached quote.");
        }
    }
}
