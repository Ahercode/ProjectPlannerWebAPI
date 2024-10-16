namespace ProjectFinance.Domain.Dtos.Requests;

public class CommonProjectActivityCostRequest
{
    public int Id { get; set; }

    public int? ProjectActivityId { get; set; }

    public int? CostDetailId { get; set; }
}