using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.HelpingServices.UploadFile;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ProjectDocsController : BaseController
{
    private readonly IFileUploadService _fileUploadService;
    // private 
    //

    public ProjectDocsController(IUnitOfWork unitOfWork, IMapper mapper, IFileUploadService fileUploadService) : base(unitOfWork, mapper)
    {
        _fileUploadService = fileUploadService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllProjectDocs()
    {
        var projectDocs = await _unitOfWork.ProjectDocuments.GetAll();
        return Ok(projectDocs);
    }

    [HttpPost]
    public async Task<IActionResult> UploadProjectDoc([FromForm] ProjectDocumentCreateRequest request)
    {
        var imageName = await _fileUploadService.UploadFile(request.FileObject, "ProjectDocs");
        
        request.DocUrl = imageName;
        
        var proejctDocToCreate = _mapper.Map<ProjectDocument>(request);
        
        await _unitOfWork.ProjectDocuments.Add(proejctDocToCreate);
        await _unitOfWork.CompleteAsync();
        return Ok("Project Document uploaded successfully");
    }
    
    // update project doc
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProjectDoc(int id, [FromForm] ProjectDocumentUpdateRequest request)
    {
        var projectDoc = await _unitOfWork.ProjectDocuments.GetById(id);
        
        if (projectDoc == null)
        {
            return NotFound();
        }
        
        if (request.FileObject != null)
        {
            if (projectDoc.DocUrl != null) await _fileUploadService.DeleteFile(projectDoc.DocUrl, "ProjectDocs");
            
            var imageName = await _fileUploadService.UploadFile(request.FileObject, "ProjectDocs");
            projectDoc.Id = request.Id;
            projectDoc.DocUrl = imageName;
            projectDoc.ProjectId = request.ProjectId;
        }
        
        await _unitOfWork.ProjectDocuments.Update(projectDoc);
        await _unitOfWork.CompleteAsync();
        return Ok("Project Document updated successfully");
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectDoc(int id)
    {
        var projectDoc = await _unitOfWork.ProjectDocuments.GetById(id);
        
        if (projectDoc == null)
        {
            return NotFound();
        }
        
        await _unitOfWork.ProjectDocuments.Delete(projectDoc.Id);
        if (projectDoc.DocUrl != null) await _fileUploadService.DeleteFile(projectDoc.DocUrl, "ProjectDocs");
        
        await _unitOfWork.CompleteAsync();
        return Ok();
    }
}