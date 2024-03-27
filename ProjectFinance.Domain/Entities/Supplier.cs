using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("Supplier")]
public partial class Supplier
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Code { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    [StringLength(10)]
    public string? Phone { get; set; }

    [InverseProperty("Supplier")]
    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();
}
