namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateProjectActivity
{
    public int Id { get; set; }

    public int? ActivityId { get; set; }

    public int? ProjectId { get; set; }
}