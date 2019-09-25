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
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        readonly string registryPath = "Software\\Wow6432Node\\WpfApp1\\";
        public LoginScreen()
        {
            InitializeComponent();
            if (CheckRememberMeRegistry())
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(registryPath + "RememberMe");
                txtUsername.Text = key.GetValue("username").ToString();
                txtPassword.Password = key.GetValue("password").ToString();
                cbRememberMe.IsChecked = true;
                key.Close();
            }
        }

        public bool CheckRememberMeRegistry()
        {
            var key = "HKEY_CURRENT_USER\\" + registryPath + "RememberMe";
            if ((Microsoft.Win32.Registry.GetValue(key, "username", null) == null) || (Microsoft.Win32.Registry.GetValue(key, "username", null) == null))
            {
                return false;
            }
            return true;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var userName = txtUsername.Text;
            var userPassword = txtPassword.Password;
            var isRememberMe = cbRememberMe.IsChecked.Value;
            if (userName != String.Empty && userPassword != String.Empty)
            {
                using (DataContext db = new DataContext())
                {
                    try
                    {
                        var findUser = db.Users
                             .Where(x => x.Username == userName)
                             .First();


                            PasswordHash hash = new PasswordHash(findUser.PasswordHash);

                        if (!hash.Verify(userPassword))
                             throw new System.UnauthorizedAccessException();

                            if (isRememberMe == true)
                            {
                                if (!CheckRememberMeRegistry())
                                {
                                    Microsoft.Win32.RegistryKey key;
                                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(registryPath + "RememberMe");
                                    key.SetValue("username", txtUsername.Text);
                                    key.SetValue("password", txtPassword.Password);
                                }
                                findUser.RembemberMe = true;
                            }
                            else
                            {
                                if (CheckRememberMeRegistry())
                                {
                                    Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(registryPath + "RememberMe");
                                }
                                findUser.RembemberMe = false;
                            }

                            findUser.LastLoginDate = DateTime.Now;
                            

                            MessageBox.Show("Logged in");
                            MainWindow dashboard = new MainWindow();
                            dashboard.LoggedUser = findUser;
                            db.SaveChanges();

                            dashboard.Show();
                            this.Close();
                        
                        
                    }
                    catch
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Entered login or password cannot be empty.");
            }
        }

        private void TxtRegisterRedirect_Click(object sender, RoutedEventArgs e)
        {
            RegisterScreen registerScreen = new RegisterScreen();
            registerScreen.Show();
            this.Close();
        }
    }
}
