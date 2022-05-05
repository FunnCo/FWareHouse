using FWareHouse.common.database;
using FWareHouse.common.entity;
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

            using (ApplicationContext context = new ApplicationContext())
            {
                Partner user = null;

                if (MainWindow.isEmployeeSelected)
                {

                }
                else
                {
                    user = context.partner.Where(item => item.email == etxtEmail.Text && item.password == etxtPassword.Password).FirstOrDefault<Partner>();
                    if (user == null)
                    {
                        MessageBox.Show("Вы ввели неправильный e-mail или пароль. Пожалуйста, проверьте данные и введите их снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Вы: " + user.name, "Успех", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }
    }
}
