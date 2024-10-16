using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ProjectActivityCostController : BaseController
{
    public ProjectActivityCostController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    
    public async Task<IActionResult> GetAllProjectActivityCosts()
    {
        var projectActivityCosts = await _unitOfWork.ProjectActivityCosts.GetAll();
        var projectActivityCostsDto = _mapper.Map<IEnumerable<ProjectActivityCost>>(projectActivityCosts);
        
        return Ok(projectActivityCostsDto);
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetAProjectActivityCost(int id)
    {
        var projectActivityCost = await _unitOfWork.ProjectActivityCosts.GetById(id);
        var projectActivityCostDto = _mapper.Map<ProjectActivityCost>(projectActivityCost);
        
        if(projectActivityCostDto == null)
            return NotFound("ProjectActivityCost not found");
        
        return Ok(projectActivityCostDto);
    }
    
    [HttpPost]
    
    public async Task<IActionResult> CreateProjectActivityCost(ProjectActivityCost createProjectActivityCostRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");
 
        try
        {
            var projectActivityCost = _mapper.Map<ProjectActivityCost>(createProjectActivityCostRequest);
 
            if (projectActivityCost.Id != null)
            {
                var projectActivityCostInDb = await _unitOfWork.ProjectActivityCosts.GetById(projectActivityCost.Id);
                if (projectActivityCostInDb != null)
                    return BadRequest("ProjectActivityCost already exists");
            }
 
            await _unitOfWork.ProjectActivityCosts.Add(projectActivityCost);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while creating the ProjectActivityCost");
        }
        
        return Ok("ProjectActivityCost created successfully");
    }
    
    [HttpPut]
    
    public async Task<IActionResult> UpdateProjectActivityCost(ProjectActivityCost updateProjectActivityCostRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");
 
        try
        {
            var projectActivityCost = _mapper.Map<ProjectActivityCost>(updateProjectActivityCostRequest);
 
            if (projectActivityCost.Id == null)
                return BadRequest("ProjectActivityCost does not exist");
 
            var projectActivityCostInDb = await _unitOfWork.ProjectActivityCosts.GetById(projectActivityCost.Id);
            if (projectActivityCostInDb == null)
                return BadRequest("ProjectActivityCost does not exist");
 
            await _unitOfWork.ProjectActivityCosts.Update(projectActivityCost);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while updating the ProjectActivityCost");
        }
        
        return Ok("ProjectActivityCost updated successfully");
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteProjectActivityCost(int id)
    {
        try
        {
            var projectActivityCost = await _unitOfWork.ProjectActivityCosts.GetById(id);
            if (projectActivityCost == null)
                return BadRequest("ProjectActivityCost does not exist");
 
            await _unitOfWork.ProjectActivityCosts.Delete(id);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while deleting the ProjectActivityCost");
        }
        
        return Ok("ProjectActivityCost deleted successfully");
    }
    
}