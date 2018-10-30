namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enrollment")]
    public partial class Enrollment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnrollmentId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? StudentId { get; set; }

        [StringLength(50)]
        public string SemesterId { get; set; }

        public int? AccountId { get; set; }
    }

    class EnrollmentComparer : IEqualityComparer<Enrollment>
    {
        public bool Equals(Enrollment e1, Enrollment e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.EnrollmentId == e2.EnrollmentId
                        && e1.CreateDate == e2.CreateDate
                        && e1.StudentId == e2.StudentId
                        && e1.SemesterId == e2.SemesterId
                        && e1.AccountId == e2.AccountId)
                return true;
            else
                return false;
        }

        public int GetHashCode(Enrollment e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return e1.EnrollmentId;
        }
    }
}
