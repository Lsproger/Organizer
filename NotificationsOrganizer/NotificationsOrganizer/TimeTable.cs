namespace NotificationsOrganizer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeTable")]
    public partial class TimeTable
    {
        public int Day { get; set; }

        public int LessonNumber { get; set; }

        [StringLength(10)]
        public string Auditorium { get; set; }

        public int? IdGroup { get; set; }

        [StringLength(50)]
        public string LessonName { get; set; }

        [StringLength(2)]
        public string LessonType { get; set; }

        [StringLength(10)]
        public string Week { get; set; }

        public int Id { get; set; }

        public virtual Group Group { get; set; }
    }
}
