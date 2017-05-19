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
using System.Xml;
using System.Xml.Linq;

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
           
            using (OrgContext db = new OrgContext())
            {
                bool isgood = false;
                foreach (var usr in db.Users)
                {
                    if(_authLogin.Text == usr.Login && _authPassword.Password == usr.Password)
                    {
                        
                        {
                            if (_rememberUser.IsChecked == true)
                            {
                                XDocument doc = new XDocument(
                                    new XElement("Usr",
                                    new XAttribute("login", _authLogin.Text),
                                    new XAttribute("password", _authPassword.Password)));
                                doc.Save(@"..\..\Resources\RestUsr.xml");
                            }
                            else
                            {
                                XDocument doc = new XDocument(
                                    new XElement("Usr",
                                    new XAttribute("login", ""),
                                    new XAttribute("password", "")));
                                doc.Save(@"..\..\Resources\RestUsr.xml");
                            }
                        }
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
