namespace ProjectFinance.Domain.Dtos.Responses;

public class PODetailReceiveResponse
{
    public int Id { get; set; }

    public int? PODetailId { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public int? QunatityReceived { get; set; }
}