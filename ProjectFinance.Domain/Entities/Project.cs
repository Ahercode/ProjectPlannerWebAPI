﻿using System;
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

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? ClientId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ContractSum { get; set; }

    public string? Note { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    public string? Location { get; set; }

    public int? ContractorId { get; set; }

    public int? ProjectPriority { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Projects")]
    public virtual Client? Client { get; set; }

    [ForeignKey("ContractorId")]
    [InverseProperty("Projects")]
    public virtual Contractor? Contractor { get; set; }

    [ForeignKey("CurrencyId")]
    [InverseProperty("Projects")]
    public virtual Currency? Currency { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectActivity> ProjectActivities { get; set; } = new List<ProjectActivity>();

    [ForeignKey("ProjectCategoryId")]
    [InverseProperty("Projects")]
    public virtual ProjectCategory? ProjectCategory { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<ProjectSchedule> ProjectSchedules { get; set; } = new List<ProjectSchedule>();

    [ForeignKey("ProjectTypeId")]
    [InverseProperty("Projects")]
    public virtual ProjectType? ProjectType { get; set; }
}
