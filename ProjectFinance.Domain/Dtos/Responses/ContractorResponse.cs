namespace ProjectFinance.Domain.Dtos.Responses;

public class ContractorResponse
{
    public int id { get; set; }
    public string? name { get; set; }

    public string? email { get; set; }

    public string? address { get; set; }

    public string? phone { get; set; }

    public string? code { get; set; }
}