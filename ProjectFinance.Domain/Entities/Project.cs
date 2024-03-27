using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("Project")]
public partial class Project
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Code { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public int? ProjectTypeId { get; set; }

    public int? ProjectCategoryId { get; set; }

    public int? CurrencyId { get; set; }

    [Column(TypeName = "date")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }

    public int? ClientId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ContractSum { get; set; }

    public string? Note { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Projects")]
    public virtual Client? Client { get; set; }

    [ForeignKey("CurrencyId")]
    [InverseProperty("Projects")]
    public virtual Currency? Currency { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectActivity> ProjectActivities { get; } = new List<ProjectActivity>();

    [ForeignKey("ProjectCategoryId")]
    [InverseProperty("Projects")]
    public virtual ProjectCategory? ProjectCategory { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<ProjectSchedule> ProjectSchedules { get; } = new List<ProjectSchedule>();

    [ForeignKey("ProjectTypeId")]
    [InverseProperty("Projects")]
    public virtual ProjectType? ProjectType { get; set; }
}
