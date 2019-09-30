using System;
using System.Linq;
using System.Data.Entity;
using System.Windows;
using WpfApp1.Models;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp1.Models.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ItemsScreen.xaml
    /// </summary>
    public partial class ItemsScreen : INotifyPropertyChanged
    {
        
        public ItemsScreen()
        {

           
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeItemCollection(null);
            this.DataContext = this;
        }

        private void InitializeItemCollection(int? itemID)
        {
            using (var db = new DataContext())
            {
                if (itemID == null)
                {
                    var query = db.Items.Select(x => new ItemModel()
                    {
                        ItemID = x.ItemID,
                        ItemName = x.ItemName,
                        SerialNumber = x.SerialNumber,
                        ItemType = x.ItemType.ItemCode,
                        ClientName = x.Client.FirstName + " " + x.Client.LastName,
                        CreationDate = x.CreationDate,
                        OccupiedDate = x.OccupiedDate
                    });
                    ItemModels = query.ToObservableCollection();
                }
                else
                {
                    var query = db.Items.Select(x => new ItemModel()
                    {
                        ItemID = x.ItemID,
                        ItemName = x.ItemName,
                        SerialNumber = x.SerialNumber,
                        ItemType = x.ItemType.ItemCode,
                        ClientName = x.Client.FirstName + " " + x.Client.LastName,
                        CreationDate = x.CreationDate,
                        OccupiedDate = x.OccupiedDate
                    }).Where(x => x.ItemID == itemID).FirstOrDefault();;
                    ItemModels.Add(query);
                }
            }
        }


        private void CbItemType_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new DataContext())
            {

                cbItemType.ItemsSource = db.ItemTypes.Select(x => x.ItemCode).ToList();
            }
        }



        private Client Client { get; set; }
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


            // await System.Threading.Tasks.Task.Run(() => AddNewItemTask(itemName, serialNumber, itemType));
            AddNewItemTask(itemName, serialNumber, itemType);
 
            //MessageBox.Show("Dodano nowy produkt magazynowy.");
            
        }

        public void AddNewItemTask(string itemName, string serialNumber, string itemType)
        {
           
            //string var = new InputBox("text").ShowDialog();
            using (var db = new DataContext())
            {
                if (SelectedItem == null)
                {
                    var selectedItem = db.ItemTypes.FirstOrDefault(x => x.ItemCode == itemType).ItemTypeID;
                    Item item = new Item();
                    item.ItemName = itemName;
                    item.SerialNumber = serialNumber;
                    item.ItemTypeID = selectedItem;
                    item.CreationDate = DateTime.Now;
                    if (Client != null)
                    {
                        item.ClientID = Client.ClientID;
                        SelectOccupiedDateBox inputBox = new SelectOccupiedDateBox();
                        inputBox.ShowDialog();
                        Console.WriteLine(inputBox.SelectedOccupiedDate);
                    }
                    db.Items.Add(item);
                    db.SaveChanges();
                    updateForm();
                    InitializeItemCollection(item.ItemID);
                    MessageBox.Show("Dodano nowy produkt magazynowy.");
                }
                else
                {

                        var selectedItem = db.ItemTypes.FirstOrDefault(x => x.ItemCode == itemType).ItemTypeID;
                        var item = db.Items.Where(x => x.ItemID == SelectedItem.ItemID).FirstOrDefault();
                        item.ItemName = itemName;
                        item.SerialNumber = serialNumber;
                        item.ItemTypeID = selectedItem;

                    if (Client != null)
                    {
                            item.ClientID = Client.ClientID;
                            SelectOccupiedDateBox inputBox = new SelectOccupiedDateBox();
                            inputBox.ShowDialog();
                        var x = inputBox.SelectedOccupiedDate;
                        Console.WriteLine(x);
                    }
                    else
                    { 
                        item.ClientID = null;
                        Nullable<DateTime> dt = null;
                        item.OccupiedDate = dt;
                    }

                    db.SaveChanges();
                        
                    ItemModel itemModel = new ItemModel();
                    itemModel.ItemID = item.ItemID;
                    itemModel.ItemName = item.ItemName;
                    itemModel.ItemType = item.ItemType.ItemCode;
                    itemModel.SerialNumber = item.SerialNumber;
                    itemModel.CreationDate = item.CreationDate;
                    if(item.Client != null)
                        itemModel.ClientName = item.Client.FullName;
                    var index = ItemsCollection.IndexOf(SelectedItem);
                    ItemsCollection.Remove(SelectedItem);
                    ItemsCollection.Insert(index, itemModel);
                    SelectedItem = null;
                    updateForm();
                    MessageBox.Show($"Pomyslnie edytowano artykuł:\nID: {itemModel.ItemID}\nNazwa artykułu: {itemModel.ItemName}");
                }

                
            }
            
        }
        private ObservableCollection<ItemModel> ItemsCollection = new ObservableCollection<ItemModel>();
        
        public ObservableCollection<ItemModel> ItemModels
        {
            get
            {
                
                return ItemsCollection;
            }
            set
            {
                ItemsCollection = value;
                NotifyPropertyChanged("Items");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
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
            Client = e.Client;
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
                NotifyPropertyChanged("Item");
            }
        }

        private void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {;
                using (var db = new DataContext())
                {

                    var item = db.Items.Where(x => x.ItemID == SelectedItem.ItemID).FirstOrDefault();
                    db.Items.Remove(item);
                    db.SaveChanges();
                }
                ItemModels.Remove(SelectedItem);
            }
        }

        private void BtnEditItem_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DataContext())
            {
                var item = db.Items.Where(x => x.ItemID == SelectedItem.ItemID).FirstOrDefault();
                updateForm(item);
            }
        }

        private void updateForm(Item item = null)
        {
            if (item == null)
            {               
                btnSubmit.Content = "Dodaj";
                btnCancel.Visibility = Visibility.Hidden;
                txtItemName.Text = "";
                txtSerialNumber.Text = "";
                txtClient.Text = "";
                cbItemType.SelectedValue = null;
                Client = null;
            }
            else
            {
                btnSubmit.Content = "Edytuj";
                btnCancel.Visibility = Visibility.Visible;
                txtItemName.Text = item.ItemName;
                txtSerialNumber.Text = item.SerialNumber;
                if (item.Client != null)
                    txtClient.Text = item.Client.FullName;
                else
                    txtClient.Text = "";
                cbItemType.SelectedValue = item.ItemType.ItemCode;
                
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            updateForm();
        }

        private void BtnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedItem != null)
            SelectedItem.ClientName = null;
            Client = null;
            txtClient.Text = "";
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