using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("PODetail")]
public partial class PODetail
{
    [Key]
    public int Id { get; set; }

    public int? CostDetailId { get; set; }

    public int? PurchaseOrderId { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    public DateOnly? Date { get; set; }
}
