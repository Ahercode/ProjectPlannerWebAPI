using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses.monitoringevaluation;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class MonitoringEvaluationController : BaseController
{
    public MonitoringEvaluationController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var monitoringEvaluations = _unitOfWork.MonitoringEvaluations.GetAll().Result
            .Join(
                _unitOfWork.Projects.GetAll().Result,
                monitoringEvaluation => monitoringEvaluation.ProjectId,
                project => project.Id,
                (monitoringEvaluation, project) => new { monitoringEvaluation, project })
            .Join(
                _unitOfWork.Activities.GetAll().Result,
                monitoringEvaluationProject => monitoringEvaluationProject.monitoringEvaluation.ActivityId,
                activity => activity.Id,
                (monitoringEvaluationProject, activity) => new MonitoringEvaluationResponse()
                {
                    Id = monitoringEvaluationProject.monitoringEvaluation.Id,
                    ProjectId = monitoringEvaluationProject.monitoringEvaluation.ProjectId,
                    ActivityId = monitoringEvaluationProject.monitoringEvaluation.ActivityId,
                    workDone = monitoringEvaluationProject.monitoringEvaluation.workDone,
                    Note = monitoringEvaluationProject.monitoringEvaluation.Note,
                    ProjectName = monitoringEvaluationProject.project.Name,
                    ActivityName = activity.Name

                }
            );
        
        return Ok(monitoringEvaluations);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var monitoringEvaluation = await _unitOfWork.MonitoringEvaluations.GetById(id);
        if (monitoringEvaluation == null)
            return NotFound();
        
        var monitoringEvaluationResponse = _mapper.Map<MonitoringEvaluationResponse>(monitoringEvaluation);
        return Ok(monitoringEvaluationResponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(MonitoringEvaluationCreateRequest monitoringEvaluationRequest)
    {
        try
        {
            var monitoringEvaluation = _mapper.Map<MonitoringEvaluation>(monitoringEvaluationRequest);
            await _unitOfWork.MonitoringEvaluations.Add(monitoringEvaluation);
            await _unitOfWork.CompleteAsync();
        
            return Ok("Monitoring Evaluation created successfully");
        }
        catch (Exception e)
        {
            return BadRequest($"Error creating monitoring evaluation record: {e.Message}");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MonitoringEvaluationUpdateRequest monitoringEvaluationRequest)
    {
        try
        {
            var monitoringEvaluation = await _unitOfWork.MonitoringEvaluations.GetById(id);
            if (monitoringEvaluation == null)
                return NotFound();
            
            var monitoringEvaluationToUpdate = _mapper.Map<MonitoringEvaluation>(monitoringEvaluationRequest);
            await _unitOfWork.MonitoringEvaluations.Update(monitoringEvaluationToUpdate);
            await _unitOfWork.CompleteAsync();
        
            return Ok("Monitoring Evaluation updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest($"Error updating monitoring evaluation record: {e.Message}");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var monitoringEvaluation = await _unitOfWork.MonitoringEvaluations.GetById(id);
            if (monitoringEvaluation == null)
                return NotFound();
            
            await _unitOfWork.MonitoringEvaluations.Delete(id);
            await _unitOfWork.CompleteAsync();
        
            return Ok("Monitoring Evaluation deleted successfully");
        }
        catch (Exception e)
        {
            return BadRequest($"Error deleting monitoring evaluation record: {e.Message}");
        }
    }
}