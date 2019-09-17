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
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ItemsScreen.xaml
    /// </summary>
    public partial class ItemsScreen : Window
    {
        public ItemsScreen()
        {
            InitializeComponent();
            
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var itemID = e.
        }

        private void CbCountryCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbCountryCode_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new DataContext())
            {
               
                cbItemType.ItemsSource = db.ItemTypes.Select(x => x.ItemCode).ToList();
            }
        }

        private void ItemsListView_Loaded(object sender, RoutedEventArgs e)
        {

            refreshListView();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var itemName = txtItemName.Text;
            var serialNumber = txtSerialNumber.Text;
            var itemType = cbItemType.SelectedValue.ToString();

            using(var db = new DataContext())
            {
                var selectedItem = db.ItemTypes.FirstOrDefault(x => x.ItemCode == itemType).ItemTypeID;
                Item item = new Item();
                item.ItemName = itemName;
                item.SerialNumber = serialNumber;
                item.ItemTypeID = selectedItem;
                item.CreationDate = DateTime.Now;
                db.Items.Add(item);
                db.SaveChanges();
               
            }
            MessageBox.Show("Dodano nowy produkt magazynowy.");
            refreshListView();
        }

        void refreshListView()
        {
            using (var db = new DataContext())
            {
                var query = db.Items.Select(x => new
                {
                    x.ItemID,
                    x.ItemName,
                    ArticleCode = x.ItemType.ItemCode,

                }).ToList();

                itemsListView.ItemsSource = query;
            }
        }

    }
}
