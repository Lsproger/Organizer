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
using System.Data.Entity;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Windows.Controls.Ribbon;
using System.Data.Entity.Validation;
using Organizer.Entites;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace Organizer
{
    /// <summary>
    /// Логика взаимодействия для Authentication.xaml
    /// </summary>
    public partial class Authentication : Window
    {
        public Authentication()
        {
            InitializeComponent();
            _rememberUser.IsChecked = true;
            W_Authentication.Loaded += RestoreLogs;
        }

        private void _log_in_Click(object sender, RoutedEventArgs e)
        {
            bool isgood = false;
            using (OrgContext db = new OrgContext())
            {
                User u = new User();
                using (MD5 md5h = MD5.Create())
                {
                    u = new User { Login = _authLogin.Text, Password = GetMd5Hash(md5h, _authPassword.Password) };
                }

                foreach (var usr in db.Users)
                {
                    if (u.Login == usr.Login && u.Password == usr.Password)
                    {
                        if (_rememberUser.IsChecked == true)
                            CreateRestoringFile(_authLogin.Text, _authPassword.Password);
                        else
                            CreateRestoringFile("", "");
                        MainWindow wnd = new MainWindow(usr);
                        wnd.Show();
                        isgood = true;
                    }
                }
                if (!isgood)
                    MessageBox.Show("Логин или пароль введён неверно!");
                else this.Close();
            }
        }

        private void CreateRestoringFile(string login, string password)
        {
            XDocument doc = new XDocument(
                                    new XElement("Usr",
                                    new XAttribute("login", login),
                                    new XAttribute("password", password)));
            doc.Save(@"..\..\Resources\RestUsr.xml");
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void RestoreLogs(object sender, RoutedEventArgs e)
        {
            XDocument doc = XDocument.Load(@"..\..\Resources\RestUsr.xml");
            _authLogin.Text = doc.Root.Attribute("login").Value;
            _authPassword.Password = doc.Root.Attribute("password").Value;
        }

        private void _gotoRegistration_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
            this.Close();
        }
    }
}
