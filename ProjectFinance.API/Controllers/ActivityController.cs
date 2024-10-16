using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ActivityController : BaseController
{
    public ActivityController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllActivities()
    {
        var activities = await _unitOfWork.Activities.GetAll();
        var activitiesDto = _mapper.Map<IEnumerable<CommonResponse>>(activities);
        
        return Ok(activitiesDto);
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetAnActivity(int id)
    {
        var activity = await _unitOfWork.Activities.GetById(id);
        var activityDto = _mapper.Map<CommonResponse>(activity);
        
        if(activityDto == null)
            return NotFound("Activity not found");
        
        return Ok(activityDto);
    }
    
    [HttpPost]
    
    public async Task<IActionResult> CreateActivity(CommonCreateRequest createActivityRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var activity = _mapper.Map<Activity>(createActivityRequest);

            if (activity.Code != null)
            {
                var activityInDb = await _unitOfWork.Activities.GetByCode(activity.Code);
                if (activityInDb != null)
                    return BadRequest("Activity already exists");
            }

            await _unitOfWork.Activities.Add(activity);
            await _unitOfWork.CompleteAsync();
        
            return Ok("Activity created successfully");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPut]
    
    public async Task<IActionResult> UpdateActivity(CommonUpdateRequest updateActivityRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var activity = _mapper.Map<Activity>(updateActivityRequest);

            var activityInDb = await _unitOfWork.Activities.GetById(activity.Id);
            if (activityInDb == null)
                return BadRequest("Activity not found");

            _unitOfWork.Activities.Update(activity);
            await _unitOfWork.CompleteAsync();
        
            return Ok("Activity updated successfully");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteActivity(int id)
    {
        var activity = await _unitOfWork.Activities.GetById(id);
        
        if(activity == null)
            return NotFound("Activity not found");
        
        await _unitOfWork.Activities.Delete(id);
        await _unitOfWork.CompleteAsync();
        
        return Ok("Activity deleted successfully");
    }
    
}