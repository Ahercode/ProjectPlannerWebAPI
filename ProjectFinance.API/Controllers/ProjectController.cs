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
        var projects = _unitOfWork.Projects.GetAll().Result.Join(
                _unitOfWork.Clients.GetAll().Result,
                project => project.ClientId,
                client => client.Id,
                (project, client) => new { project, client }
            ).Join(
                _unitOfWork.ProjectTypes.GetAll().Result,
                projectClient => projectClient.project.ProjectTypeId,
                projectType => projectType.Id,
                (projectClient, projectType) => new { projectClient, projectType }
            ).Join(
                _unitOfWork.Contractors.GetAll().Result,
                projectClientProjectType => projectClientProjectType.projectClient.project.ContractorId,
                contractor => contractor.id,
                (projectClientProjectType, contractor) => new { projectClientProjectType, contractor })
            .Join(
                _unitOfWork.Currencies.GetAll().Result,
                projectClientProjectTypeContractor => projectClientProjectTypeContractor.projectClientProjectType
                    .projectClient.project.CurrencyId,
                currency => currency.Id,
                (projectClientProjectTypeContractor, currency) => new { projectClientProjectTypeContractor, currency })
            .Join(
                _unitOfWork.ProjectCategories.GetAll().Result,
                projectClientProjectTypeContractorCurrency => projectClientProjectTypeContractorCurrency.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.ProjectCategoryId,
                projectCategory => projectCategory.Id,
                (p, projectCategory) => new ProjectResponse()
                {
                    Id = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.Id,
                    Name = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.Name,
                    Code = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.Code,
                    ProjectTypeId = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.ProjectTypeId,
                    ProjectTypeName = p.projectClientProjectTypeContractor.projectClientProjectType.projectType.Name,
                    ProjectCategoryId = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.ProjectCategoryId,
                    ProjectCategoryName = projectCategory.Name,
                    ClientId = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.ClientId,
                    ClientName = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.client.Name,
                    ContractorId = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.ContractorId,
                    ContractorName = p.projectClientProjectTypeContractor.contractor.name,
                    CurrencyId = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.CurrencyId,
                    CurrencyName = p.currency.Name,
                    StartDate = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.StartDate,
                    EndDate = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.EndDate,
                    ContractSum = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.ContractSum,
                    Note = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.Note,
                    Status = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.Status,
                    Location = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.Location,
                    ProjectPriority = p.projectClientProjectTypeContractor.projectClientProjectType.projectClient.project.ProjectPriority,
                    
                });
        
        return await Task.FromResult<IActionResult>(Ok(projects));
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