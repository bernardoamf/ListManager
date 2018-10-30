namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SemesterName")]
    public partial class SemesterName
    {
        [StringLength(50)]
        public string SemesterNameId { get; set; }
    }

    class SemesterNameComparer : IEqualityComparer<SemesterName>
    {
        public bool Equals(SemesterName e1, SemesterName e2)
        {
            if (e1 == null && e2 == null)
                return true;
            else if (e1 == null || e2 == null)
                return false;
            else if (e1.SemesterNameId == e2.SemesterNameId)
                return true;
            else
                return false;
        }

        public int GetHashCode(SemesterName e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.SemesterNameId).GetHashCode();
        }
    }
}
