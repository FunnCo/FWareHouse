using FWareHouse.common.database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FWareHouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new ApplicationContext();
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

        public static bool isEmployeeSelected = false;
        private void Button_Click_Employee_Mode(object sender, RoutedEventArgs e)
        {
            ParterCheckButton.Foreground = new SolidColorBrush(Colors.DarkGray);
            EmployeeCheckButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#64dd17");
            ParterCheckButton.FontWeight = FontWeights.Regular;
            EmployeeCheckButton.FontWeight = FontWeights.Medium;
            ((Button)LoginCard.FindName("RegisterButton")).IsEnabled = false;
            var registerButton = (Button)RegisterCard.FindName("BackToAuth");
            if (registerButton.IsEnabled)
            {
                // Magic thing, invoking click event in BackToAuth button
                ButtonAutomationPeer peer = new ButtonAutomationPeer(registerButton);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
            }
            isEmployeeSelected = true;
        }


        private void ParterCheckButton_Click(object sender, RoutedEventArgs e)
        {
            ParterCheckButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#64dd17");
            EmployeeCheckButton.Foreground = new SolidColorBrush(Colors.DarkGray);
            ParterCheckButton.FontWeight = FontWeights.Medium;
            EmployeeCheckButton.FontWeight = FontWeights.Regular;
            ((Button)LoginCard.FindName("RegisterButton")).IsEnabled = true;
            isEmployeeSelected = false;
        }
    }
}
