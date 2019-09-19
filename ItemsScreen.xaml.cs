using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfApp1.Models.Events;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ItemsScreen.xaml
    /// </summary>
    public partial class ItemsScreen : Window
    {
        private ObservableCollection<ItemModel> ItemsCollection = new ObservableCollection<ItemModel>();
        public ItemsScreen()
        {

            this.DataContext = this;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeItemCollection();
        }

        private void InitializeItemCollection()
        {
            using (var db = new DataContext())
            {

                var query = db.Items.Select(x => new ItemModel
                {
                    ItemID = x.ItemID,
                    ItemName = x.ItemName,
                    SerialNumber = x.SerialNumber,
                    ItemType = x.ItemType.ItemCode,
                    ClientName = x.Client.FirstName + " " + x.Client.LastName,
                    CreationDate = x.CreationDate,
                    OccupiedDate = x.OccupiedDate

                });
                ItemsCollection = query.ToObservableCollection();
            }
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

           // refreshListView();
        }


        private int ClientID { get; set; }
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (cbItemType.SelectedItem == null || cbItemType.SelectedIndex == -1)
            {
                MessageBox.Show("Nieprawidlowy kod artykulu");
                return;
            }
            var itemName = txtItemName.Text;
            var serialNumber = txtSerialNumber.Text;
            var itemType = cbItemType.SelectedValue.ToString();
            

            using (var db = new DataContext())
            {
                var selectedItem = db.ItemTypes.FirstOrDefault(x => x.ItemCode == itemType).ItemTypeID;
                Item item = new Item();
                item.ItemName = itemName;
                item.SerialNumber = serialNumber;
                item.ItemTypeID = selectedItem;
                item.CreationDate = DateTime.Now;
                item.ClientID = ClientID;
                db.Items.Add(item);
                db.SaveChanges();

            }
            MessageBox.Show("Dodano nowy produkt magazynowy.");
            
        }

        
        public ObservableCollection<ItemModel> ItemModels
        {
            get
            {
                
                return ItemsCollection;
            }
            set
            {
                ItemsCollection = value;
            }
        }

        private void ItemsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            var row = dg.SelectedItems[0];
            Console.WriteLine(row.ToString());
        }

        private void ItemsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
           //itemsDataGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void BtnSelectClient_Click(object sender, RoutedEventArgs e)
        {
            ClientListScreen clientListScreen = new ClientListScreen();
            clientListScreen.SelectedClientEvent += OnSelectClient;
            clientListScreen.Show();

        }

        public void OnSelectClient(object o, SelectedClientEventArgs e)
        {
            txtClient.Text = e.Client.FullName;
            ClientID = e.Client.ClientID;
        }

        private ItemModel currentSelectedItem;
        public ItemModel SelectedItem
        {
            get
            {
                return currentSelectedItem;
            }
            set
            {
                currentSelectedItem = value;
               // OnClientSelected(currentSelectedItem);
            }
        }

        private void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                ItemsCollection.Remove(SelectedItem);
                using (var db = new DataContext())
                {

                }
            }
        }

        
    }

    public static class CollectionUtility
    {
        public static ObservableCollection<T> ToObservableCollection<T>(
            this IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}