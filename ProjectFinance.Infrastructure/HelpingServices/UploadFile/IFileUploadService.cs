using Microsoft.AspNetCore.Http;

namespace ProjectFinance.Infrastructure.HelpingServices.UploadFile;

public interface IFileUploadService
{
        Task<string> UploadFile(IFormFile? imageFile, string folderName);
        
        Task DeleteFile(string fileName, string folderName);
}