using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("CostCategory")]
public partial class CostCategory
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Code { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("CostCategory")]
    public virtual ICollection<CostDetail> CostDetails { get; set; } = new List<CostDetail>();
}
