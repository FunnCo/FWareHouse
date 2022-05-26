using FWareHouse.windows.partner.userControls;
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

namespace FWareHouse.windows.partner
{
    /// <summary>
    /// Логика взаимодействия для PartnerForm.xaml
    /// </summary>
    public partial class PartnerForm : Window
    {
        public PartnerForm()
        {
            InitializeComponent();
            this.OrderCheckButton_Click(null, null);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void Button_Click_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        OrderControl orderControl;
        BasketControl basketControl;

        private void OrderCheckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BasketCheckButton.Foreground = new SolidColorBrush(Colors.DarkGray);
                OrderCheckButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#64dd17");
                BasketCheckButton.FontWeight = FontWeights.Regular;
                OrderCheckButton.FontWeight = FontWeights.Medium;
                this.root_grid.Children.Remove(orderControl);
                orderControl = new OrderControl();
                this.root_grid.Children.Remove(basketControl);
                Grid.SetRow(orderControl, 1);
                Grid.SetColumn(orderControl, 1);
                this.root_grid.Children.Add(orderControl);
            } catch(ArgumentException) { }



        }

        private void BasketCheckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BasketCheckButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#64dd17");
                OrderCheckButton.Foreground = new SolidColorBrush(Colors.DarkGray);
                BasketCheckButton.FontWeight = FontWeights.Medium;
                OrderCheckButton.FontWeight = FontWeights.Regular;
                this.root_grid.Children.Remove(basketControl);                
                basketControl = new BasketControl();
                this.root_grid.Children.Remove(orderControl);
                Grid.SetRow(basketControl, 1);
                Grid.SetColumn(basketControl, 1);
                this.root_grid.Children.Add(basketControl);
            }
            catch (ArgumentException) { }
        }
    }
}
