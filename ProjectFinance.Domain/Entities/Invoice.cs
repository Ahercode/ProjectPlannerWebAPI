using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("Invoice")]
public partial class Invoice
{
    [Key]
    public int Id { get; set; }

    public int? SupplierId { get; set; }

    public int? PurchaseOrderId { get; set; }

    [StringLength(50)]
    public string? InvoiceNumber { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    public DateOnly? DueDate { get; set; }

    public int? ProjectId { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("Invoices")]
    public virtual Project? Project { get; set; }

    [ForeignKey("PurchaseOrderId")]
    [InverseProperty("Invoices")]
    public virtual PurchaseOrder? PurchaseOrder { get; set; }

    [ForeignKey("SupplierId")]
    [InverseProperty("Invoices")]
    public virtual Supplier? Supplier { get; set; }
}
