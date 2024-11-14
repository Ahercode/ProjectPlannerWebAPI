using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("Payment")]
public partial class Payment
{
    [Key]
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [StringLength(100)]
    public string? PayeeName { get; set; }

    [StringLength(20)]
    public string? InvoiceNumber { get; set; }
}
