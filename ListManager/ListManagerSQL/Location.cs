namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Location")]
    public partial class Location
    {
        public int LocationId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
    }

    class LocationComparer : IEqualityComparer<Location>
    {
        public bool Equals(Location e1, Location e2)
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

        public int GetHashCode(Location e1)
        {
            // int hCode = semesterYear.SemesterYearId;
            return (e1.Name).GetHashCode();
        }
    }
}
