namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public int ContactId { get; set; }

        public int? EmailId { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }
    }

    public partial class ContactHelper
    {
        public int ContactId { get; set; }

        public string Email { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }
    }
    class ContactComparer : IEqualityComparer<Contact>
    {
        public bool Equals(Contact e1, Contact e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.EmailId == e2.EmailId
                        && e1.FirstName == e2.FirstName
                        && e1.LastName == e2.LastName)
                return true;
            else
                return false;
        }

        public int GetHashCode(Contact e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.EmailId + e1.FirstName + e1.LastName).GetHashCode();
        }
    }

    class ContactHelperComparer : IEqualityComparer<ContactHelper>
    {
        public bool Equals(ContactHelper e1, ContactHelper e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.Email == e2.Email
                        && e1.FirstName == e2.FirstName
                        && e1.LastName == e2.LastName)
                return true;
            else
                return false;
        }

        public int GetHashCode(ContactHelper e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.Email + e1.FirstName + e1.LastName).GetHashCode();
        }
    }

}
