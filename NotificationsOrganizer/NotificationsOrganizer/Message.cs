namespace NotificationsOrganizer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message
    {
        public int IdStudent { get; set; }

        [Required]
        [StringLength(500)]
        public string MessageText { get; set; }

        [Key]
        public int MessId { get; set; }

        public DateTime? MessageDate { get; set; }

        public virtual Student Student { get; set; }
    }
}
