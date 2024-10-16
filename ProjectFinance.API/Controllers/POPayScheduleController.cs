using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Responses.popayschedule;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class POPayScheduleController : BaseController
{
    public POPayScheduleController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    
    public async Task<IActionResult> GetAllPOPaySchedules()
    {
        var poPaySchedules = await _unitOfWork.POPaySchedules.GetAll();
        var poPaySchedulesDto = _mapper.Map<IEnumerable<POPayScheduleResponse>>(poPaySchedules);
        
        return Ok(poPaySchedulesDto);
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetAPOPaySchedule(int id)
    {
        var poPaySchedule = await _unitOfWork.POPaySchedules.GetById(id);
        var poPayScheduleDto = _mapper.Map<POPayScheduleResponse>(poPaySchedule);
        
        if(poPayScheduleDto == null)
            return NotFound("POPaySchedule not found");
        
        return Ok(poPayScheduleDto);
    }
    
    [HttpPost]
    
    public async Task<IActionResult> CreatePOPaySchedule(POPayScheduleResponse createPOPayScheduleRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var poPaySchedule = _mapper.Map<POPaySchedule>(createPOPayScheduleRequest);

            if (poPaySchedule.Id != null)
            {
                var poPayScheduleInDb = await _unitOfWork.POPaySchedules.GetById(poPaySchedule.Id);
                if (poPayScheduleInDb != null)
                    return BadRequest("POPaySchedule already exists");
            }

            await _unitOfWork.POPaySchedules.Add(poPaySchedule);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }
    
    [HttpPut]
    
    public async Task<IActionResult> UpdatePOPaySchedule(POPayScheduleResponse updatePOPayScheduleRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var poPaySchedule = _mapper.Map<POPaySchedule>(updatePOPayScheduleRequest);

            var poPayScheduleInDb = await _unitOfWork.POPaySchedules.GetById(poPaySchedule.Id);
            if (poPayScheduleInDb == null)
                return BadRequest("POPaySchedule not found");

            await _unitOfWork.POPaySchedules.Update(poPaySchedule);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeletePOPaySchedule(int id)
    {
        try
        {
            var poPaySchedule = await _unitOfWork.POPaySchedules.GetById(id);
            if (poPaySchedule == null)
                return NotFound("POPaySchedule not found");

            await _unitOfWork.POPaySchedules.Delete(id);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }
}