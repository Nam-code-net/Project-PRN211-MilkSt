using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace MilkSt.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for WMilk.xaml
    /// </summary>
    public partial class wMilk : Window
    {
        private readonly MilkBusiness _business;

        public wMilk()
        {
            InitializeComponent();
            _business ??= new MilkBusiness();
            //LoadGrdMilk();
        }

        public void SetMilkDetails(Milk item)
        {
            MilkId.Text = item.MilkId.ToString();
            Name.Text = item.Name;
            Brand.Text = item.Brand;
            Price.Text = item.Price.ToString();
            Discount.Text = item.Discount.ToString();
            Description.Text = item.Description;
            Expiry.Text = item.Expiry.ToString();
            MilkType.Text = item.MilkType;
            Weight.Text = item.Weight.ToString();
            ManufactureIn.Text = item.ManufactureIn.ToString();
        }



        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int MilkIDTmp = -1;
                int.TryParse(MilkId.Text, out MilkIDTmp);
                var item = await _business.GetById(MilkIDTmp);

                if (item.Data == null)
                {
                    var milk = new Milk()
                    {

                        Name = Name.Text,
                        Brand = Brand.Text,
                        Weight = (double)decimal.Parse(Weight.Text),
                        MilkType = MilkType.Text,
                        Expiry = DateOnly.Parse(Expiry.Text),
                        Description = Description.Text,
                        Price = (double)decimal.Parse(Price.Text),
                        Discount = (double)decimal.Parse(Discount.Text),
                        ManufactureIn = ManufactureIn.Text,
                    };

                    var result = await _business.Save(milk);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var milk = item.Data as Milk;

                    milk.Name = Name.Text;
                    milk.Brand = Brand.Text;
                    milk.Weight = (double)decimal.Parse(Weight.Text);
                    milk.MilkType = MilkType.Text;
                    milk.Expiry = DateOnly.Parse(Expiry.Text);
                    milk.Description = Description.Text;
                    milk.Price = (double)decimal.Parse(Price.Text);
                    milk.Discount = (double)decimal.Parse(Discount.Text);
                    milk.ManufactureIn = ManufactureIn.Text;

                    var result = await _business.Update(milk);
                    MessageBox.Show(result.Message, "Save");
                }

                MilkId.Text = string.Empty;
                Name.Text = string.Empty;
                Brand.Text = string.Empty;
                Weight.Text = string.Empty;
                Price.Text = string.Empty;
                Discount.Text = string.Empty;
                Description.Text = string.Empty;
                Expiry.Text = string.Empty;
                MilkType.Text = string.Empty;
                ManufactureIn.Text = string.Empty;
                //LoadGrdMilk();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        /*private async void LoadGrdMilk()
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

        private async void grdMilk_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Milk;
                    if (item != null)
                    {
                        var milkResult = await _business.GetById(item.MilkId);

                        if (milkResult.Status > 0 && milkResult.Data != null)
                        {
                            item = milkResult.Data as Milk;
                            MilkId.Text = item.MilkId.ToString();
                            Name.Text = item.Name;
                            Brand.Text = item.Brand;
                            Price.Text = item.Price.ToString();
                            Discount.Text = item.Discount.ToString();
                            Description.Text = item.Description;
                            Expiry.Text = item.Expiry.ToString();
                            MilkType.Text = item.MilkType;
                            Weight.Text = item.Weight.ToString();
                            ManufactureIn.Text = item.ManufactureIn.ToString();

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
                    LoadGrdMilk();
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
        }*/

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



    }
}