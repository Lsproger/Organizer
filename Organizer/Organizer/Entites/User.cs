namespace Organizer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[\S]{6,}$",ErrorMessage = "passErr Пароль от 6 символов без пробелов!")]
        public string Password { get; set; }

        public int IdStudent { get; set; }

        public virtual Student Student { get; set; }
    }
}
