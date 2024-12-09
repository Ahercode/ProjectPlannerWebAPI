using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("PODetailReceive")]
public partial class PODetailReceive
{
    [Key]
    public int Id { get; set; }

    public int? PODetailId { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public int? QuantityReceived { get; set; }
}
