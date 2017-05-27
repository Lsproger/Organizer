using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Organizer.Entites
{
    public class Note
    {
        [Key]
        [Required]
        [Column(Order = 1)]
        public int StudentId { get; set; }

        [Key]
        [Required]
        [Column(Order = 0)]
        public string NoteDate { get; set; }

        [MaxLength(300)]
        public string NoteDescription { get; set; }
    }
}
