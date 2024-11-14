using System.ComponentModel.DataAnnotations;

namespace ProjectFinance.Domain.Dtos.Responses.activity;

public class ActivityResponse
{
    public int Id { get; set; }

    [StringLength(10)]
    public string? Code { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }
    
    public string? Description { get; set; }
}