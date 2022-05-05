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
    /// Логика взаимодействия для RegisterPartnerCardControl.xaml
    /// </summary>
    public partial class RegisterPartnerCardControl : UserControl
    {
        public RegisterPartnerCardControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (etxtEmail.Text.Trim() == "" || etxtName.Text.Trim() == "" || etxtOrganisation.Text.Trim() == "" || etxtPassword.Password.Trim() == "" || etxtPhone.Text.Trim() == "" || etxtAddress.Text.Trim() == "")
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (ApplicationContext context = new ApplicationContext())
            {
                Partner newUser = new Partner(
                    (int)context.partner.LongCount(),
                    etxtName.Text,
                    etxtAddress.Text,
                    etxtPhone.Text,
                    etxtEmail.Text,
                    etxtPassword.Password
                    );

                context.partner.Add(newUser);
                context.SaveChanges();

            }
        }
    }
}
