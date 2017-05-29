namespace NotificationsOrganizer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Progress
    {
        public int? NeededTasks { get; set; }

        public int? CompletedTasks { get; set; }

        [StringLength(50)]
        public string LessonName { get; set; }

        [Key]
        public int TaskId { get; set; }

        public double? TaskProgress { get; set; }

        public int IdStudent { get; set; }

        public virtual Student Student { get; set; }
    }
}
