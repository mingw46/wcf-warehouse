using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfApp1.Models.Events;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ClientListScreen.xaml
    /// </summary>
  
    public partial class ClientListScreen : Window
    {
        public ClientListScreen()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        //private int number;

        //public int BoundNumber
        //{
        //    get { return number; }
        //    set
        //    {
        //        if (number != value)
        //        {                   
        //            number = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //  private List<Item> items = new List<Item>();

        //public List<ItemModel> Items
        //{
        //    get
        //    {
        //        using(var db = new DataContext())
        //        {
        //            return db.Items.Select(x => new ItemModel
        //            {
        //               ItemName = x.ItemName,
        //               ItemType = x.ItemType.ItemCode
        //            }).ToList();
        //        }
        //    }
        //}

        public List<Client> Clients
        {
            get
            {
                using (var db = new DataContext())
                {
                    return db.Clients.ToList();
                }
            }
        }



        //   public event PropertyChangedEventHandler PropertyChanged;

        //public Client SelectedClients
        //{
        //    get {
        //        if (currentSelectedPerson != null)
        //        {
        //            Console.WriteLine(currentSelectedPerson.ClientID.ToString());
        //        }
        //            return currentSelectedPerson;
        //    }
        //    set
        //    {
        //        currentSelectedPerson = value;
        //    //    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CurrentSelectedPerson"));
        //    }
        //}

        public delegate void SelectedEventHandler(object o, SelectedClientEventArgs e);
        public event SelectedEventHandler SelectedClientEvent;
        private Client currentSelectedClient;
        public Client SelectedClient {
            get
            {
                return currentSelectedClient;
            }
            set {
                currentSelectedClient = value;
                OnClientSelected(currentSelectedClient);
            }
        }

        protected virtual void OnClientSelected(Client client)
        {
            if (SelectedClientEvent != null) SelectedClientEvent(this, new SelectedClientEventArgs() {Client = client});
            this.Close();
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    BoundNumber = 0;
        //}
    }
  
}
