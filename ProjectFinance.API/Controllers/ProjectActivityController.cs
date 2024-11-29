using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses.projectactivity;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.HelpingServices.UploadFile;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ProjectActivityController : BaseController
{
    private readonly IFileUploadService _fileUploadService;
    public ProjectActivityController(IUnitOfWork unitOfWork, IMapper mapper, IFileUploadService fileUploadService) : base(unitOfWork, mapper)
    {
        _fileUploadService = fileUploadService;
    }
  
   [HttpGet("")]
   public async Task<IActionResult> GetAllProjectActivities()
   {
       var projectActivities = _unitOfWork.ProjectActivities.GetAll().Result
           .Join(
               _unitOfWork.Projects.GetAll().Result,
               projectActivity => projectActivity.ProjectId,
               project => project.Id,
               (projectActivity, project) => new { projectActivity, project }).Join(

               _unitOfWork.Activities.GetAll().Result,
               projectActivityProject => projectActivityProject.projectActivity.ActivityId,
               activity => activity.Id,
               (projectActivityProject, activity) => new { projectActivityProject, activity })
           .Join(
               _unitOfWork.Contractors.GetAll().Result,
               projectActivityProjectActivity =>
                   projectActivityProjectActivity.projectActivityProject.projectActivity.ContractorId,
               contractor => contractor.id
               , (projectActivityProjectActivity, contractor) => new ProjectActivityResponse()
               {
                   Id = projectActivityProjectActivity.projectActivityProject.projectActivity.Id,
                   ActivityId = projectActivityProjectActivity.projectActivityProject.projectActivity.ActivityId,
                   ActivityName = projectActivityProjectActivity.activity.Name,
                   ProjectId = projectActivityProjectActivity.projectActivityProject.projectActivity.ProjectId,
                   ProjectName = projectActivityProjectActivity.projectActivityProject.project.Name,
                   Reference = projectActivityProjectActivity.projectActivityProject.projectActivity.Reference,
                   Amount = projectActivityProjectActivity.projectActivityProject.projectActivity.Amount,
                   StartDate = projectActivityProjectActivity.projectActivityProject.projectActivity.StartDate,
                   EndDate = projectActivityProjectActivity.projectActivityProject.projectActivity.EndDate,
                   ContractorId = projectActivityProjectActivity.projectActivityProject.projectActivity.ContractorId,
                   Note = projectActivityProjectActivity.projectActivityProject.projectActivity.Note,
                   FileName = projectActivityProjectActivity.projectActivityProject.projectActivity.FileName,
                   ContractorName = contractor.name
               }
           );
       
       return Ok(projectActivities);
   }
   
   [HttpGet("{id}")]
   public async Task<IActionResult> GetAProjectActivity(int id)
   {
       var projectActivity = await _unitOfWork.ProjectActivities.GetById(id);
       var projectActivityDto = _mapper.Map<ProjectActivityResponse>(projectActivity);
       
       if(projectActivityDto == null)
           return NotFound("ProjectActivity not found");
       
       return Ok(projectActivityDto);
   }
   
   [HttpPost]
   public async Task<IActionResult> CreateProjectActivity([FromForm] ProjectActivityRequest createProjectActivityRequest)
   {
       if(!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var fileName = await _fileUploadService.UploadFile(createProjectActivityRequest.FileObject, "ProjectDocs");

           createProjectActivityRequest.FileName = fileName;
           
           var projectActivity = _mapper.Map<ProjectActivity>(createProjectActivityRequest);

           await _unitOfWork.ProjectActivities.Add(projectActivity);
           await _unitOfWork.CompleteAsync();
       }
       catch (Exception e)
       { 
           return BadRequest(e.Message);
       }
       
       return Ok("ProjectActivity created successfully");
   }
   
   [HttpPut("{id}")] 
   public async Task<IActionResult> UpdateProjectActivity(int id,[FromForm] UpdateProjectActivity updateProjectActivityRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");
        

        var projectActivity = await _unitOfWork.ProjectActivities.GetById(id);

        if (projectActivity == null)
            return NotFound("ProjectActivity not found");

        try
        { 
            var projectActivityDto = _mapper.Map<ProjectActivity>(updateProjectActivityRequest);
            
            if (updateProjectActivityRequest.FileObject != null)
            {
                if (projectActivity.FileName != null)
                    await _fileUploadService.DeleteFile(projectActivity.FileName, "ProjectDocs");

                var fileName = await _fileUploadService.UploadFile(updateProjectActivityRequest.FileObject, "ProjectDocs");
                
                projectActivityDto.FileName = fileName;
                
            }
            
            await _unitOfWork.ProjectActivities.Update(projectActivityDto);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("ProjectActivity updated successfully");
    }
   
   [HttpDelete("{id}")]
   public async Task<IActionResult> DeleteProjectActivity(int id)
   {
       var projectActivity = await _unitOfWork.ProjectActivities.GetById(id);
       
       if(projectActivity == null)
           return NotFound("ProjectActivity not found");
       
       if(projectActivity.FileName != null)
         await _fileUploadService.DeleteFile(projectActivity.FileName, "ProjectDocs");
       
       await _unitOfWork.ProjectActivities.Delete(id);
       await _unitOfWork.CompleteAsync();
       
       return Ok("ProjectActivity deleted successfully");
   }
}