namespace DanxAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminEmployee")]
    public partial class AdminEmployee
    {
        [Key]
        public int EmployeeId { get; set; }

        public int? AdminLvl { get; set; }

        public int? SalaryNumber { get; set; }

        [StringLength(50)]
        public string Manager { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLogin { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLogout { get; set; }

        public TimeSpan? StdHours { get; set; }

        public TimeSpan? WatchHours { get; set; }

        public TimeSpan? TotalHours { get; set; }

        public int? Vacationdays { get; set; }

        public int? Sickdays { get; set; }

        public int? WorkedDays { get; set; }
    }
}
