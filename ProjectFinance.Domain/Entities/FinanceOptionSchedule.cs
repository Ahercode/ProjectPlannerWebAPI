using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("FinanceOptionSchedule")]
public partial class FinanceOptionSchedule
{
    [Key]
    public int Id { get; set; }

    public int? FinanceOptionId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Cost { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Repayment { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Disbursement { get; set; }

    public DateOnly? Date { get; set; }

    [ForeignKey("FinanceOptionId")]
    [InverseProperty("FinanceOptionSchedules")]
    public virtual FinanceOption? FinanceOption { get; set; }
}
