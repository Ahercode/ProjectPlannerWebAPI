namespace ProjectFinance.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsActive { get; set; }
}