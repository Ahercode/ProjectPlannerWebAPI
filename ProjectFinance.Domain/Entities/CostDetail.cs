using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

public partial class CostDetail
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Code { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public int? CostCategoryId { get; set; }

    [ForeignKey("CostCategoryId")]
    [InverseProperty("CostDetails")]
    public virtual CostCategory? CostCategory { get; set; }

    [InverseProperty("CostDetail")]
    public virtual ICollection<ProjectActivityCost> ProjectActivityCosts { get; set; } = new List<ProjectActivityCost>();
}
