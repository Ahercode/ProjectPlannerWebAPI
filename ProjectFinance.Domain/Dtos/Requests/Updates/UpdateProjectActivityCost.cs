namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateProjectActivityCost
{
    public int Id { get; set; }

    public int? ProjectActivityId { get; set; }

    public int? CostDetailId { get; set; }
}