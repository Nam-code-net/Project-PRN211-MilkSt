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
    /// Interaction logic for wBill.xaml
    /// </summary>
    public partial class wBill : Window
    {
        private readonly BillBusiness _business;
        public wBill()
        {
            InitializeComponent();
            this._business = new BillBusiness();
            this.LoadGrdBill();
        }

        public wBill(int id)
        {
            InitializeComponent();
            this._business = new BillBusiness();
            this.GetBills(id);
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int billIDTmp = -1;
                int.TryParse(BillID.Text, out billIDTmp);
                var item = await _business.GetById(billIDTmp);


                int? billDetailID = null;
                int? voucherID = null;
                if (!string.IsNullOrEmpty(BillDetailID.Text))
                {
                    billDetailID = int.Parse(BillDetailID.Text);
                }

                if (!string.IsNullOrEmpty(VoucherID.Text))
                {
                    voucherID = int.Parse(VoucherID.Text);
                }
                if (item.Data == null)
                {
                    var bill = new Bill()
                    {
                        //BillId = int.Parse(BillID.Text),
                        BillDetailId = billDetailID,
                        CustomerId = int.Parse(CustomerID.Text),
                        Date = DateOnly.Parse(Date.Text),
                        SubTotal = (double)decimal.Parse(SubTotal.Text),
                        VoucherId = voucherID,
                        DiscountAmount = (double)decimal.Parse(DiscountAmount.Text),
                        TotalAmount = (double)decimal.Parse(TotalAmount.Text),
                        Status = Status.Text,
                        Description = Description.Text,
                    };

                    var result = await _business.Save(bill);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var bill = item.Data as Bill;
                    bill.BillId = int.Parse(BillID.Text);
                    bill.BillDetailId = billDetailID;
                    bill.CustomerId = int.Parse(CustomerID.Text);
                    bill.Date = DateOnly.Parse(Date.Text);
                    bill.SubTotal = (double)decimal.Parse(SubTotal.Text);
                    bill.VoucherId = voucherID;
                    bill.DiscountAmount = (double)decimal.Parse(DiscountAmount.Text);
                    bill.TotalAmount = (double)decimal.Parse(TotalAmount.Text);
                    bill.Status = Status.Text;
                    bill.Description = Description.Text;

                    var result = await _business.Update(bill);
                    MessageBox.Show(result.Message, "Save");
                }

                BillID.Text = string.Empty;
                BillDetailID.Text = string.Empty;
                CustomerID.Text = string.Empty;
                Date.Text = string.Empty;
                SubTotal.Text = string.Empty;
                VoucherID.Text = string.Empty;
                DiscountAmount.Text = string.Empty;
                TotalAmount.Text = string.Empty;
                Status.Text = string.Empty;
                Description.Text = string.Empty;

                this.LoadGrdBill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void grdBill_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Bill;
                    if (item != null)
                    {
                        var billResult = await _business.GetById(item.BillId);

                        if (billResult.Status > 0 && billResult.Data != null)
                        {
                            item = billResult.Data as Bill;
                            BillID.Text = item.BillId.ToString();
                            BillDetailID.Text = item.BillDetailId.ToString();
                            CustomerID.Text = item.CustomerId.ToString();
                            Date.Text = item.Date.ToString();
                            SubTotal.Text = item.SubTotal.ToString();
                            VoucherID.Text = item.VoucherId.ToString();
                            DiscountAmount.Text = item.DiscountAmount.ToString();
                            TotalAmount.Text = item.TotalAmount.ToString();
                            Status.Text = item.Status.ToString();
                            Description.Text = item.Description.ToString();
                        }
                    }
                }
            }
        }


        private async void grdBill_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;

            string BillID = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(BillID))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(BillID));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdBill();
                }
            }
        }

        private async void GetBills(int id)
        {
            var billResult = await _business.GetById(id);
            if (billResult.Status > 0 && billResult.Data != null)
            {
                this.LoadGrdBill();


                var item = billResult.Data as Bill;
                BillID.Text = item.BillId.ToString();
                BillDetailID.Text = item.BillDetailId.ToString();
                CustomerID.Text = item.CustomerId.ToString();
                Date.Text = item.Date.ToString();
                SubTotal.Text = item.SubTotal.ToString();
                VoucherID.Text = item.VoucherId.ToString();
                DiscountAmount.Text = item.DiscountAmount.ToString();
                TotalAmount.Text = item.TotalAmount.ToString();
                Status.Text = item.Status.ToString();
                Status.Text = item.Status.ToString();
                Description.Text = item.Description.ToString();
            }
            else
            {
                MessageBox.Show("Không thể tải chi tiết hóa đơn.");
            }
        }

        private void grdBill_ButtonView_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var bill = button.DataContext as Bill;

            var billView = new wViewBill(bill);
            billView.ShowDialog();
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
