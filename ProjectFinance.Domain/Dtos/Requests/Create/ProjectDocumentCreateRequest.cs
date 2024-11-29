using Microsoft.AspNetCore.Http;

namespace ProjectFinance.Domain.Dtos.Requests;

public class ProjectDocumentCreateRequest
{
    public int? ProjectId { get; set; }
    public string? DocUrl { get; set; }
    
    public string? Note { get; set; }
    public IFormFile? FileObject { get; set; }
}