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
}
