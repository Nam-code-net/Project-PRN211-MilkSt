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
    /// Interaction logic for wViewVoucher.xaml
    /// </summary>
    public partial class wViewVoucher : Window
    {
        public wViewVoucher(Voucher voucher)
        {
            InitializeComponent();

            VoucherId.Text = voucher.VoucherId.ToString();
            NameVoucher.Text = voucher.Name.ToString();
            Discount.Text = voucher.Discount.ToString();
            Expiry.Text = voucher.Expiry.ToString();
            Quantity.Text = voucher.Quantity.ToString();
            Description.Text = voucher.Description.ToString();
            CreatedDate.Text = voucher.CreatedDate.ToString();
            UpdatedDate.Text = voucher.UpdatedDate.ToString();
            RedemptionCount.Text = voucher.RedemptionCount.ToString();
            Status.Text = voucher.Status.ToString();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
