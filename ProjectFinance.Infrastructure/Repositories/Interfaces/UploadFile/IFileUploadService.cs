using Microsoft.AspNetCore.Http;

namespace ProjectFinance.Infrastructure.Repositories.Interfaces.UploadFile;

public interface IFileUploadService
{
        Task<string> UploadFile(IFormFile? imageFile, string folderName);
}