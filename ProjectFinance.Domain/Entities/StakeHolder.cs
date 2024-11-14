using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("StakeHolder")]
public partial class StakeHolder
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public string? Email { get; set; }

    [StringLength(100)]
    public string? Designation { get; set; }

    public string? Address { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    public int? ItemId { get; set; }
}
