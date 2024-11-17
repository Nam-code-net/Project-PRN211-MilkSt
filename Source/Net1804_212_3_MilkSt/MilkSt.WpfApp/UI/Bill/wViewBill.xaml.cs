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
    /// Interaction logic for wViewBill.xaml
    /// </summary>
    public partial class wViewBill : Window
    {
        public wViewBill(Bill bill)
        {
            InitializeComponent();

            BillId.Text = bill.BillId.ToString();
            BillDetailId.Text = bill.BillDetailId.ToString();
            CustomerId.Text = bill.CustomerId.ToString();
            Date.Text = bill.Date.ToString();
            SubTotal.Text = bill.SubTotal.ToString();
            VoucherId.Text = bill.VoucherId.ToString();
            DiscountAmount.Text = bill.DiscountAmount.ToString();
            TotalAmount.Text = bill.TotalAmount.ToString();
            Status.Text = bill.Status.ToString();
            Description.Text = bill.Description.ToString();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
