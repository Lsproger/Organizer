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
            Notes = db.Notes.Where(n => n.StudentId == u.IdStudent).ToList();
            stud = u.Student;
            this.Loaded += MainWindow_Loaded;
            _messages.Loaded += _messages_Loaded;
            _lessons.LostFocus += _lessons_LostFocus;
            ExistingNotesList.Loaded += ExistingNotesList_Loaded;
            ExistingNotesList.SelectionChanged += ExistingNotesList_SelectionChanged;
            _lessonsBox.Loaded += _lessonsBox_Loaded;
            _progressList.Loaded += _progressList_Loaded;
            AddNotificationsToAutorun();
        }

        private void AddNotificationsToAutorun()
        {
            // ткрываем нужную ветку в реестре   
            // @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\"  

            Microsoft.Win32.RegistryKey Key =
                Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
            string path = System.IO.Path.GetFullPath(@"Notifications\NotificationsOrganizer.exe");
            //добавляем первый параметр - название ключа  
            // Второй параметр - это путь к   
            // исполняемому файлу нашей программы.  
            Key.SetValue("NtOrg", "\"" + path+"\"");
            Key.Close();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigurateControlsIfNotStarosta();
            LoadTimeTableIfEmpty();
            _week.Loaded += _week_Loaded;
            _week.SelectionChanged += _week_SelectionChanged;
            _calendar.Loaded += _calendar_Loaded;
            _calendar.SelectedDatesChanged += _calendar_SelectedDatesChanged;

        }

        private void ConfigurateControlsIfNotStarosta()
        {
            if (!stud.IsStarosta)
            {
                _messageToDB.Foreground = Brushes.IndianRed;
                _messageToDB.Text = "Извиняйте, сообщения могут оствлять только админ и староста дабы избежать дичайшего спама))))";
                _messageToDB.IsReadOnly = true;
                _messageToDB.Cursor = Cursors.Arrow;
                SendMessageButton.Visibility = Visibility.Collapsed;
            }
        }

        #region TimeTable

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void _lessons_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Core.EntityException)
            { MessageBox.Show("Отсутствует подключение к сети интерент!"); }
        }

        private void LoadTimeTableIfEmpty()
        {
            try
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
            catch (System.Data.Entity.Core.EntityException) { MessageBox.Show("проверьте подключение к интернету"); }
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
            int dayStart = FirstSeptDay().DayOfYear - (int)FirstSeptDay().DayOfWeek + 1;//Номер понедельника в году в неделе с первым сентября
            if ( (DaysSinceStart(dayStart) / 7) % 2 ==0 )
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
            Notes = db.Notes.Where(n => n.StudentId == stud.IdStudent).ToList();
        }

        private void SaveNotes()
        {
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Core.EntityException) { MessageBox.Show("Проверьте подключение к интернету"); }
        }
        #endregion

        #region Messages
        private void _deleteMsg(object sender, RoutedEventArgs e)
        {
            try
            {
                using (OrgContext oc = new OrgContext())
                {
                    Message m = (_messages.SelectedItem as Message);
                    if (m.IdStudent == stud.IdStudent)
                    {
                        oc.Messages.Remove(oc.Messages.Find(m.MessId));
                        oc.SaveChanges();
                        _messages_Loaded(this, new RoutedEventArgs());
                    }
                    else MessageBox.Show("Вы можете удалять тоолько свои сообщения(для старост)");
                }
            }
            catch (System.Data.Entity.Core.EntityException) { MessageBox.Show("Проверьте подключение к интернету!"); }
        }

        private void _messages_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMessages();
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_messageToDB.Text != "" && stud.IsStarosta)
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
            catch (System.Data.Entity.Core.EntityException) { MessageBox.Show("Проверьте подключение к интернету!"); }

        }

        private void ClearMessageArea()
        {
            _messageToDB.Text = "";
        }

        private void LoadMessages()
        {
            try
            {
                db.Dispose();
                db = new OrgContext();
                db.Messages.Distinct().OrderByDescending(p => p.MessageDate).Where(m => m.IdStudent == stud.IdStudent || m.IdStudent == 1).Load();
                _messages.ItemsSource = db.Messages.Local;
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException) { MessageBox.Show("Потеряно соединение с интеренетом!"); }
        }


        #endregion

        #region Tasks
        private void DeleteProgress(Progress p)
        {
            try
            {
                using (OrgContext oc = new OrgContext())
                {
                    oc.Progresses.Remove(oc.Progresses.Find(new object[] { p.TaskId }));
                    oc.SaveChanges();
                }
            }
            catch (System.Data.Entity.Core.EntityException) { MessageBox.Show("Проверьте подключение к интернету"); }
        }

        private bool IsYesMessageBoxResult()
        {
            if (MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                return true;
            else return false;
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            Progress p = _progressList.SelectedItem as Progress;
            if (IsYesMessageBoxResult())
            {
                DeleteProgress(p);
                UpdateTasks();
            }
        }

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
            if (_lessonsBox.Items.Count == 1)
                NotificationMessgeToProgress.Content = "Для выбора предмета должно быть расписание:(";
        }

        private void _addProgress_Click(object sender, RoutedEventArgs e)
        {
            ClearNotification();
            try
            {
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
            catch (System.Data.Entity.Core.EntityException)
            {
                MessageBox.Show("Проверьте подключение к интернету!");
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
