using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("FinanceOption")]
public partial class FinanceOption
{
    [Key]
    public int Id { get; set; }

    public string? Description { get; set; }

    [StringLength(20)]
    public string? OptionType { get; set; }

    public int? BankId { get; set; }

    [ForeignKey("BankId")]
    [InverseProperty("FinanceOptions")]
    public virtual Bank? Bank { get; set; }

    [InverseProperty("FinanceOption")]
    public virtual ICollection<FinanceOptionSchedule> FinanceOptionSchedules { get; set; } = new List<FinanceOptionSchedule>();
}
