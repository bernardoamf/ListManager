namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountId { get; set; }

        [StringLength(50)]
        public string CreatedSemesterId { get; set; }

        public int? ContactId { get; set; }
    }

    public partial class AccountHelper
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountId { get; set; }

        [StringLength(50)]
        public string CreatedSemesterId { get; set; }

        public string ContactEmail { get; set; }

        public int ContactEmailId { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }
    }

    class AccountComparer : IEqualityComparer<Account>
    {
        public bool Equals(Account e1, Account e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.AccountId == e2.AccountId 
                        && e1.CreatedSemesterId == e2.CreatedSemesterId 
                        && e1.ContactId == e2.ContactId)
                return true;
            else
                return false;
        }

        public int GetHashCode(Account e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return e1.AccountId;
        }
    }

    class AccountHelperComparer : IEqualityComparer<AccountHelper>
    {
        public bool Equals(AccountHelper e1, AccountHelper e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.AccountId == e2.AccountId
                        && e1.CreatedSemesterId == e2.CreatedSemesterId
                        && e1.ContactEmail == e2.ContactEmail
                        && e1.ContactFirstName == e2.ContactFirstName
                        && e1.ContactLastName == e2.ContactLastName)
                return true;
            else
                return false;
        }

        public int GetHashCode(AccountHelper e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.ContactEmail + e1.ContactFirstName + e1.ContactLastName).GetHashCode();
        }
    }
}
