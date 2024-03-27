using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("ProjectType")]
public partial class ProjectType
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Code { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [InverseProperty("ProjectType")]
    public virtual ICollection<Project> Projects { get; } = new List<Project>();
}
