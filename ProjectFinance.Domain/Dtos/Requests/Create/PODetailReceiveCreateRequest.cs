namespace ProjectFinance.Domain.Dtos.Requests;

public class PODetailReceiveCreateRequest
{
    public int? PODetailId { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public int? QuantityReceived { get; set; }
}