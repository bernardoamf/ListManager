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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? ContactId { get; set; }
    }

    public class StudentHelper
    {
        public int StudentId { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? ContactId { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName{ get; set; }

        public string ContactEmail { get; set; }

        public int ContactEmailId { get; set; }
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

    class StudentHelperComparer : IEqualityComparer<StudentHelper>
    {
        public bool Equals(StudentHelper e1, StudentHelper e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.StudentId == e2.StudentId
                        && e1.FirstName == e2.FirstName
                        && e1.LastName == e2.LastName
                        && e1.BirthDate == e2.BirthDate
                        && e1.ContactFirstName == e2.ContactFirstName
                        && e1.ContactLastName == e2.ContactLastName
                        && e1.ContactEmail == e2.ContactEmail)
                return true;
            else
                return false;
        }

        public int GetHashCode(StudentHelper e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.FirstName + e1.LastName + e1.BirthDate.ToString() + e1.StudentId + e1.ContactFirstName + e1.ContactLastName + e1.ContactEmail).GetHashCode();
        }
    }
}
