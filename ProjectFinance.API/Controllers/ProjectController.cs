using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Responses.project;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ProjectController : BaseController
{
    public ProjectController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _unitOfWork.Projects.GetAll();
        var projectsDto = _mapper.Map<IEnumerable<ProjectResponse>>(projects);
        
        return Ok(projectsDto);
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetAProject(int id)
    {
        var project = await _unitOfWork.Projects.GetById(id);
        var projectDto = _mapper.Map<ProjectResponse>(project);
        
        if(projectDto == null)
            return NotFound("Project not found");
        
        return Ok(projectDto);
    }
    
    [HttpPost]
    
    public async Task<IActionResult> CreateProject(ProjectResponse createProjectRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var project = _mapper.Map<Project>(createProjectRequest);

            if (project.Id != null)
            {
                var projectInDb = await _unitOfWork.Projects.GetById(project.Id);
                if (projectInDb != null)
                    return BadRequest("Project already exists");
            }

            await _unitOfWork.Projects.Add(project);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok();
    }
    
    [HttpPut("{id}")]
    
    public async Task<IActionResult> UpdateProject(int id, ProjectResponse updateProjectRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var project = await _unitOfWork.Projects.GetById(id);

            if (project == null)
                return NotFound("Project not found");

            _mapper.Map(updateProjectRequest, project);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteProject(int id)
    {
        try
        {
            var project = await _unitOfWork.Projects.GetById(id);

            if (project == null)
                return NotFound("Project not found");

            await _unitOfWork.Projects.Delete(id);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }
}