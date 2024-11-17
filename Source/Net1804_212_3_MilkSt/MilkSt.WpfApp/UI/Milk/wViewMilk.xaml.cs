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
    /// Interaction logic for wMilkDetail.xaml
    /// </summary>
    public partial class wViewMilk : Window
    {
        private readonly MilkBusiness _business;
        private int _milkId;

        public wViewMilk(int MilkId)
        {
            InitializeComponent();
            _business = new MilkBusiness();
            _milkId = MilkId;
            LoadMilkDetails();


        }
        private async void LoadMilkDetails()
        {
            var milkResult = await _business.GetById(_milkId);

            if (milkResult.Status > 0 && milkResult.Data != null)
            {
                var item = milkResult.Data as Milk;
                SetMilkDetails(item);
            }
            else
            {
                MessageBox.Show("Failed to load milk details.", "Error");
                Close();
            }
        }
        private void SetMilkDetails(Milk milk)
        {
            MilkId.Text = milk.MilkId.ToString();
            Name.Text = milk.Name;
            Brand.Text = milk.Brand;
            Price.Text = milk.Price.ToString();
            Discount.Text = milk.Discount.ToString();
            Description.Text = milk.Description;
            Expiry.Text = milk.Expiry.ToString();
            MilkType.Text = milk.MilkType;
            Weight.Text = milk.Weight.ToString();
            ManufactureIn.Text = milk.ManufactureIn.ToString();
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Button cancel = (Button)sender;
            Close();

        }
    }

}
