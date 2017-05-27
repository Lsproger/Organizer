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
        public string NoteDate { get; set; }

        public string NoteDescription { get; set; }
    }
}
