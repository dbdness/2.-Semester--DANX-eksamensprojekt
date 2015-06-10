namespace DanxAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoggedInEmployee")]
    public partial class LoggedInEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLogin { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLogout { get; set; }

        public TimeSpan? TotalHours { get; set; }
    }
}
