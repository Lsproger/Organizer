using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Organizer.Entites
{
    public class Note : INotifyPropertyChanged
    {
        private string noteName, description;
        private DateTime lastDate, startDate;

        public string NoteName
        {
            get { return noteName; }
            set
            {
                noteName = value;
                OnPropertyChanged("NoteName");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public DateTime LastDate
        {
            get { return lastDate; }
            set
            {
                lastDate = value;
                OnPropertyChanged("LastDate");
            }
        }

        public DateTime StartDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
