namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Teacher")]
    public partial class Teacher
    {
        public int TeacherId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }

    class TeacherComparer : IEqualityComparer<Teacher>
    {
        public bool Equals(Teacher e1, Teacher e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.Name == e2.Name)
                return true;
            else
                return false;
        }

        public int GetHashCode(Teacher e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.Name).GetHashCode();
        }
    }
}
