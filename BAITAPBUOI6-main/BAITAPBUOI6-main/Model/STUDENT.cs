namespace Lab04_01.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("STUDENT")]
    public partial class STUDENT
    {
        [StringLength(20)]
        public string StudentID { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        public double? AverageScore { get; set; }

        public int? FacultyID { get; set; }

        public virtual FACULTY FACULTY { get; set; }
    }
}
