namespace Organizer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Group()
        {
            Students = new HashSet<Student>();
            TimeTables = new HashSet<TimeTable>();
        }
        [RegularExpression(@"^[1-9]{1}[0]*$", ErrorMessage = "groupNumErr Группа: 1-10")]
        public int Group_numb { get; set; }

        [RegularExpression(@"^[1-4]{1}$", ErrorMessage = "courseErr Курс: 1-4")]
        public int Course { get; set; }

        [RegularExpression(@"^[1-2]{1}$", ErrorMessage = "subgrErr Подгруппа: 1 или 2")]
        public byte? Subgroup { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTable> TimeTables { get; set; }
    }
}
