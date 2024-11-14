using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses.projectschedule;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ProjectScheduleController : BaseController
{
    public ProjectScheduleController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllProjectSchedules()
    {
        var projectSchedules = await _unitOfWork.ProjectSchedules.GetAll();
        var projectSchedulesDto = _mapper.Map<IEnumerable<ProjectScheduleResponse>>(projectSchedules);

        return Ok(projectSchedulesDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAProjectSchedule(int id)
    {
        var projectSchedule = await _unitOfWork.ProjectSchedules.GetById(id);
        var projectScheduleDto = _mapper.Map<ProjectScheduleResponse>(projectSchedule);

        if (projectScheduleDto == null)
            return NotFound("ProjectSchedule not found");

        return Ok(projectScheduleDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectSchedule(ProjectScheduleCreateRequest createProjectScheduleRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var projectSchedule = _mapper.Map<ProjectSchedule>(createProjectScheduleRequest);

            await _unitOfWork.ProjectSchedules.Add(projectSchedule);
            await _unitOfWork.CompleteAsync();

            return Ok("ProjectSchedule created successfully");
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = e.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProjectSchedule(int id, UpdateProjectScheduleRequest updateProjectScheduleRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var projectSchedule = await _unitOfWork.ProjectSchedules.GetById(id);
            if (projectSchedule == null)
                return NotFound("ProjectSchedule not found");

            var projectScheduleToUpdate = _mapper.Map<ProjectSchedule>(updateProjectScheduleRequest);
            
            await _unitOfWork.ProjectSchedules.Update(projectScheduleToUpdate);
            await _unitOfWork.CompleteAsync();
            
            return Ok("ProjectSchedule updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteProjectSchedule(int id)
    {
        var projectSchedule = await _unitOfWork.ProjectSchedules.GetById(id);
        if (projectSchedule == null)
            return NotFound("ProjectSchedule not found");

        await _unitOfWork.ProjectSchedules.Delete(id);
        await _unitOfWork.CompleteAsync();

        return Ok("ProjectSchedule deleted successfully");
    }
}