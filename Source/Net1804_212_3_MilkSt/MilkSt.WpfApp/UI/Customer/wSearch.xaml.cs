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
using Microsoft.IdentityModel.Tokens;
using MilkSt.Business.Category;
using MilkSt.Data.Models;

namespace MilkSt.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wSearch.xaml
    /// </summary>
    public partial class wSearch : Window
    {
        private readonly CustomerBusiness _business;

        public wSearch()
        {
            InitializeComponent();
            this._business ??= new CustomerBusiness();
            this.LoadGrdSearchs();
        }

        private async void LoadGrdSearchs()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdSearch.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                grdSearch.ItemsSource = new List<Customer>();
            }
        }

        private async void grdSearch_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var customer = grd.SelectedItem as Customer;
                if (customer != null)
                {
                    var currencyResult = await _business.GetById(customer.CustomerId);

                    if (currencyResult.Status > 0 && currencyResult.Data != null)
                    {
                        customer = currencyResult.Data as Customer;
                        var p = new wCustomer(customer);
                        p.Owner = this;
                        p.Show();
                    }
                }
            }
        }


        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await _business.GetAll();  // Giả sử _business.GetAll() trả về tất cả khách hàng
            if (result.Status > 0 && result.Data != null)
            {
                string nameText = Name.Text.ToLower();
                string addressText = Address.Text.ToLower();
                string phoneText = Phone.Text.ToLower();
                string emailText = Email.Text.ToLower();

                var filteredCustomers = (result.Data as List<Customer>).Where(c =>
                    (string.IsNullOrEmpty(nameText) || c.Name.ToLower().Contains(nameText)) &&
                    (string.IsNullOrEmpty(addressText) || c.Address.ToLower().Contains(addressText)) &&
                    (string.IsNullOrEmpty(phoneText) || c.Phone.ToLower().Contains(phoneText)) &&
                    (string.IsNullOrEmpty(emailText) || c.Email.ToLower().Contains(emailText))
                ).ToList();

                grdSearch.ItemsSource = filteredCustomers;
            }
            else
            {
                grdSearch.ItemsSource = new List<Customer>();
            }
        }

        private void OpenCustomer_Click(object sender, RoutedEventArgs e)
        {
            wCustomer customerWindow = new wCustomer(); // Giả sử wCustomer là tên của Window bạn muốn mở
            customerWindow.Show(); // Hiển thị cửa sổ wCustomer
        }


        private async void grdSearch_ButtonDelete_Click(object sender, RoutedEventArgs e)
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
                    this.LoadGrdSearchs();
                }
            }
        }

        private async void grdSearch_ButtonView_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button != null)
            {
                int customerId = (int)button.CommandParameter;
                var detailWindow = new wCustomerDetail(customerId);
                detailWindow.Show();
            }
        }


    }
}
