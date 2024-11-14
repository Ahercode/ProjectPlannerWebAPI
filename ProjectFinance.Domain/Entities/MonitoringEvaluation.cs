using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("MonitoringEvaluation")]
public partial class MonitoringEvaluation
{
    [Key]
    public int Id { get; set; }

    public int? ActivityId { get; set; }

    [StringLength(30)]
    public string? Email { get; set; }

    public string? Note { get; set; }

    [StringLength(15)]
    public string? workDone { get; set; }

    public int? ProjectId { get; set; }

    [ForeignKey("ActivityId")]
    [InverseProperty("MonitoringEvaluations")]
    public virtual Activity? Activity { get; set; }
}
