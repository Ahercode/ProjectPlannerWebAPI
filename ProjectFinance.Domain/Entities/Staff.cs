using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

public partial class Staff
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? StaffID { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? Surname { get; set; }

    [StringLength(50)]
    public string? OtherName { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    public string? Email { get; set; }

    [StringLength(10)]
    public string? Phone { get; set; }

    public string? Address { get; set; }

    [StringLength(20)]
    public string? GhanaCard { get; set; }
}
