using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("Currency")]
public partial class Currency
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Code { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [InverseProperty("Currency")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
