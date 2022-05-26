using FWareHouse.common.database;
using FWareHouse.common.entity;
using FWareHouse.common.model;
using System;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows;
using FWareHouse.common;

namespace FWareHouse
{
    /// <summary>
    /// Логика взаимодействия для OrderControl.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {
        public OrderControl()
        {
            InitializeComponent();
            using (ApplicationContext context = new ApplicationContext())
            {
                var products = context.products_current_info.Select(x => new PartnerTableRow { name = x.name, count = x.stored }).Where(x => x.count > 0).ToList();
                this.products.ItemsSource = products;

                var tranports = context.transport_company.Select(x => new TransportCompany { name = x.name }).ToList();
                List<String> companies = new List<String>();
                foreach(TransportCompany item in tranports)
                {
                    companies.Add(item.name);
                }
                this.ordered_company.ItemsSource = companies;
            }
        }


    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    private void products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                string selectedItemName = "";
                try { 
                selectedItemName = (products.SelectedItem as PartnerTableRow).name;
                } catch (NullReferenceException exception)
                {
                    Console.WriteLine("Selected empty item");
                    return;
                }
                StoredProduct product = context.products_current_info.Where(x => x.name == selectedItemName).First();
                this.volume.Text = product.volume.ToString();
                this.weight.Text = product.weight.ToString();
                this.description.Text = product.description.ToString();
                this.count.Text = product.stored.ToString();
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(this.ordered_company.Text.Trim() == "" || this.ordered_count.Text.Trim() == "" || this.description.Text.Trim() == "")
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string selectedItemName = (products.SelectedItem as PartnerTableRow).name;
            ApplicationContext context = new ApplicationContext();
            CurrentOrder.getInstance()
                .addProduct(
                    context.products_current_info
                    .Select(x => new OrderedProductModel { 
                        name = x.name,
                        count = Convert.ToInt32(this.ordered_count.Text.ToString()),
                        company = this.ordered_company.Text.ToString()
                    })
                    .Where(x => x.name == selectedItemName).First());
        }
    }
}
