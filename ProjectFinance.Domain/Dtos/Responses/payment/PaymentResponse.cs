using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFinance.Domain.Dtos.Responses.payment;

public class PaymentResponse
{
    
    public int Id { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Date { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [StringLength(100)]
    public string? PayeeName { get; set; }

    [StringLength(20)]
    public string? InvoiceNumber { get; set; }
}