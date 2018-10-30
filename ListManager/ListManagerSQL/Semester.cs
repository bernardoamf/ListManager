namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Semester")]
    public partial class Semester
    {
        [StringLength(50)]
        public string SemesterId { get; set; }

        [StringLength(50)]
        public string SemesterNameId { get; set; }

        public int? SemesterYearId { get; set; }
    }

    class SemesterComparer : IEqualityComparer<Semester>
    {
        public bool Equals(Semester e1, Semester e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.SemesterId == e2.SemesterId
                        && e1.SemesterNameId == e2.SemesterNameId
                        && e1.SemesterYearId == e2.SemesterYearId)
                return true;
            else
                return false;
        }

        public int GetHashCode(Semester e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.SemesterId).GetHashCode();
        }
    }
}
