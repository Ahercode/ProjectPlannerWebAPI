using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("PurchaseOrder")]
public partial class PurchaseOrder
{
    [Key]
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public int? ActivityId { get; set; }

    public int? CostDetailId { get; set; }

    public int? SupplierId { get; set; }

    public DateOnly? Date { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [StringLength(50)]
    public string? PONumber { get; set; }

    [StringLength(15)]
    public string? Reference { get; set; }

    [InverseProperty("PurchaseOrder")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("PO")]
    public virtual ICollection<POPaySchedule> POPaySchedules { get; set; } = new List<POPaySchedule>();
}
