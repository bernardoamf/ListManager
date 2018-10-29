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

        [StringLength(255)]
        public string SemesterId { get; set; }

        public int? LocationId { get; set; }

        [StringLength(9)]
        public string DayOfWeek { get; set; }

        public TimeSpan? Time { get; set; }
    }
}
