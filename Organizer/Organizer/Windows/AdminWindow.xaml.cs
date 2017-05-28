using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using Organizer.Entites;

namespace Organizer.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        OrgContext db = new OrgContext();
        Student stud = new Student();
        List<Note> Notes = new List<Note>();

        public AdminWindow()
        {
            InitializeComponent();
        }

        public AdminWindow(User u)
        {
            InitializeComponent();
            Notes = db.Notes.Where(n=>n.StudentId == u.IdStudent).ToList();
            stud = u.Student;
            this.Loaded += AdminWindow_Loaded;
            _messages.Loaded += _messages_Loaded;
            _lessons.LostFocus += _lessons_LostFocus;
            ExistingNotesList.Loaded += ExistingNotesList_Loaded;
            ExistingNotesList.SelectionChanged += ExistingNotesList_SelectionChanged;
            StudentsGrid.LostFocus += StudentsGrid_LostFocus;
            StudentsGrid.Loaded += StudentsGrid_Loaded;
        }

        private void StudentsGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void StudentsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            db.Students.Where(s => s.Name != "Admin").Load();
            StudentsGrid.ItemsSource = db.Students.Local;
        }

        private void AdminWindow_Loaded(object sender, RoutedEventArgs e)
        {

            LoadTimeTableIfEmpty();
            _week.Loaded += _week_Loaded;
            _week.SelectionChanged += _week_SelectionChanged;
            _idGroup.SelectionChanged += _idGroup_SelectionChanged;
            _calendar.Loaded += _calendar_Loaded;
            _calendar.SelectedDatesChanged += _calendar_SelectedDatesChanged;

        }



        #region TimeTable
        private void _idGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTT((_week.SelectedItem as ComboBoxItem).Content.ToString());
        }

        private void _week_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTT(((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString());
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void _lessons_LostFocus(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void LoadTimeTableIfEmpty()
        {
            using (OrgContext tt = new OrgContext())
            {
                if (tt.TimeTables.Where(p => p.IdGroup == stud.IdGroup).Count() == 0)
                {
                    for (int i = 1; i < 7; i++)
                    {
                        for (int j = 1; j < 5; j++)
                        {
                            TimeTable t1 = new TimeTable { Day = i, LessonNumber = j, IdGroup = stud.IdGroup, Week = "Первая", Auditorium = "", LessonName = "", LessonType = "" };
                            TimeTable t2 = new TimeTable { Day = i, LessonNumber = j, IdGroup = stud.IdGroup, Week = "Вторая", Auditorium = "", LessonName = "", LessonType = "" };
                            tt.TimeTables.Add(t1);
                            tt.TimeTables.Add(t2);
                        }
                    }
                }
                tt.SaveChanges();
            }
        }

        private void _week_Loaded(object sender, RoutedEventArgs e)
        {
            SetWeek();
            LoadGroupsId();
        }

        private void SetWeek()
        {
            foreach (var el in _week.Items)
            {
                if ((el is ComboBoxItem))
                {
                    if ((el as ComboBoxItem).Content.ToString() == CurrentWeek())
                    {
                        (el as ComboBoxItem).IsSelected = true;
                        break;
                    }
                }
            }
        }

        private void LoadGroupsId()
        {
            List<int> idGroups = new List<int>();
            foreach (Group g in db.Groups)
            {
                if (g.IdGroup != 1)
                    idGroups.Add(g.IdGroup);
            }
            _idGroup.ItemsSource = idGroups;
            _idGroup.SelectedIndex = 0;
        }

      

        private void UpdateTT(string week)
        {
            db.Dispose();
            db = new OrgContext();
            int idGroup = Convert.ToInt32(_idGroup.SelectedItem);
            db.TimeTables.Where(p => p.IdGroup == idGroup).Where(p => p.Week == week).Load();
            _lessons.ItemsSource = db.TimeTables.Local.ToBindingList();
            DataContext = new TimetableViewModel(stud, week);
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
            {
                return DateTime.Now.DayOfYear - dayStart;
            }
            else
            {
                if (DateTime.IsLeapYear(FirstSeptDay().Year))
                    return 366 - dayStart + DateTime.Now.DayOfYear;
                else
                    return 365 - dayStart + DateTime.Now.DayOfYear;
            }
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

        #endregion

        #region Notes

        private void ExistingNotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _calendar.SelectedDate = Convert.ToDateTime((ExistingNotesList.SelectedItem as Note).NoteDate);
            }
            catch (NullReferenceException) { }
        }

        private void ExistingNotesList_Loaded(object sender, RoutedEventArgs e)
        {
            ExistingNotesList.ItemsSource = Notes;
        }

        private void _calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime d = Convert.ToDateTime(_calendar.SelectedDate);
            ConfigurateControlsViaDate(d);
            ShowNote(GetNoteOnDate(d));
            Mouse.Capture(null);
        }

        private void ConfigurateControlsViaDate(DateTime date)
        {
            ExistingNotesList.ItemsSource = Notes;
            if (IsNoteOnDateExist(date))
            {
                AddNoteButton.IsEnabled = false;
                DeleteNoteButton.IsEnabled = true;
                _noteText.IsEnabled = true;
                ExistingNotesList.SelectedItem = GetNoteOnDate(date);
            }
            else
            {
                AddNoteButton.IsEnabled = true;
                DeleteNoteButton.IsEnabled = false;
                _noteText.IsEnabled = false;
                _noteText.Text = "";
                ExistingNotesList.SelectedItem = null;
            }
        }

        private void ShowNote(Note n)
        {
            _noteText.Text = n.NoteDescription;
        }

        private Note GetNoteOnDate(DateTime d)
        {
            Note note = new Note { NoteDescription = "", NoteDate = "" };
            if (Notes.Find(n => n.NoteDate == d.ToShortDateString()) != null)
                note = Notes.Find(n => n.NoteDate == d.ToShortDateString());
            return note;
        }

        private void _calendar_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNotes();
            DateTime d = Convert.ToDateTime(_calendar.SelectedDate);
            ConfigurateControlsViaDate(d);
            ShowNote(GetNoteOnDate(d));
        }

        private void _noteText_LostFocus(object sender, RoutedEventArgs e)
        {
            DateTime d = Convert.ToDateTime(_calendar.SelectedDate);
            if (_noteText.Text != "")
                db.Notes.Find(new object[] { d.ToShortDateString(), stud.IdStudent }).NoteDescription = _noteText.Text;
            else
            {
                try
                {
                    DeleteNote();
                }
                catch (ArgumentException) { }
            }
            ReloadNotes();
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = Convert.ToDateTime(_calendar.SelectedDate);
            Note n = new Note { NoteDate = date.ToShortDateString(), NoteDescription = "Новая заметочка!", StudentId = stud.IdStudent };
            db.Notes.Add(n);
            ReloadNotes();
            ShowNote(n);
            ConfigurateControlsViaDate(date);
        }

        private void ReloadNotes()
        {
            SaveNotes();
            LoadNotes();
        }

        private void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteNote();
        }

        private void DeleteNote()
        {
            DateTime date = Convert.ToDateTime(_calendar.SelectedDate);
            Note note = Notes.Find(n => n.NoteDate == date.ToShortDateString());
            db.Notes.Remove(db.Notes.Find(new object[] { note.NoteDate, note.StudentId}));
            ReloadNotes();
            ConfigurateControlsViaDate(date);
        }

        private bool IsNoteOnDateExist(DateTime date)
        {
            if (Notes.Find(n => n.NoteDate == date.ToShortDateString()) != null) return true;
            else return false;
        }

        private void LoadNotes()
        {
            Notes = db.Notes.Where(n=>n.StudentId==stud.IdStudent).ToList();
        }

        private void SaveNotes()
        {
            db.SaveChanges();
        }
        #endregion

        #region Messages
        private void _deleteMsg(object sender, RoutedEventArgs e)
        {

            using (OrgContext oc = new OrgContext())
            {
                Message m = (_messages.SelectedItem as Message);

                MessageBox.Show("Chekai:" + m.IdStudent + " " + m.MessageDate + " " + m.MessageText);
                oc.Messages.Remove(oc.Messages.Find(m.MessId));
                oc.SaveChanges();
                _messages_Loaded(this, new RoutedEventArgs());
            }
        }

        private void _messages_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMessages();
        }

        private void LoadMessages()
        {
            db.Dispose();
            db = new OrgContext();
            db.Messages.Distinct().OrderByDescending(p => p.MessageDate).Load();
            _messages.ItemsSource = db.Messages.Local;
        }

        private void ClearMessageArea()
        {
            _messageToDB.Text = "";
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (_messageToDB.Text != "")
            {
                Message msg = new Message
                {
                    MessageText = _messageToDB.Text,
                    IdStudent = stud.IdStudent,
                    MessageDate = DateTime.Now
                };
                db.Messages.Add(msg);
                db.SaveChanges();
                LoadMessages();
                ClearMessageArea();
            }

        }
        #endregion
    }
}
