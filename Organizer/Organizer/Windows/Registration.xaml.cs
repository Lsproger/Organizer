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
                        Name = _name.Text + " " + _surname.Text,
                        IdGroup = Convert.ToInt32(_course.Text) * 1000 + Convert.ToInt32(_groupNum.Text) * 10 + Convert.ToInt32(_subGroup.Text),
                        IdStudent = Convert.ToInt32(_studID.Text)
                    };
                    usr = new User
                    {
                        Login = _login.Text,
                        Password = _password.Password,
                        IdStudent = Convert.ToInt32(_studID.Text)
                    };
                }

                else throw new ValidationException("Пароли не совпадают!");
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var contextStd = new ValidationContext(stud);
                var contextGrp = new ValidationContext(grup);
                var contextUsr = new ValidationContext(usr);
                results.Clear();

                if (!Validator.TryValidateObject(grup, contextGrp, results, true))
                {
                    foreach (var res in results)
                        throw new ValidationException(res.ErrorMessage);
                }
                results.Clear();
                if (!Validator.TryValidateObject(stud, contextStd, results, true))
                {
                    foreach (var res in results)

                        throw new ValidationException(res.ErrorMessage);
                }

                results.Clear();
                if (!Validator.TryValidateObject(usr, contextUsr, results, true))
                {
                    foreach (var res in results)
                        throw new ValidationException(res.ErrorMessage);
                }

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
    }
}
