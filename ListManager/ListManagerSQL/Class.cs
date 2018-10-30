namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Class")]
    public partial class Class
    {
        public int ClassId { get; set; }

        [StringLength(255)]
        public string SemesterId { get; set; }

        public int? LocationId { get; set; }

        [StringLength(9)]
        public string DayOfWeek { get; set; }

        [StringLength(9)]
        public string Time { get; set; }
    }

    class ClassComparer : IEqualityComparer<Class>
    {
        public bool Equals(Class e1, Class e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.SemesterId == e2.SemesterId
                        && e1.LocationId == e2.LocationId
                        && e1.DayOfWeek == e2.DayOfWeek
                        && e1.Time == e2.Time)
                return true;
            else
                return false;
        }

        public int GetHashCode(Class e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.SemesterId + e1.LocationId + e1.DayOfWeek + e1.Time).GetHashCode();
        }
    }
}
