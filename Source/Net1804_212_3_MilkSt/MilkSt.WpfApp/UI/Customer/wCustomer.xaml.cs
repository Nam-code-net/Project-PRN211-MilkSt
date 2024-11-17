using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using MilkSt.Business.Category;
using MilkSt.Data.Models;


namespace MilkSt.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wCustomer.xaml
    /// </summary>
    public partial class wCustomer : Window
    {
        private readonly CustomerBusiness _business;
        private Customer _selectedCustomer;


        public wCustomer()
        {
            InitializeComponent();
            this._business ??= new CustomerBusiness();
            //this.LoadGrdCustomers();
        }


        public wCustomer(Customer customer) : this()
        {
            _selectedCustomer = customer;
            this.DataContext = customer;  // Thiết lập DataContext để binding có thể hoạt động
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int customerIdTmp= -1;
                int.TryParse(CustomerID.Text, out customerIdTmp);
                var item = await _business.GetById(customerIdTmp);

                if (item.Data == null)
                {
                    var customer = new Customer()
                    {
                        //CustomerId = int.Parse(CustomerID.Text),
                        Name = Name.Text,
                        Address = Address.Text,
                        Phone = Phone.Text,
                        Email = Email.Text,
                        FacebookId = FacebookId.Text,
                        Language = Language.Text,
                        CustType = CustType.Text,
                        Note = Note.Text,
                        TotalSale = TotalSale.Text
                    };

                    var result = await _business.Save(customer);
                    MessageBox.Show(result.Message, "Save");

                }
                else
                {
                    var customer = item.Data as Customer;
                    //customer.CustomerId = int.Parse(CustomerID.Text); Tu tang nen khong nhap.
                    customer.Name = Name.Text;
                    customer.Address = Address.Text;
                    customer.Phone = Phone.Text;
                    customer.Email = Email.Text;
                    customer.FacebookId = FacebookId.Text;
                    customer.Language = Language.Text;
                    customer.CustType = CustType.Text;
                    customer.Note = Note.Text;
                    customer.TotalSale = TotalSale.Text;


                    var result = await _business.Update(customer);
                    MessageBox.Show("Save successfully");

                }
                CustomerID.Text = string.Empty;
                Name.Text = string.Empty;
                Address.Text = string.Empty;
                Phone.Text = string.Empty;
                Email.Text = string.Empty;
                FacebookId.Text = string.Empty;
                Language.Text = string.Empty;
                CustType.Text = string.Empty;
                Note.Text = string.Empty;
                TotalSale.Text = string.Empty;

                //this.LoadGrdCustomers();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }
        
        /*private async void LoadGrdCustomers()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdCustomer.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                grdCustomer.ItemsSource = new List<Customer>();
            }


        }*/


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*private async void grdCustomer_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Customer;
                    if (item != null)
                    {
                        var currencyResult = await _business.GetById(item.CustomerId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Customer;
                            CustomerID.Text = item.CustomerId.ToString();
                            Name.Text = item.Name;
                            Address.Text = item.Address;
                            Phone.Text = item.Phone;
                            Email.Text = item.Email;
                            FacebookId.Text = item.FacebookId;
                            Language.Text = item.Language;
                            CustType.Text = item.CustType;
                            Note.Text = item.Note;
                            TotalSale.Text = item.TotalSale;

                        }
                    }
                }
            }
        }

        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string customerCode = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(customerCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(customerCode));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCustomers();
                }
            }
        }

        private async void grdCustomer_ButtonView_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button != null)
            {
                int customerId = (int)button.CommandParameter;
                var detailWindow = new wCustomerDetail(customerId);
                detailWindow.Show();
            }
        }
        */
    }
}