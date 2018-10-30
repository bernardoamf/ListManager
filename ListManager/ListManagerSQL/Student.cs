namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public int StudentId { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? ContactId { get; set; }
    }

    class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student e1, Student e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.StudentId == e2.StudentId
                        && e1.FirstName == e2.FirstName
                        && e1.LastName == e2.LastName
                        && e1.BirthDate == e2.BirthDate
                        && e1.ContactId == e2.ContactId)
                return true;
            else
                return false;
        }

        public int GetHashCode(Student e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.StudentId).GetHashCode();
        }
    }
}
