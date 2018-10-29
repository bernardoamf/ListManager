namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Semester")]
    public partial class Semester
    {
        [StringLength(50)]
        public string SemesterId { get; set; }

        [StringLength(50)]
        public string SemesterNameId { get; set; }

        public int? SemesterYearId { get; set; }
    }
}
