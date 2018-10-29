namespace ListManager.ListManagerSQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enrollment")]
    public partial class Enrollment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnrollmentId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? StudentId { get; set; }

        [StringLength(50)]
        public string SemesterId { get; set; }
    }
}
