using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Xml.Linq;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;

namespace NotificationsOrganizer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User u = new User();
        OrgContext db = new OrgContext();
        List<TimeTable> Labs = new List<TimeTable>();
        List<Note> Notes = new List<Note>();
        bool IsICanWork;
        string week;        

        public MainWindow()
        {
            InitializeComponent();
            SetWindowPosition();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetUser();
            LetItWork();
            if (u.Login != "admin")
                SetNotificationsUser();
            else SetNotificationAdmin();
        }

        private void SetNotificationsUser()
        {
            HelloLabel.Content = "Здравствуй, " + u.Student.Name + "!";
            SetNotificationLabs(Labs.Count);
            SetNotificationReccomendations(Labs.Count);
            SetNotificationNotes();
        }

        private void SetNotificationAdmin()
        {
            HelloLabel.Content = "Здравствуй, " + u.Student.Name + "!";
            LabsLabel.Visibility = Visibility.Collapsed;
            LabelReccomendations.Visibility = Visibility.Collapsed;
            SetNotificationNotes();
        }

        private void SetNotificationNotes()
        {
            DateTime d = DateTime.Now.Date;
            Notes = db.Notes.Where(n => n.StudentId == u.IdStudent).ToList();
            DeleteLastNotes();
            try
            {
                LabelNote1.Content = Notes[0].NoteDate + "   " + Notes[0].NoteDescription;
                LabelNote2.Content = Notes[1].NoteDate + "   " + Notes[1].NoteDescription;
                LabelNote3.Content = Notes[2].NoteDate + "   " + Notes[2].NoteDescription;
            }
            catch (NullReferenceException) { }
            catch (ArgumentNullException) { }
            catch (ArgumentOutOfRangeException) { }
        }

        private void DeleteLastNotes()
        {
            try
            {
                if (Convert.ToDateTime(Notes.First().NoteDate) < DateTime.Now.Date)
                {
                    Notes.Remove(Notes.First());
                    DeleteLastNotes();
                }
            }
            catch (ArgumentNullException) { }
            catch (InvalidOperationException) { }
        }

        private void SetNotificationReccomendations(int labsNumber)
        {
            switch (labsNumber)
            {
                case 0:
                    LabelReccomendations.Content = "Если не отдохнёте, потом будет некогда 😠";
                    break;
                case 1:
                    LabelReccomendations.Content = "Настоятельно рекомендую её сделать ☺☺☺";
                    break;
                default:
                    LabelReccomendations.Content = "Настоятельно рекомендую их сделать ☺☺☺";
                    break;
            }
        }

        private void SetNotificationLabs(int labsNumber)
        {
            string labi = "";
            foreach (TimeTable t in Labs)
            {
                if (t != Labs.Last())
                    labi += t.LessonName + ",";
                else labi += t.LessonName + ";";
            }
            switch (labsNumber)
            {
                case 0:
                    LabsLabel.Content = "Завтра у вас нет лаб, можете отдохнуть :3";
                    break;
                case 1:
                    LabsLabel.Content = "Завтра у вас " + Labs.Count + " лабараторная работа: " + "\r\n" + labi;
                    break;
                default:
                    LabsLabel.Content = "Завтра у вас " + Labs.Count + " лабы: " + "\r\n" + labi;
                    break;

            }
        }

        private void LetItWork()
        {
            if (u.Login != "admin")
            {
                week = CurrentWeek();
                LoadLabsOnTomorrow();
                IsICanWork = IsThereLabsOnTomorrow();
            }
            
        }

        private bool IsThereLabsOnTomorrow()
        {
            if (Labs.Count() == 0)
                return false;
            else return true;
        }

        private void LoadLabsOnTomorrow()
        {
            int tomorrow = (int)DateTime.Now.DayOfWeek + 1;
            try
            {
                Labs = db.TimeTables.Where(t => t.Week == week &&
                t.IdGroup == u.Student.IdGroup &&
                (t.Day) == tomorrow &&
                t.LessonType.ToLower() == "лр")
                .ToList();
            }
            catch (System.Data.DataException) { }
            finally { }
        }

        private string CurrentWeek()
        {
            int dayStart = FirstSeptDay().DayOfYear - (int)FirstSeptDay().DayOfWeek + 1;//Номер понедельника в году в неделе с первым сентября
            if ((DaysSinceStart(dayStart) / 7) % 2 == 0)
            {
                return "Первая";
            }
            else return "Вторая";
        }

        private int DaysSinceStart(int dayStart)
        {
            if (DateTime.Now.Month > 8)
                return DateTime.Now.DayOfYear - dayStart;
            else
                if (DateTime.IsLeapYear(FirstSeptDay().Year))
                return 366 - dayStart + DateTime.Now.DayOfYear;
            else
                return 365 - dayStart + DateTime.Now.DayOfYear;
        }

        private DateTime FirstSeptDay()
        {
            DateTime d = DateTime.Now;
            DateTime ds;
            if (d.Month < 9)
                ds = new DateTime(DateTime.Now.Year - 1, 9, 1);
            else
                ds = new DateTime(DateTime.Now.Year, 9, 1);
            return ds;
        }

        private void SetWindowPosition()
        {
            var primaryMonitorArea = SystemParameters.WorkArea;
            Left = primaryMonitorArea.Right - Width;
            Top = primaryMonitorArea.Bottom - Height - 30;
        }

        private void GetUser()
        {
            try
            {
                XDocument doc = XDocument.Load(@"Resources\RestUsr.xml");
                u =  db.Users.Find(new object[] { doc.Root.Attribute("login").Value });
                if (u.Login == null) GetUser();
            }
            catch (NullReferenceException) { IsICanWork = false; }
            catch (System.Data.DataException) { IsICanWork = false; }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GotoMainApplication_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"..\Organizer.exe");
        }
    }
}
