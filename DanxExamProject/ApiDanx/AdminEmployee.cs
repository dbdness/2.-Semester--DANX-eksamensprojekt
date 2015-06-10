namespace ApiDanx
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
        [Column(Order = 0)]
        public int EmployeeId { get; set; }

        public int? AdminLvl { get; set; }

        public int? SalaryNumber { get; set; }

        [StringLength(50)]
        public string Manager { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? LastLogout { get; set; }

        public TimeSpan? StdHours { get; set; }

        public TimeSpan? WatchHours { get; set; }

        public TimeSpan? TotalHours { get; set; }

        public int? VacationDays { get; set; }

        public int? SickDays { get; set; }

        public int? WorkedDays { get; set; }
    }
}
