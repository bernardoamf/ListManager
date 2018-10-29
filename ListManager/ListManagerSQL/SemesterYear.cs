namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SemesterYear")]
    public partial class SemesterYear 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SemesterYearId { get; set; }
    }
    
    class SemesterYearComparer : IEqualityComparer<SemesterYear>
    {
        public bool Equals(SemesterYear sm1, SemesterYear sm2)
        {
            if (sm1 == null && sm2 == null)
                return true;
            else if (sm1 == null || sm2 == null)
                return false;
            else if (sm1.SemesterYearId == sm2.SemesterYearId)
                return true;
            else
                return false;
        }

        public int GetHashCode(SemesterYear semesterYear)
        {
           // int hCode = semesterYear.SemesterYearId;
            return semesterYear.SemesterYearId;
        }
    }
    
}
