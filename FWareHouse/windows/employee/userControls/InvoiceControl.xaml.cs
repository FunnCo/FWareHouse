using FWareHouse.common.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Snickler.EFCore;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FWareHouse.common.entity;
using FWareHouse.common;

namespace FWareHouse.windows.employee.userControls
{
    /// <summary>
    /// Логика взаимодействия для InvoiceControl.xaml
    /// </summary>
    public partial class InvoiceControl : UserControl
    {

        private List<InvoiceInfo> invoiceInfos = new List<InvoiceInfo>();
        private List<Invoice> rawInvoices;
        private List<dynamic> tableItemsSource;

        public InvoiceControl()
        {
            InitializeComponent();

            using (ApplicationContext context = new ApplicationContext())
            {
                rawInvoices = context.invoice.Where(x => x.responsible_employee == Repository.currentId).ToList();
                List<string> neededInvoices = new List<string>();
                foreach (Invoice item in rawInvoices)
                {
                    neededInvoices.Add(item.invoice_code);
                }
                neededInvoices = neededInvoices.Distinct().ToList();

                Employee user = context.employee.Where(x => x.id == Repository.currentId).First();
                foreach (string invoiceCode in neededInvoices)
                {

                    context.LoadStoredProc("get_invoice_info").WithSqlParam("needed_invoice_number", invoiceCode).ExecuteStoredProc((handler) =>
                    {
                        Console.WriteLine(Repository.currentId.ToString());
                        invoiceInfos.AddRange(handler.ReadToList<InvoiceInfo>().ToList<InvoiceInfo>().Where(x => x.employee == user.firstname + " " + user.surname + " " + user.patronymic));
                    });
                }
                invoiceInfos = invoiceInfos.Distinct().ToList();

                //Ужасно стремная хрень для заполнения таблички
                List<dynamic> datagirdItemSource = new List<dynamic>();
                foreach (InvoiceInfo item in invoiceInfos)
                {
                    datagirdItemSource.Add(new {Product = item.name, Count = item.count, Date = item.operation_date });
                }
                

                this.invoices.ItemsSource = datagirdItemSource;
            }
        }

        private void invoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedItemDate;
            try
            {
                selectedItemDate = (invoices.SelectedItem as dynamic).Date;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Selected empty item");
                return;
            }
            catch (Exception)
            {
                Console.WriteLine(e.Source);
                return;
            }
            InvoiceInfo selectedInvoice = invoiceInfos.Where(x=> x.operation_date == selectedItemDate).FirstOrDefault();
            this.SelectedCount.Text = selectedInvoice.count.ToString();
            this.SelectedProduct.Text = selectedInvoice.name.ToString();
            this.SelectionFrom.Text = selectedInvoice.sender_name.ToString();
            this.SelectionTo.Text = selectedInvoice.receiver_name.ToString();
            this.SelectionWhen.Text = selectedInvoice.operation_date.ToString();


        }
    }
}
