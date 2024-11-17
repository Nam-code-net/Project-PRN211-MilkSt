using Microsoft.Identity.Client.NativeInterop;
using MilkSt.Business.Category;
using MilkSt.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for wSearchBillDetail.xaml
    /// </summary>
    public partial class wSearchBillDetail : Window
    {
        private readonly BillDetailBusiness _business;

        public wSearchBillDetail()
        {
            InitializeComponent();
            this._business ??= new BillDetailBusiness();
            this.LoadGrdBillDetails();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string discount =Discount.Text;
            string tax = Tax.Text;
            string minTotal = MinTotal.Text;
            string maxTotal = MaxTotal.Text;
            string status = Status.Text;


            var searchResults = await _business.SearchByStatus(status, discount, tax, minTotal, maxTotal);
            if (searchResults.Status > 0 && searchResults.Data != null)
            {
                grdBillDetail.ItemsSource = searchResults.Data as List<BillDetail>;
            }
            else
            {
                MessageBox.Show("No result found!");
                grdBillDetail.ItemsSource = new List<BillDetail>();
            }
        }

        private async void ButtonAddNew_Click(object sender, RoutedEventArgs e)
        {
            var billDetail = new wBillDetail();
            var result = billDetail.ShowDialog();

            if (result.HasValue&&result == true)
            {
                LoadGrdBillDetails();
            }
        }

        private async void LoadGrdBillDetails()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdBillDetail.ItemsSource = result.Data as List<BillDetail>;
            }
            else
            {
                grdBillDetail.ItemsSource = new List<BillDetail>();
            }
        }

        private async void MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            var billDetail = grid.SelectedItem as BillDetail;
            if (billDetail != null)
            {
                var billDetailView = new wBillDetail(billDetail.BillDetailId);
                var result = billDetailView.ShowDialog();
                if (result.HasValue&&result == true)
                {
                    grdBillDetail.ItemsSource = billDetailView.DataContext as List<BillDetail>;
                }
            }
        }


        private async void grdBillDetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string billDetailId = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(billDetailId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(billDetailId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdBillDetails();
                }
            }
        }

        private void grdBillDetail_ButtonView_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var billDetail = button.DataContext as BillDetail;

            var billDetailView = new wViewBillDetail(billDetail);
            billDetailView.ShowDialog();
        }

    }
}
