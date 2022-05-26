using FWareHouse.common;
using FWareHouse.common.database;
using FWareHouse.common.entity;
using FWareHouse.common.model;
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

namespace FWareHouse.windows.partner.userControls
{
    /// <summary>
    /// Логика взаимодействия для BasketControl.xaml
    /// </summary>
    public partial class BasketControl : UserControl
    {

        private List<OrderedProductModel> products;
        private List<dynamic> productNames;

        public BasketControl()
        {
            InitializeComponent();

            var currentOrder = CurrentOrder.getInstance();
            products = currentOrder.getOrder();
            productNames = new List<dynamic>();
            foreach (OrderedProductModel item in products)
            {
                productNames.Add(new { Name = item.name });
            }

            calculateTotal();

            this.orders.ItemsSource = productNames;
            this.orders.SelectedIndex = 0;
        }

        private void calculateTotal()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                int positions = 0;
                double sumWeight = 0.0;
                double sumVolume = 0.0;
                for (int i = 0; i < products.Count(); i++)
                {
                    StoredProduct product = context.products_current_info.Where(x => x.name == products[i].name).First();
                    OrderedProductModel model = products.FirstOrDefault(x => x.name == products[i].name);

                    positions += model.count;

                    sumVolume += (Math.Round(product.volume * model.count * 10000) / 10000);
                    sumWeight += (Math.Round(product.weight * model.count * 10000) / 10000);
                }
                this.totalPositions.Text = positions.ToString();
                this.totalWeight.Text = sumWeight.ToString();
                this.totalVolume.Text = sumVolume.ToString();
            }
        }
        private void deleteSelectedOrder_Click(object sender, RoutedEventArgs e)
        {
            int indexOfSelected = orders.SelectedIndex;
            if (indexOfSelected == -1)
            {
                return;
            }
            productNames.RemoveAt(indexOfSelected);
            products.RemoveAt(indexOfSelected);
            orders.ItemsSource = null;
            orders.ItemsSource = productNames;
            orders.SelectedIndex = indexOfSelected - 1;
            calculateTotal();
        }

        private void ConfirmOrders_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int documentNumber = random.Next(100000, 999999999);
            using (ApplicationContext context = new ApplicationContext())
            {
                List<Employee> responsibleEmployees = context.employee.Where(x => x.position_id == 2 || x.position_id == 3).ToList();
                foreach (OrderedProductModel orderedProduct in products)
                {
                    StoredProduct product = context.products_current_info.Where(x => x.name == orderedProduct.name).First();
                    string query = "CALL reg_outcome_product(";
                    query += product.id + ",";
                    query += orderedProduct.count + ",";
                    query += documentNumber + ",";
                    query += Repository.currentId + ",";
                    query += context.transport_company.Where(x => x.name == orderedProduct.company).First().id + ",";
                    query += responsibleEmployees[random.Next(0, responsibleEmployees.Count)].id;

                    query += ");";
                    Console.WriteLine(query);
                    context.executeNonReturningQuery(query);
                }
            }
        }

        private void ClearOrders_Click(object sender, RoutedEventArgs e)
        {
            orders.ItemsSource = null;
        }

        private void orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                string selectedItemName = "";
                try
                {
                    selectedItemName = (orders.SelectedItem as dynamic).Name;
                }
                catch (NullReferenceException exception)
                {
                    Console.WriteLine("Selected empty item");
                    return;
                }
                catch (Exception)
                {
                    Console.WriteLine(e.Source);
                    return;
                }
                StoredProduct product = context.products_current_info.Where(x => x.name == selectedItemName).First();
                OrderedProductModel model = products.FirstOrDefault(x => x.name == selectedItemName);
                this.SelectedName.Text = product.name.ToString();
                this.SelectedCount.Text = model.count.ToString();
                this.SelectedCompany.Text = model.company.ToString();
                this.SelectedVolume.Text = (Math.Round(product.volume * model.count * 10000) / 10000).ToString();
                this.SelectedWeight.Text = (Math.Round(product.weight * model.count * 10000) / 10000).ToString();
                this.SelectedDescription.Text = product.description.ToString();
            }
        }
    }
}
