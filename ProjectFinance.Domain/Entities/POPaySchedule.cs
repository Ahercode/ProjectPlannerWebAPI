using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("POPaySchedule")]
public partial class POPaySchedule
{
    [Key]
    public int Id { get; set; }

    public int POId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    public DateOnly? Date { get; set; }

    [ForeignKey("POId")]
    [InverseProperty("POPaySchedules")]
    public virtual PurchaseOrder PO { get; set; } = null!;
}
