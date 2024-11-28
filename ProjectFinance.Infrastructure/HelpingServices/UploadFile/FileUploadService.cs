using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ProjectFinance.Infrastructure.HelpingServices.UploadFile;

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploadService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    
    public async Task<string> UploadFile(IFormFile? imageFile, string folderName)
    {
        var mes = "No file was selected";

        if (imageFile != null)
        {
            try
            {
                string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
                var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, $"Uploads/{folderName}", imageName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                return imageName;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        return mes;
    }

    public async Task DeleteFile(string fileName, string folderName)
    {
        var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, $"Uploads/{folderName}", fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}