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
namespace MilkSt.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wVoucher.xaml
    /// </summary>
    public partial class wVoucher : Window
    {
        private readonly VoucherBusiness _business;
        public wVoucher()
        {
            InitializeComponent();
            this._business ??= new VoucherBusiness();
            SetDefaultDates();
            //this.LoadGrdVoucher();
        }
        public wVoucher(int id)
        {
            InitializeComponent();
            this._business ??= new VoucherBusiness();
            SetDefaultDates();
            this.GetVouchers(id);
        }

        private void SetDefaultDates()
        {
            DateTime currentDate = DateTime.Now;
            CreatedDate.Text = currentDate.ToString("d");
            UpdatedDate.Text = string.Empty;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int voucherIdtmp = -1;
                int.TryParse(VoucherId.Text, out voucherIdtmp);
                var item = await _business.GetById(voucherIdtmp);
                if (item.Data == null)
                {
                    var voucher = new Voucher()
                    {
                        Name = NameVoucher.Text,
                        Discount = (double)decimal.Parse(Discount.Text),
                        Expiry = DateOnly.Parse(Expiry.Text),
                        Quantity = int.Parse(Quantity.Text),
                        Description = Description.Text,
                        CreatedDate = DateOnly.FromDateTime(DateTime.Parse(CreatedDate.Text)),
                        UpdatedDate = null,
                        RedemptionCount = int.Parse(RedemptionCount.Text),
                        Status = Status.Text,
                    };

                    var result = await _business.Save(voucher);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var voucher = item.Data as Voucher;

                    voucher.Name = NameVoucher.Text;
                    voucher.Discount = (double)decimal.Parse(Discount.Text);
                    voucher.Expiry = DateOnly.Parse(Expiry.Text);
                    voucher.Quantity = int.Parse(Quantity.Text);
                    voucher.Description = Description.Text;
                    voucher.CreatedDate = DateOnly.FromDateTime(DateTime.Parse(CreatedDate.Text));
                    voucher.UpdatedDate = DateOnly.FromDateTime(DateTime.UtcNow);
                    voucher.RedemptionCount = int.Parse(RedemptionCount.Text);
                    voucher.Status = Status.Text;

                    var result = await _business.Update(voucher);
                    MessageBox.Show("Finish", "Success");
                }

                ClearForm();
                //this.LoadGrdVoucher();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ClearForm()
        {
            VoucherId.Text = string.Empty;
            NameVoucher.Text = string.Empty;
            Discount.Text = string.Empty;
            Expiry.Text = string.Empty;
            Quantity.Text = string.Empty;
            Description.Text = string.Empty;
            CreatedDate.Text = DateTime.Now.ToString("d");
            UpdatedDate.Text = string.Empty;
            RedemptionCount.Text = string.Empty;
            Status.Text = string.Empty;
        }

        /*private async void LoadGrdVoucher()
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
        }*/

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            var items = await _business.GetAll();
            if(items.Status > 0 && items.Data != null)
            {
                DataContext = items.Data as List<Voucher>;
                this.DialogResult = true;
                this.Close();
            }
            
        }

        /*private async void grdVoucher_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Voucher;
                    if (item != null)
                    {
                        var voucherResult = await _business.GetById(item.VoucherId);

                        if (voucherResult.Status > 0 && voucherResult.Data != null)
                        {
                            item = voucherResult.Data as Voucher;
                            VoucherId.Text = item.VoucherId.ToString();
                            NameVoucher.Text = item.Name;
                            Discount.Text = item.Discount.ToString();
                            Expiry.Text = item.Expiry.ToString("d");
                            Quantity.Text = item.Quantity.ToString();
                            Description.Text = item.Description.ToString();
                            CreatedDate.Text = item.CreatedDate.ToString("d");
                            UpdatedDate.Text = item.UpdatedDate.ToString();
                            RedemptionCount.Text = item.RedemptionCount.ToString();
                            Status.Text = item.Status.ToString();
                        }
                    }
                }
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
            voucherView.Show();
        }*/

        private async void GetVouchers(int id)
        {
            var result = await _business.GetById(id);
            if (result.Status > 0 && result.Data != null)
            {
                //this.LoadGrdVoucher();

                var item = result.Data as Voucher;
                VoucherId.Text = item.VoucherId.ToString();
                NameVoucher.Text = item.Name;
                Discount.Text = item.Discount.ToString();
                Expiry.Text = item.Expiry.ToString("d");
                Quantity.Text = item.Quantity.ToString();
                Description.Text = item.Description.ToString();
                CreatedDate.Text = item.CreatedDate.ToString("d");
                UpdatedDate.Text = item.UpdatedDate.ToString();
                RedemptionCount.Text = item.RedemptionCount.ToString();
                Status.Text = item.Status.ToString();
            }
            else
            {
                MessageBox.Show("Không thể tải chi tiết hóa đơn.");
            }
        }
    }
}
