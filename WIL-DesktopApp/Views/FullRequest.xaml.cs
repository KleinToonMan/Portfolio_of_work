using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services;
using WIL_DesktopApp.ViewModels;

namespace WIL_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for FullRequest.xaml
    /// </summary>
    public partial class FullRequest : Window
    {
        private Request request;
        public FullRequest(Request req)
        {
            request = req;

            InitializeComponent();

            requestID.Text = request.RequestId.ToString();

            if (request.Email != null)
            {
                email.Text = request.Email.ToString();
            }
            else
            {
                email.Text = string.Empty;
            }

            if (request.FileURL != null)
            {
                fileURL.Text = request.FileURL.ToString();
            }
            else
            {
                fileURL.Text = string.Empty;
            }

            if (request.Notes != null)
            {
                notes.Text = request.Notes.ToString();
            }
            else
            {
                notes.Text = string.Empty;
            }

            foreach (var item in request.RequestItems)
            {
                string stringAtts = FlattenAttributes(item);

                var r = new RequestItemDataViewModel
                {
                    ItemId = item.Id,
                    Name = item.Name,
                    Attributes = stringAtts,
                    EstimateGiven = item.EstimateGiven, // Formats as currency
                    Quantity = item.Quantity,
                    Estimate = item.GetQuoteEstimate()*item.Quantity
                };


                RequestItemsListView.Items.Add(r);
            }

        }

        private string FlattenAttributes(RequestItem item)
        {
            if (item == null || item.Attributes == null)
            {
                return null!;
            }

            StringBuilder result = new StringBuilder();
            int firstVal = 0;
            foreach (var attribute in item.Attributes)
            {
                if (firstVal == 0)
                {
                    firstVal++;
                }
                else
                {
                    result.Append(attribute.Name + ": ");

                    // Concatenate attribute values with commas
                    if (attribute.Values != null && attribute.Values.Count > 0)
                    {
                        result.Append(string.Join(", ", attribute.Values) + ", ");
                    }
                }
                
            }

            return result.ToString().TrimEnd(new char[] { ' ', ',' });
        }

        private void ContactClient(object sender, RoutedEventArgs e)
        {
            string email = request.Email.ToString();
            string subject = "Regarding your quote : #"+request.RequestId;

            string mailtoUri = $"mailto:{email}?subject={Uri.EscapeDataString(subject)}";

            // Start the default mail client
            Process.Start(new ProcessStartInfo
            {
                FileName = mailtoUri,
                UseShellExecute = true
            });
        }

        private void PrepareQuote(object sender, RoutedEventArgs e)
        {
            FinalQuote fc = new FinalQuote(request);
            NavService.Navigate(fc);
            this.Close();
        }
    }
}
