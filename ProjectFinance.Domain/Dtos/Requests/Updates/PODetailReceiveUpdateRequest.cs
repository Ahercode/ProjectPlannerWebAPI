namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class PODetailReceiveUpdateRequest
{
    public int Id { get; set; }

    public int? PODetailId { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public int? QunatityReceived { get; set; }
}