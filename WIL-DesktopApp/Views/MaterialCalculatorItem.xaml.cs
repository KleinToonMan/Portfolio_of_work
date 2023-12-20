using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WIL_DesktopApp.ViewModels;

namespace WIL_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for MaterialCalculatorItem.xaml
    /// </summary>
    public partial class MaterialCalculatorItem : UserControl
    {
        public ComboBox GetComboBox()
        {
            return MatCombo;
        }
        // Initialize UI components and variables
        public MaterialCalculatorItem()
        {
            InitializeComponent();
            DataContext = new MaterialCalcItemViewModel();
        }

        // Store the selected index, unit price, and calculation values
        public int currentIndex = 0;
        private double unitPrice = 0;
        private double value1 = 1;
        private double value2 = 1;
        private double value3 = 1;
        public double CalculatedTotal = 0;

        // Handle selection change in the combo box
        private void MatCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentIndex = GetCurrentIndex();
            unitPrice = SetTotal(currentIndex);
            CalcTotal(); // Recalculate the total
        }

        // Event to signal value changes
        public event RoutedEventHandler ValueChanged;

        // Notify parent about value changes
        private void NotifyValueChanged()
        {
            ValueChanged?.Invoke(this, new RoutedEventArgs());
        }

        // Calculate the total for this item
        private void CalcTotal()
        {
            priceVal.Content = String.Format("{0:0,0.00}", unitPrice * value1 * value2 * value3);
            CalculatedTotal = unitPrice * value1 * value2 * value3;

            NotifyValueChanged(); // Notify parent about the value change
        }

        // Handle text change in the quantity input box
        private void QtyTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Ensure a valid numeric input
            try
            {
                value1 = double.Parse(qtyTxt.Text);
            }
            catch
            {
                // Set quantity to 1 if input is empty or a charachter
                value1 = 1;
            }
            CalcTotal();
        }

        // Handle text change in the unit price input box
        private void UnitTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Ensure a valid numeric input
            try
            {
                value2 = double.Parse(unitTxt.Text);
            }
            catch
            {
                // Set unit price to 1 if input is empty or charachter
                value2 = 1;
            }
            CalcTotal();
        }
        private void value3_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Ensure a valid numeric input
            try
            {
                value3 = double.Parse(value3Txt.Text);
            }
            catch
            {
                // Set unit price to 1 if input is empty or charachter
                value3 = 1;
            }
            CalcTotal();
        }

        // Retrieve unit price based on selected index
        public static double SetTotal(int index)
        {
            MaterialCalcItemViewModel viewModel = new();
            List<double> unitPriceList = (List<double>)viewModel.GetUnitPrice;

            if (index >= 0 && index < unitPriceList.Count)
            {
                return unitPriceList[index];
            }
            else
            {
                // Handle index out of range here (return a default value or handle the error)
                // For example, you can return 0 or throw an exception.
                return 0;
            }
        }

        // Retrieve the currently selected index from the combo box
        public int GetCurrentIndex()
        {
            return MatCombo.SelectedIndex;
        }
    }
}
