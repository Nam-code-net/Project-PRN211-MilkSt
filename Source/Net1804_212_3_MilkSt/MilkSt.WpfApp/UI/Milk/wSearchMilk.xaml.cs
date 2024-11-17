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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace MilkSt.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class wSearchMilk : Window
    {
        private readonly MilkBusiness _business;
        public wSearchMilk()
        {

            InitializeComponent();
            this._business ??= new MilkBusiness();
            this.LoadGrdMilk();
        }
        private async void LoadGrdMilk()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdMilk.ItemsSource = result.Data as List<Milk>;
            }
            else
            {
                grdMilk.ItemsSource = new List<Milk>();
            }
        }
        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await _business.GetAll();  // Giả sử _business.GetAll() trả về tất cả khách hàng
            if (result.Status > 0 && result.Data != null)
            {
                string NameText = Name.Text.ToLower();
                string BrandText = Brand.Text.ToLower();
                string MilkTypeText = MilkType.Text.ToLower();
                string PriceText = Price.Text.ToLower();
                string DiscountText = Discount.Text.ToLower();
                var filteredMilks = (result.Data as List<Milk>).Where(c =>
                     (string.IsNullOrEmpty(NameText) || c.Name.ToLower().Contains(NameText)) &&
                    (string.IsNullOrEmpty(BrandText) || c.Brand.ToLower().Contains(BrandText)) &&
                    (string.IsNullOrEmpty(MilkTypeText) || c.MilkType.ToLower().Contains(MilkTypeText)) &&
                    (string.IsNullOrEmpty(PriceText) || c.Price.ToString().ToLower().Contains(PriceText)) &&
                    (string.IsNullOrEmpty(DiscountText) || c.Discount.ToString().ToLower().Contains(DiscountText))).ToList();
                grdMilk.ItemsSource = filteredMilks;
            }
            else
            {
                grdMilk.ItemsSource = new List<Milk>();
            }
        }
        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var p = new wMilk();
            p.Show();
        }
        private async void grdSearch_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            // Cast the sender to a DataGrid
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                // Get the selected row
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    // Get the Milk item from the selected row
                    var item = row.Item as Milk;
                    if (item != null)
                    {
                        try
                        {
                            // Fetch the milk details asynchronously
                            var milkResult = await _business.GetById(item.MilkId);

                            if (milkResult.Status > 0 && milkResult.Data != null)
                            {
                                // Update the item with the fetched milk details
                                item = milkResult.Data as Milk;
                                if (item != null)
                                {
                                    // Open the WMilk window and pass the milk details
                                    var p = new wMilk
                                    {
                                        Owner = this
                                    };

                                    
                                    p.SetMilkDetails(item);
                                    p.ShowDialog();
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            // Handle any exceptions that occurred during the data retrieval
                            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                }

            }

        }
        private async void grdMilk_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string milkID = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(milkID))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(milkID));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdMilk();
                }
            }
        }

        private async void grdMilk_ButtonView_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button != null)
            {
                int milkId = (int)button.CommandParameter;
                var detailWindow = new wViewMilk(milkId);
                detailWindow.Show();
            }
        }

    }
}
