
namespace Organizer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [Table("TimeTable")]
    public partial class TimeTable : INotifyPropertyChanged
    {
        private int day, lessonNumber;
        private string auditorium, lessonName, lessonType;

        public int Day
        {
            get
            {
                return day;
            }
            set
            {
                day = value;
                OnPropertyChanged("Day");
            }
        }

        public int LessonNumber
        {
            get { return lessonNumber; }
            set
            {
                lessonNumber = value;
                OnPropertyChanged("LessonNumber");
            }
        }

        [StringLength(10)]
        public string Auditorium
        {
            get { return auditorium; }
            set
            {
                auditorium = value;
                OnPropertyChanged("Auditorium");
            }
        }

        public int? IdGroup { get; set; }

        public int Id { get; set; }

        //[StringLength(50)]
        //public string LessonName { get; set; }

        //[StringLength(2)]
        //public string LessonType { get; set; }

        [StringLength(50)]
        public string LessonName
        {
            get { return lessonName; }
            set
            {
                lessonName = value;
                OnPropertyChanged("LessonName");
            }
        }

        [StringLength(2)]
        public string LessonType
        {
            get { return lessonType; }
            set
            {
                lessonType = value;
                OnPropertyChanged("LessonType");
            }
        }

        public string Week { get; set; }

        public virtual Group Group { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
