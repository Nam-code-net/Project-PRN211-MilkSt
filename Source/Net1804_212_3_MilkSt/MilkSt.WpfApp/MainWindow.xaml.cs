using MilkSt.WpfApp.UI;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MilkSt.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_wSearchBillDetail_Click(object sender, RoutedEventArgs e)
        {
            var p = new wSearchBillDetail();
            p.Owner = this;
            p.Show();
        }

        private void Open_wSearchMilk_Click(object sender, RoutedEventArgs e)
        {
            var p = new wSearchMilk();
            p.Owner = this;
            p.Show();
        }

        private void Open_wSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            var p = new wSearch();
            p.Owner = this;
            p.Show();
        }

        private void Open_wSearchBill_Click(object sender, RoutedEventArgs e)
        {
            var p = new wSearchBill();
            p.Owner = this;
            p.Show();
        }

        private void Open_wSearchVoucher_Click(object sender, RoutedEventArgs e)
        {
            var p = new wSearchVoucher();
            p.Owner = this;
            p.Show();
        }
    }
}