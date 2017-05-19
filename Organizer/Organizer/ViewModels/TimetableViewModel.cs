using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Organizer
{
    public class TimetableViewModel : INotifyPropertyChanged
    {
        OrgContext tt = new OrgContext();
        private TimeTable selectedTimeTable;

        public ObservableCollection<TimeTable> TimeTables { get; set; }
        public TimeTable SelectedTimeTable
        {
            get { return selectedTimeTable; }
            set
            {
                selectedTimeTable = value;
                OnPropertyChanged("SelectedTimeTable");
            }
        }

        public TimetableViewModel(Student u, string week)
        {
            tt.TimeTables.Local.Clear();
            tt.TimeTables.Where(p => p.IdGroup == u.IdGroup && p.Week == week).Load();
            TimeTables = tt.TimeTables.Local;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
