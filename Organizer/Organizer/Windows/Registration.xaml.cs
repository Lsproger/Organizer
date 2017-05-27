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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Organizer
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            ClearErrorLabels();
            try
            {
                Student stud;
                Group grup;
                User usr;
                if (_password.Password == _passwordReplied.Password)
                {
                    grup = new Group
                    {
                        IdGroup = Convert.ToInt32(_course.Text) * 1000 + Convert.ToInt32(_groupNum.Text) * 10 + Convert.ToInt32(_subGroup.Text),
                        Course = Convert.ToInt32(_course.Text),
                        Group_numb = Convert.ToInt32(_groupNum.Text),
                        Subgroup = Convert.ToByte(_subGroup.Text)
                    };
                    stud = new Student
                    {
                        Name = _name.Text,
                        IdGroup = Convert.ToInt32(_course.Text) * 1000 + Convert.ToInt32(_groupNum.Text) * 10 + Convert.ToInt32(_subGroup.Text),
                        IdStudent = Convert.ToInt32(_studID.Text)
                    };
                    using (MD5 md5h = MD5.Create())
                    {
                        usr = new User
                        {
                            Login = _login.Text,
                            Password = Authentication.GetMd5Hash(md5h, _password.Password),
                            IdStudent = Convert.ToInt32(_studID.Text)
                        };
                    }
                    if (!ValidateAll(usr, grup, stud)) throw new ValidationException("Ошибки в полях!");
                }
                else throw new ValidationException("Пароли не совпадают!");

                using (OrgContext db = new OrgContext())
                {
                    if (db.Groups.Where(g => g.IdGroup == grup.IdGroup).Count() == 0) db.Groups.Add(grup);
                    if (db.Students.Where(s => s.IdStudent == stud.IdStudent).Count() == 0) db.Students.Add(stud);
                    else throw new Exception("Студент с таким номером студ.билета уже заригестрирован!");
                    if (db.Users.Where(u => u.Login == usr.Login).Count() == 0) db.Users.Add(usr);
                    else throw new Exception("Этот логин уже занят!");
                    db.SaveChanges();
                }
                Authentication auth = new Authentication();
                auth.Show();
                this.Close();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidateStudent(Student s)
        {
            Regex r2 = new Regex(@"\w*Err");
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var cont = new ValidationContext(s);
            if (!Validator.TryValidateObject(s, cont, results, true))
            {
                foreach (var r in results)
                {
                    Label l = (Label)this.FindName(r2.Match(r.ErrorMessage).Value);
                    l.Content = r.ErrorMessage.Remove(0, r2.Match(r.ErrorMessage).Value.Count());
                }
                return false;
            }
            else return true;
        }

        private void ValidateGroup(Group g)
        {
            Regex r2 = new Regex(@"\w*Err");
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var cont = new ValidationContext(g);
            if (!Validator.TryValidateObject(g, cont, results, true))
            {
                foreach (var r in results)
                {
                    Label l = (Label)this.FindName(r2.Match(r.ErrorMessage).Value);
                    l.Content = r.ErrorMessage.Remove(0, r2.Match(r.ErrorMessage).Value.Count());
                }
            }
        }

        private void ValidateUser(User u)
        {
            Regex r2 = new Regex(@"\w*Err");
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var cont = new ValidationContext(u);
            if (!Validator.TryValidateObject(u, cont, results, true))
            {
                foreach (var r in results)
                {
                    Label l = (Label)this.FindName(r2.Match(r.ErrorMessage).Value);
                    l.Content = r.ErrorMessage.Remove(0, r2.Match(r.ErrorMessage).Value.Count());
                }
            }
        }

        private bool ValidateAll(User u, Group g, Student s)
        {
            ValidateUser(u);
            ValidateGroup(g);
            if (ValidateStudent(s)) return true;
            else return false;
        }

        private void ClearErrorLabels()
        {
            List<FrameworkElement> spisok = new List<FrameworkElement>();
            ChildControls(this, spisok);
            foreach (Label l in spisok)
            {
                if (Regex.IsMatch(l.Name, "Err")) (this.FindName(l.Name) as Label).Content = "";
            }

        }

        public void ChildControls(FrameworkElement elem, List<FrameworkElement> Controls)
        {
                foreach (FrameworkElement child in LogicalTreeHelper.GetChildren(elem))
                {
                    try
                    {
                        if (child is Label) Controls.Add(child);
                        if (elem.GetType() != typeof(RowDefinition)) ChildControls(child, Controls);
                    }
                    catch { }
                }
        }
    }
}
