namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class ContractorUpdateRequest
{
    public int id { get; set; }
    public string? name { get; set; }

    public string? email { get; set; }

    public string? address { get; set; }

    public string? phone { get; set; }

    public string? code { get; set; }
}