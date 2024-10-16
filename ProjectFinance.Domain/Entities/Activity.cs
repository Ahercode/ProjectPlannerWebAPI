using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("Activity")]
public partial class Activity
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Code { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("Activity")]
    public virtual ICollection<MonitoringEvaluation> MonitoringEvaluations { get; set; } = new List<MonitoringEvaluation>();

    [InverseProperty("Activity")]
    public virtual ICollection<ProjectActivity> ProjectActivities { get; set; } = new List<ProjectActivity>();
}
