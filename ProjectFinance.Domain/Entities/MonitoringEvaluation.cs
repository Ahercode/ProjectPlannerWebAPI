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

    [StringLength(100)]
    [Unicode(false)]
    public string? Stakeholder { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [ForeignKey("ActivityId")]
    [InverseProperty("MonitoringEvaluations")]
    public virtual Activity? Activity { get; set; }
}
