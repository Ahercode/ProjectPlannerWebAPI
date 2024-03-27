using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("ProjectActivityCost")]
public partial class ProjectActivityCost
{
    [Key]
    public int Id { get; set; }

    public int? ProjectActivityId { get; set; }

    public int? CostDetailId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [ForeignKey("CostDetailId")]
    [InverseProperty("ProjectActivityCosts")]
    public virtual CostDetail? CostDetail { get; set; }

    [ForeignKey("ProjectActivityId")]
    [InverseProperty("ProjectActivityCosts")]
    public virtual ProjectActivity? ProjectActivity { get; set; }
}
