using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        var projectTypesDto = _mapper.Map<IEnumerable<ProjectTypeResponse>>(projectTypes);
        
        return Ok(projectTypesDto);
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetAProjectType(int id)
    {
        var projectType = await _unitOfWork.ProjectTypes.GetById(id);
        var projectTypeDto = _mapper.Map<ProjectTypeResponse>(projectType);
        
        if(projectTypeDto == null)
            return NotFound("ProjectType not found");
        
        return Ok(projectTypeDto);
    }

    [HttpPost]

    public async Task<IActionResult> CreateProjectType(ProjectTypeResponse createProjectTypeRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var projectType = _mapper.Map<ProjectType>(createProjectTypeRequest);

            if (projectType.Id != null)
            {
                var projectTypeInDb = await _unitOfWork.ProjectTypes.GetById(projectType.Id);
                if (projectTypeInDb != null)
                    return BadRequest("ProjectType already exists");
            }

            await _unitOfWork.ProjectTypes.Add(projectType);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }


        return Ok();

    }
    
    [HttpPut("{id}")]
    
    public async Task<IActionResult> UpdateProjectType(int id, ProjectTypeResponse updateProjectTypeRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");
        
        try
        {
            var projectType = await _unitOfWork.ProjectTypes.GetById(id);
            if (projectType == null)
                return NotFound("ProjectType not found");
            
            _mapper.Map(updateProjectTypeRequest, projectType);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok();
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteProjectType(int id)
    {
        var projectType = await _unitOfWork.ProjectTypes.GetById(id);
        if (projectType == null)
            return NotFound("ProjectType not found");
        
        await _unitOfWork.ProjectTypes.Delete(id);
        await _unitOfWork.CompleteAsync();
        
        return Ok();
    }
}