using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("ProjectDocument")]
public partial class ProjectDocument
{
    [Key]
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public string? DocUrl { get; set; }

    public string? Note { get; set; }
}
