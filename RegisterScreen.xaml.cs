using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for RegisterScreen.xaml
    /// </summary>
    public partial class RegisterScreen : Window
    {
        public RegisterScreen()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var userName = txtUsername.Text;
            var email = txtEmail.Text;
            PasswordHash hash = new PasswordHash(txtPassword.Password);
            byte[] hashBytes = hash.ToArray();
            using(var db = new DataContext())
            {
                if (db.Users.Any(x => x.Username == userName || x.Email == email)) return;
               
                User user = new User();
                user.Username = userName;
                user.Email = email;
                user.PasswordHash = hashBytes;
                user.CreatedDate = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}
