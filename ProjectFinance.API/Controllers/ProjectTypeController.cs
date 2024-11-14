using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Dtos.Responses.projecttype;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ProjectTypeController : BaseController
{
    public ProjectTypeController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    
    public async Task<IActionResult> GetAllProjectTypes()
    {
        var projectTypes = await _unitOfWork.ProjectTypes.GetAll();
        var projectTypesDto = _mapper.Map<IEnumerable<CommonResponse>>(projectTypes);
        
        return Ok(projectTypesDto);
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetAProjectType(int id)
    {
        var projectType = await _unitOfWork.ProjectTypes.GetById(id);
        var projectTypeDto = _mapper.Map<CommonResponse>(projectType);
        
        if(projectTypeDto == null)
            return NotFound("ProjectType not found");
        
        return Ok(projectTypeDto);
    }

    [HttpPost]

    public async Task<IActionResult> CreateProjectType(CommonCreateRequest createProjectTypeRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var projectType = _mapper.Map<ProjectType>(createProjectTypeRequest);

            await _unitOfWork.ProjectTypes.Add(projectType);
            await _unitOfWork.CompleteAsync();
            
            return Ok("ProjectType created successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProjectType(int id, CommonUpdateRequest  updateProjectTypeRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");
        
        try
        {
            var projectType = await _unitOfWork.ProjectTypes.GetById(id);
            if (projectType == null)
                return NotFound("ProjectType not found");
            
            var projectTypeToUpdate = _mapper.Map<ProjectType>(updateProjectTypeRequest);
            
            await _unitOfWork.ProjectTypes.Update(projectTypeToUpdate);
            await _unitOfWork.CompleteAsync();
            
            return Ok("ProjectType updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectType(int id)
    {
        var projectType = await _unitOfWork.ProjectTypes.GetById(id);
        if (projectType == null)
            return NotFound("ProjectType not found");
        
        await _unitOfWork.ProjectTypes.Delete(id);
        await _unitOfWork.CompleteAsync();
        
        return Ok("ProjectType deleted successfully");
    }
}