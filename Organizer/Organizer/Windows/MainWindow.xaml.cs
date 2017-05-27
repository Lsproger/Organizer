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

namespace Organizer
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OrgContext db = new OrgContext();
        Student stud = new Student();
        List<Note> Notes = new List<Note>(); 
        
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(User u)
        {
            InitializeComponent();
            Notes = db.Notes.ToList();
            stud = u.Student;
            this.Loaded += MainWindow_Loaded;
            _messages.Loaded += _messages_Loaded;
            _lessonsBox.Loaded += _lessonsBox_Loaded;
            _lessons.LostFocus += _lessons_LostFocus;
            _progressList.Loaded += _progressList_Loaded;
            ExistingNotesList.Loaded += ExistingNotesList_Loaded;
            ExistingNotesList.SelectionChanged += ExistingNotesList_SelectionChanged;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _userName.Text = stud.Name;
            _userID.Text = stud.IdStudent.ToString();
            _otherInfo.Text = "Курс — " + stud.Group.Course + ", группа — " + stud.Group.Group_numb + "-" + stud.Group.Subgroup;
            LoadTimeTableIfEmpty();
            _week.Loaded += _week_Loaded;
            _week.SelectionChanged += _week_SelectionChanged;
            _calendar.Loaded += _calendar_Loaded;
            _calendar.SelectedDatesChanged += _calendar_SelectedDatesChanged;
        }

        #region TimeTable

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

        private void _week_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTT(((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString());
        }

        private void UpdateTT(string week)
        {
            db.Dispose();
            db = new OrgContext();
            db.TimeTables.Where(p => p.IdGroup == stud.IdGroup).Where(p => p.Week == week).Load();
            _lessons.ItemsSource = db.TimeTables.Local.ToBindingList();
            DataContext = new TimetableViewModel(stud, week);
        }

        private string CurrentWeek()
        {
            int end;
            int diaposone;
            DateTime firstSept = new DateTime(DateTime.Now.Year, 9, 1);
            end = DateTime.Now.DayOfYear;
            if (DateTime.Now.Month >= 9)
            {
                diaposone = end - firstSept.DayOfYear + (int)firstSept.DayOfWeek;
                if ((int)((diaposone + 7) / 7) % 2 == 0)
                {
                    return "Первая";
                }
                else return "Вторая";
            }
            else
            {
                diaposone = 365 - (firstSept.DayOfYear - end) - (int)firstSept.DayOfWeek;
                if ((diaposone / 7) % 2 == 0)
                {
                    return "Первая";
                }
                else return "Вторая";
            }
        }
        #endregion

        #region Notes

        private void ExistingNotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _calendar.SelectedDate = Convert.ToDateTime((ExistingNotesList.SelectedItem as Note).NoteDate);
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
            if (IsNoteOnDateExist(date))
            {
                AddNoteButton.IsEnabled = false;
                _noteText.IsEnabled = true;
                ExistingNotesList.SelectedItem = GetNoteOnDate(date);
            }
            else
            {
                AddNoteButton.IsEnabled = true;
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
            Note note= new Note { NoteDescription = "", NoteDate = ""};
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
                db.Notes.Find(new object[] { d.ToShortDateString(), stud.IdStudent}).NoteDescription = _noteText.Text;
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
            Note n = new Note { NoteDate = date.ToShortDateString(), NoteDescription = "Новая заметочка!" , StudentId = stud.IdStudent};
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
            db.Notes.Remove(note);
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
            Notes = db.Notes.ToList();
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
            db.Dispose();
            db = new OrgContext();
            db.Messages.Where(p => p.Student.Group.Course == stud.Group.Course && p.Student.Group.Group_numb == stud.Group.Group_numb).OrderByDescending(p => p.MessageDate).Load();
            _messages.ItemsSource = db.Messages.Local;
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
                db.Dispose();
                db = new OrgContext();
                db.Messages.Where(p => p.Student.Group.Course == stud.Group.Course && p.Student.Group.Group_numb == stud.Group.Group_numb).OrderByDescending(p => p.MessageDate).Load();
                _messages.ItemsSource = db.Messages.Local;
            }

        }
        #endregion

        #region Tasks

        private void _lessonsBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> progressSubjects = new List<string> { "Выберите предмет" };
            using (OrgContext db = new OrgContext())
            {
                progressSubjects.AddRange(db.TimeTables.Where(p => (p.Group.IdGroup == stud.IdGroup) && (p.LessonName != "")).OrderBy(p => p.LessonName).Select(p => p.LessonName).Distinct().ToList());
            }
            _lessonsBox.ItemsSource = progressSubjects;
            _lessonsBox.SelectedIndex = 0;
            SetNoElementsNotification();
        }

        private void SetExistElementNotification()
        {
                NotificationMessgeToProgress.Content = "Вы уже отслеживаете этот предмет";
        }

        private void SetNoElementsNotification()
        {
            if(_lessonsBox.Items.Count == 1)
                NotificationMessgeToProgress.Content = "Для выбора предмета должно быть расписание:(";
        }

        private void _addProgress_Click(object sender, RoutedEventArgs e)
        {
            ClearNotification();
            if (_lessonsBox.SelectedIndex != 0 && !IsProgressInProgressList(_lessonsBox.SelectedItem as string))
            {
                Progress prog = new Progress { CompletedTasks = 0, IdStudent = stud.IdStudent, LessonName = _lessonsBox.SelectedValue.ToString(), NeededTasks = 1, TaskProgress = 0 };
                using (OrgContext pr = new OrgContext())
                {
                    pr.Progresses.Add(prog);
                    pr.SaveChanges();
                    pr.Progresses.Where(p => p.IdStudent == stud.IdStudent).OrderBy(p => p.LessonName).Load();
                    _progressList.ItemsSource = pr.Progresses.Local;
                }
            }
        }

        private void ClearNotification()
        {
            NotificationMessgeToProgress.Content = "";
        }

        private bool IsProgressInProgressList(string pr)
        {
            foreach (Progress p in _progressList.Items)
            {
                if (pr == p.LessonName)
                {
                    SetExistElementNotification();
                    return true;
                }
            }
            ClearNotification();
            return false;
        }

        private void _progressList_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTasks();
        }

        private void NeededTasksPlus_Click(object sender, RoutedEventArgs e)
        {
            Progress pp = _progressList.SelectedItem as Progress;
            db.Progresses.Find(pp.TaskId).NeededTasks += 1;
            db.Progresses.Find(pp.TaskId).TaskProgress =
                (double)db.Progresses.Find(pp.TaskId).CompletedTasks /
                (double)db.Progresses.Find(pp.TaskId).NeededTasks * 100;
            db.SaveChanges();
            UpdateTasks();
        }

        private void NeededTasksMinus_Click(object sender, RoutedEventArgs e)
        {
            Progress pp = _progressList.SelectedItem as Progress;
            if (
                    pp.NeededTasks > 1 &&
                    pp.NeededTasks > pp.CompletedTasks
                )
            {
                db.Progresses.Find(pp.TaskId).NeededTasks -= 1;
                db.Progresses.Find(pp.TaskId).TaskProgress = (double)db.Progresses.Find(pp.TaskId).CompletedTasks / (double)db.Progresses.Find(pp.TaskId).NeededTasks * 100;
                db.SaveChanges();
                UpdateTasks();
            }
        }

        private void CompletedTasksPlus_Click(object sender, RoutedEventArgs e)
        {
            Progress pp = _progressList.SelectedItem as Progress;
            if (pp.CompletedTasks < pp.NeededTasks)
            {
                db.Progresses.Find(pp.TaskId).CompletedTasks += 1;
                db.Progresses.Find(pp.TaskId).TaskProgress = (double)db.Progresses.Find(pp.TaskId).CompletedTasks / (double)db.Progresses.Find(pp.TaskId).NeededTasks * 100;
                db.SaveChanges();
                UpdateTasks();
            }
        }

        private void CompletedTasksMinus_Click(object sender, RoutedEventArgs e)
        {
            Progress pp = _progressList.SelectedItem as Progress;
            if (pp.CompletedTasks > 0)
            {
                db.Progresses.Find(pp.TaskId).CompletedTasks -= 1;
                db.Progresses.Find(pp.TaskId).TaskProgress = (double)db.Progresses.Find(pp.TaskId).CompletedTasks / (double)db.Progresses.Find(pp.TaskId).NeededTasks * 100;
                db.SaveChanges();
                UpdateTasks();
            }
        }

        private void UpdateTasks()
        {
            using (OrgContext oc = new OrgContext())
            {
                oc.Progresses.Where(p => p.IdStudent == stud.IdStudent).OrderBy(p => p.LessonName).Load();
                _progressList.ItemsSource = oc.Progresses.Local;
            }
        }
        #endregion
    }
}
