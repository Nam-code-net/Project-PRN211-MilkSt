using MilkSt.Business.Category;
using MilkSt.Data.Models;
using System.Windows;
using System.Windows.Controls;

namespace MilkSt.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wBillDetail.xaml
    /// </summary>
    public partial class wBillDetail : Window
    {
        private readonly BillDetailBusiness _business;
        private readonly MilkBusiness _milkBusiness;
        //public BillDetail BillDetail { get;set; }

        public wBillDetail()
        {
            InitializeComponent();
            this._business ??= new BillDetailBusiness();
            this._milkBusiness ??= new MilkBusiness();
            this.LoadGrdBillDetails();
        }

        public wBillDetail(int id)
        {
            InitializeComponent();
            this._business ??= new BillDetailBusiness();
            this._milkBusiness ??= new MilkBusiness();
            this.GetBillDetails(id);
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int billDetailIDTmp = -1;
                int.TryParse(BillDetailId.Text, out billDetailIDTmp);
                var item = await _business.GetById(billDetailIDTmp);

                int selectedMilkId = (int)SelectMilkId.SelectedValue;

                if (item.Data == null)
                {
                    var billDetail = new BillDetail()
                    {
                        //BillDetailId = int.Parse(BillDetailId.Text),
                        MilkId = selectedMilkId,
                        Quantity = int.Parse(Quantity.Text),
                        Price = (double)decimal.Parse(Price.Text),
                        Amount = (double)decimal.Parse(Amount.Text),
                        Discount = (double)decimal.Parse(Discount.Text),
                        Tax = (double)decimal.Parse(Tax.Text),
                        Total = (double)decimal.Parse(Total.Text),
                        Description = Description.Text,
                        Status = Status.Text,
                    };

                    var result = await _business.Save(billDetail);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var billDetail = item.Data as BillDetail;

                    billDetail.MilkId = selectedMilkId;
                    billDetail.Quantity = int.Parse(Quantity.Text);
                    billDetail.Price = (double)decimal.Parse(Price.Text);
                    billDetail.Amount = (double)decimal.Parse(Amount.Text);
                    billDetail.Discount = (double)decimal.Parse(Discount.Text);
                    billDetail.Tax = (double)decimal.Parse(Tax.Text);
                    billDetail.Total = (double)decimal.Parse(Total.Text);
                    billDetail.Description = Description.Text;
                    billDetail.Status = Status.Text;

                    var result = await _business.Update(billDetail);
                    MessageBox.Show(result.Message, "Save");
                }

                BillDetailId.Text = string.Empty;
                SelectMilkId.SelectedIndex = -1;
                Quantity.Text = string.Empty;
                Price.Text = string.Empty;
                Amount.Text = string.Empty;
                Discount.Text = string.Empty;
                Tax.Text = string.Empty;
                Total.Text = string.Empty;
                Description.Text = string.Empty;
                Status.Text = string.Empty;
                this.LoadGrdBillDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private async void grdBillDetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string billDetailId = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

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

        private async void LoadGrdBillDetails()
        {
            var result = await _business.GetAll();
            var milks = await _milkBusiness.GetAll();

            if (result.Status > 0 && result.Data != null && milks.Status > 0 && milks.Data != null)
            {
                grdBillDetail.ItemsSource = result.Data as List<BillDetail>;
                SelectMilkId.ItemsSource = milks.Data as List<Milk>;
            }
            else
            {
                grdBillDetail.ItemsSource = new List<BillDetail>();
            }
        }

        private async void GetBillDetails(int id)
        {
            var result = await _business.GetById(id);

            if (result.Status > 0 && result.Data != null)
            {
                this.LoadGrdBillDetails();

                var item = result.Data as BillDetail;

                BillDetailId.Text = item.BillDetailId.ToString();
                SelectMilkId.SelectedValue = item.MilkId.ToString();
                Quantity.Text = item.Quantity.ToString();
                Price.Text = item.Price.ToString();
                Amount.Text = item.Amount.ToString();
                Discount.Text = item.Discount.ToString();
                Tax.Text = item.Tax.ToString();
                Total.Text = item.Total.ToString();
                Description.Text = item.Description.ToString();
                Status.Text = item.Status.ToString();
                
            }
            else
            {
                MessageBox.Show("Không thể tải chi tiết hóa đơn.");
            }
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            var items = await _business.GetAll();

            if(items.Status > 0 && items.Data != null)
            {
                DataContext = items.Data as List<BillDetail>;
                this.DialogResult = true;
                this.Close();
            }
        }

        private async void grdBillDetail_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as BillDetail;
                    if (item != null)
                    {
                        var billDetailResult = await _business.GetById(item.BillDetailId);

                        if (billDetailResult.Status > 0 && billDetailResult.Data != null)
                        {
                            item = billDetailResult.Data as BillDetail;
                            BillDetailId.Text = item.BillDetailId.ToString();
                            SelectMilkId.SelectedValue = item.MilkId.ToString();
                            Quantity.Text = item.Quantity.ToString();
                            Price.Text = item.Price.ToString();
                            Amount.Text = item.Amount.ToString();
                            Discount.Text = item.Discount.ToString();
                            Tax.Text = item.Tax.ToString();
                            Total.Text = item.Total.ToString();
                            Description.Text = item.Description.ToString();
                            Status.Text = item.Status.ToString();
                        }
                    }
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
