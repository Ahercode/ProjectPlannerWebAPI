using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("ProjectSchedule")]
public partial class ProjectSchedule
{
    [Key]
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public DateOnly? Date { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [StringLength(30)]
    public string? InvoiceNumber { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectSchedules")]
    public virtual Project? Project { get; set; }
}
