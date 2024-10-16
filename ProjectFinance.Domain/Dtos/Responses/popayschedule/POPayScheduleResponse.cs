using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFinance.Domain.Dtos.Responses.popayschedule;

public class POPayScheduleResponse
{
    public int Id { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }
}