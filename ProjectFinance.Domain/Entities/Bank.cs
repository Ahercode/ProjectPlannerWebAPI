using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("Bank")]
public partial class Bank
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Code { get; set; }

    [StringLength(80)]
    public string? Name { get; set; }

    [InverseProperty("Bank")]
    public virtual ICollection<FinanceOption> FinanceOptions { get; } = new List<FinanceOption>();
}
