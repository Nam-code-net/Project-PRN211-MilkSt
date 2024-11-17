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
    /// Interaction logic for wSearchBill.xaml
    /// </summary>
    public partial class wSearchBill : Window
    {
        private readonly BillBusiness _business;
        public wSearchBill()
        {
            InitializeComponent();
            this._business ??= new BillBusiness();
            this.LoadGrdBill();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string subTotal = SubTotal.Text;
            string discountAmount = DiscountAmount.Text;
            string totalAmount = TotalAmount.Text;
            string status = Status.Text;
        


            var searchResults = await _business.SearchByCriteria(subTotal, discountAmount, totalAmount, status);

            if (searchResults.Status > 0 && searchResults.Data != null)
            {
                grdBill.ItemsSource = searchResults.Data as List<Bill>;
            }
            else
            {
                MessageBox.Show("No results found for the search criteria.");
                grdBill.ItemsSource = new List<Bill>();
            }
        }

        private async void LoadGrdBill()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdBill.ItemsSource = result.Data as List<Bill>;
            }
            else
            {
                grdBill.ItemsSource = new List<Bill>();
            }
        }

        private void grdBill_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid grid)
            {
                var bill = grid.SelectedItem as Bill;
                if (bill != null)
                {
                    var billView = new wBill(bill.BillId);
                    billView.Show();
                }
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var s = new wBill();
            var result = s.ShowDialog();

            if (result.HasValue && result == true)
            {
                LoadGrdBill();
            }
        }
        private async void grdBill_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string billId = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(billId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(billId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdBill();
                }
            }
        }
        private async void grdBill_ButtonView_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var bill = button.DataContext as Bill;

            var billView = new wViewBill(bill);
            billView.Show();
        }

        private async void grdBill_ButtonBillDetail_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var bill = button.DataContext as Bill;
            int billDetailID = bill.BillDetailId ?? -1;
            if (billDetailID != -1)
            {
                var billView = new wBillDetail(billDetailID);
                billView.ShowDialog();
            }
            else
            {
                var billView = new wBillDetail();
                billView.ShowDialog();
            }

        }
    }
}
