using Microsoft.AspNetCore.Http;

namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class ProjectDocumentUpdateRequest
{
    public int Id { get; set; }
    public int? ProjectId { get; set; }
    public string? DocUrl { get; set; }
    public string? Note { get; set; }
    public IFormFile? FileObject { get; set; }
}
