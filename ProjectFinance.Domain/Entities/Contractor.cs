using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("Contractor")]
public partial class Contractor
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string? name { get; set; }

    public string? email { get; set; }

    public string? address { get; set; }

    [StringLength(15)]
    public string? phone { get; set; }

    [StringLength(10)]
    public string? code { get; set; }

    [StringLength(5)]
    public string? yearlyrollover { get; set; }

    public int? maximumDays { get; set; }

    [InverseProperty("Contractor")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
