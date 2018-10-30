namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Email")]
    public partial class Email
    {
        public int EmailId { get; set; }

        [Column("Email")]
        [StringLength(255)]
        public string Email1 { get; set; }
    }

    class EmailComparer : IEqualityComparer<Email>
    {
        public bool Equals(Email e1, Email e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.Email1 == e2.Email1)
                return true;
            else
                return false;
        }

        public int GetHashCode(Email e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return e1.EmailId.GetHashCode();
        }
    }
}
