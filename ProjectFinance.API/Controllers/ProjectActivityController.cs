using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Responses.projectactivity;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ProjectActivityController : BaseController
{
    public ProjectActivityController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
  
   [HttpGet("")]
   
   public async Task<IActionResult> GetAllProjectActivities()
   {
       var projectActivities = await _unitOfWork.ProjectActivities.GetAll();
       var projectActivitiesDto = _mapper.Map<IEnumerable<ProjectActivityResponse>>(projectActivities);
       
       return Ok(projectActivitiesDto);
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
   
   public async Task<IActionResult> CreateProjectActivity(ProjectActivityResponse createProjectActivityRequest)
   {
       if(!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var projectActivity = _mapper.Map<ProjectActivity>(createProjectActivityRequest);

           if (projectActivity.Id != null)
           {
               var projectActivityInDb = await _unitOfWork.ProjectActivities.GetById(projectActivity.Id);
               if (projectActivityInDb != null)
                   return BadRequest("ProjectActivity already exists");
           }

           await _unitOfWork.ProjectActivities.Add(projectActivity);
           await _unitOfWork.CompleteAsync();
       }
       catch (Exception e)
       { 
           return BadRequest(e.Message);
       }
       
       return Ok("ProjectActivity created successfully");
   }
   
   [HttpDelete("{id}")]
   
   public async Task<IActionResult> DeleteProjectActivity(int id)
   {
       var projectActivity = await _unitOfWork.ProjectActivities.GetById(id);
       
       if(projectActivity == null)
           return NotFound("ProjectActivity not found");
       
       await _unitOfWork.ProjectActivities.Delete(id);
       await _unitOfWork.CompleteAsync();
       
       return Ok("ProjectActivity deleted successfully");
   }
}