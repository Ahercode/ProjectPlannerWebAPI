namespace ProjectFinance.Domain.Dtos.Requests;

public class ContractorCreateRequest
{
   
    public string? name { get; set; }

    public string? email { get; set; }

    public string? address { get; set; }

    public string? phone { get; set; }

    public string? code { get; set; }
}