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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ItemsScreen itemsScreen = new ItemsScreen();
            itemsScreen.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new DataContext())
            {
                ticketsDataGrid.ItemsSource = db.Tickets
                    .Where(x => x.User.UserId == LoggedUser.UserId).ToList();
            }
        }

        public User LoggedUser { get; set; }

        private void BtnAddTicket_Click(object sender, RoutedEventArgs e)
        {
            TicketAddScreen ticketAddScreen = new TicketAddScreen();
            ticketAddScreen.Show();
        }
    }
}
