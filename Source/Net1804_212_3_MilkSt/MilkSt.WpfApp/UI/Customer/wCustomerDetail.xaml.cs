using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net;
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
    /// Interaction logic for wCustomerDetail.xaml
    /// </summary>
    public partial class wCustomerDetail : Window
    {

        private readonly CustomerBusiness _business;
        private int _customerId;

        public wCustomerDetail(int CustomerId)
        {
            InitializeComponent();
            _business = new CustomerBusiness();
            _customerId = CustomerId;
            LoadCustomerDetails();


        }
        private async void LoadCustomerDetails()
        {
            var customerResult = await _business.GetById(_customerId);

            if (customerResult.Status > 0 && customerResult.Data != null)
            {
                var item = customerResult.Data as Customer;
                SetCustomerDetails(item);
            }
            else
            {
                MessageBox.Show("Failed to load milk details.", "Error");
                this.Close();
            }
        }
        private void SetCustomerDetails(Customer item)
        {
            CustomerId.Text = item.CustomerId.ToString();
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

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

        private void CustomerId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}


