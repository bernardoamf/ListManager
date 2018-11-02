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

        [StringLength(20)]
        public string SemesterId { get; set; }

        public int? LocationId { get; set; }

        [StringLength(9)]
        public string DayOfWeek { get; set; }

        [StringLength(30)]
        public string Time { get; set; }

        public int? TeacherId { get; set; }
    }

    public partial class ClassHelper
    {
        public int ClassId { get; set; }

        public string SemesterId { get; set; }

        public string LocationName { get; set; }

        public string DayOfWeek { get; set; }

        public string Time { get; set; }

        public string TeacherName { get; set; }
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
                        && e1.Time == e2.Time
                        && e1.TeacherId == e2.TeacherId)
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

    class ClassHelperComparer : IEqualityComparer<ClassHelper>
    {
        public bool Equals(ClassHelper e1, ClassHelper e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.SemesterId == e2.SemesterId
                        && e1.LocationName == e2.LocationName
                        && e1.DayOfWeek == e2.DayOfWeek
                        && e1.Time == e2.Time
                        && e1.TeacherName == e2.TeacherName)
                return true;
            else
                return false;
        }

        public int GetHashCode(ClassHelper e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.SemesterId + e1.LocationName + e1.DayOfWeek + e1.Time + e1.TeacherName).GetHashCode();
        }
    }
}
