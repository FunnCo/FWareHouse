using FWareHouse.windows.employee.userControls;
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

namespace FWareHouse.windows.employee
{
    /// <summary>
    /// Логика взаимодействия для EmployeeForm.xaml
    /// </summary>
    public partial class EmployeeForm : Window
    {
        public EmployeeForm()
        {
            InitializeComponent();
            InvoiceCheckButton_Click(null, null);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        InvoiceControl invoiceControl;
 

        private void InvoiceCheckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                InvoiceCheckButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#64dd17");
                InvoiceCheckButton.FontWeight = FontWeights.Medium;
                this.root_grid.Children.Remove(invoiceControl);
                invoiceControl = new InvoiceControl();
                Grid.SetRow(invoiceControl, 1);
                Grid.SetColumn(invoiceControl, 1);
                this.root_grid.Children.Add(invoiceControl);
            }
            catch (ArgumentException exception) {
                throw exception;
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
