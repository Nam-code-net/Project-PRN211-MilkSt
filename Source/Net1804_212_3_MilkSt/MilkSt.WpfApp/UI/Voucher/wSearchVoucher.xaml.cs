using MilkSt.Business.Category;
using MilkSt.Data.Models;
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
using System.Windows.Shapes;

namespace MilkSt.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wSearchVoucher.xaml
    /// </summary>
    public partial class wSearchVoucher : Window
    {
        private readonly VoucherBusiness _business;
        public wSearchVoucher()
        {
            InitializeComponent();
            this._business ??= new VoucherBusiness();
            this.LoadGrdVoucher();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = NameVoucher.Text;
            string discount = Discount.Text;
            string expiry = Expiry.Text;
            string quantity = Quantity.Text;

            var searchResults = await _business.SearchByCriteria(name, discount, expiry, quantity);

            if (searchResults.Status > 0 && searchResults.Data != null)
            {
                grdVoucher.ItemsSource = searchResults.Data as List<Voucher>;
            }
            else
            {
                MessageBox.Show("No results found for the search criteria.");
                grdVoucher.ItemsSource = new List<Voucher>();
            }
        }

        private async void LoadGrdVoucher()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdVoucher.ItemsSource = result.Data as List<Voucher>;
            }
            else
            {
                grdVoucher.ItemsSource = new List<Voucher>();
            }
        }

        private async void grdVoucher_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            if (sender is DataGrid grid)
            {
                var voucher = grid.SelectedItem as Voucher;
                if (voucher != null)
                {
                    var voucherView = new wVoucher(voucher.VoucherId);                 
                    var result = voucherView.ShowDialog();
                    if (result.HasValue && result == true)
                    {
                        grdVoucher.ItemsSource = voucherView.DataContext as List<Voucher>;
                    }
                }
            }
        }


        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var s = new wVoucher();
            var result = s.ShowDialog();

            if (result.HasValue && result == true)
            {
                LoadGrdVoucher();
            }
        }

        private async void grdVoucher_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string voucherId = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(voucherId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(voucherId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdVoucher();
                }
            }
        }
        private async void grdVoucher_ButtonView_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var voucher = button.DataContext as Voucher;

            var voucherView = new wViewVoucher(voucher);
            voucherView.ShowDialog();
        }

    }
}
