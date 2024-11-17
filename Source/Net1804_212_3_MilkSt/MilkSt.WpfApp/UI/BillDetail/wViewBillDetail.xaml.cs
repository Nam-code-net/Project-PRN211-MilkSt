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
    /// Interaction logic for wViewBillDetail.xaml
    /// </summary>
    public partial class wViewBillDetail : Window
    {
        public wViewBillDetail(BillDetail billDetail)
        {
            InitializeComponent();
            BillDetailIdTextBlock.Text = billDetail.BillDetailId.ToString();
            MilkIdTextBlock.Text = billDetail.MilkId.ToString();
            QuantityTextBlock.Text = billDetail.Quantity.ToString();
            PriceTextBlock.Text = billDetail.Price.ToString();
            AmountTextBlock.Text = billDetail.Amount.ToString();
            DiscountTextBlock.Text = billDetail.Discount.ToString();
            TaxTextBlock.Text = billDetail.Tax.ToString();
            TotalTextBlock.Text = billDetail.Total.ToString();
            DescriptionTextBlock.Text = billDetail.Description;
            StatusTextBlock.Text = billDetail.Status;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
