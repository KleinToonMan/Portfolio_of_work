using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WIL_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for MaterialCalculator.xaml
    /// </summary>
    public partial class MaterialCalculator : Window
    {
        public MaterialCalculator()
        {
            InitializeComponent();
            //FOR TESTING
            LoadOptions();
        }
        private void CalculateTotals()
        {
            double total = 0;

            foreach (MaterialCalculatorItem item in mainList.Children.OfType<MaterialCalculatorItem>())
            {
                total += item.CalculatedTotal;
            }
            TotalBox.Text = "R " + String.Format("{0:0,0.00}", total); ; // Update UI with the calculated total
        }

        public void LoadOptions()
        {
            MaterialCalculatorItem item = new MaterialCalculatorItem();
            item.MatCombo.SelectionChanged += MatCombo_SelectionChanged;
            item.ValueChanged += Item_ValueChanged;
            mainList.Children.Add(item);
        }

        private void MatCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the selection was made in the last ComboBox
            if (sender is ComboBox comboBox && comboBox.Equals((mainList.Children[mainList.Children.Count - 1] as MaterialCalculatorItem)!.MatCombo))
            {
                LoadOptions();
            }
        }
        private void Item_ValueChanged(object sender, RoutedEventArgs e)
        {
            CalculateTotals(); // Recalculate totals
        }

        private void CopyToClipboardButton(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TotalBox.Text.ToString());
            MessageBox.Show("Copied to clipboard.");
        }
    }
}
