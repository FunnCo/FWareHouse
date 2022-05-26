using FWareHouse.common.database;
using FWareHouse.common.entity;
using FWareHouse.windows.employee;
using FWareHouse.windows.partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FWareHouse
{
    /// <summary>
    /// Логика взаимодействия для LoginCardControl.xaml
    /// </summary>
    public partial class LoginCardControl : UserControl
    {
        public LoginCardControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (etxtEmail.Text.Trim() == "" || etxtPassword.Password.Trim() == "")
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            using var sha1 = SHA1.Create();
            String hashedPassword = Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(etxtPassword.Password))).ToLower();
            using (ApplicationContext context = new ApplicationContext())
            {            
                if (MainWindow.isEmployeeSelected)
                {                                     
                    Employee user = context.employee.Where(item => item.email == etxtEmail.Text && item.password == hashedPassword).FirstOrDefault<Employee>();
                    if (user == null)
                    {
                        MessageBox.Show("Вы ввели неправильный e-mail или пароль. Пожалуйста, проверьте данные и введите их снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        (new EmployeeForm()).Show();
                        Window.GetWindow(this).Close();
                    }
                }
                else
                {
                    Partner user = context.partner.Where(item => item.email == etxtEmail.Text && item.password == hashedPassword).FirstOrDefault<Partner>();
                    List<StoredProduct> products = context.products_current_info.ToList();
                    Console.WriteLine(products.Count);
                    //Console.WriteLine("\n\n\n");
                    //Console.WriteLine(Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(etxtPassword.Password))).ToLower());
                    //Console.WriteLine("\n\n\n");

                    if (user == null)
                    {
                        MessageBox.Show("Вы ввели неправильный e-mail или пароль. Пожалуйста, проверьте данные и введите их снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        var window = new PartnerForm();
                        window.Opacity = 0;
                        window.Show();

                        DoubleAnimation animation = new DoubleAnimation();
                        animation.From = 0;
                        animation.To = 1;
                        animation.Duration = new Duration(TimeSpan.FromSeconds(0.325));
                        window.BeginAnimation(OpacityProperty, animation);
                        Window.GetWindow(this).Close();
                    }
                }
            }
        }
    }
}
