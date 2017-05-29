namespace NotificationsOrganizer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Note
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string NoteDate { get; set; }

        [StringLength(300)]
        public string NoteDescription { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }
    }
}
