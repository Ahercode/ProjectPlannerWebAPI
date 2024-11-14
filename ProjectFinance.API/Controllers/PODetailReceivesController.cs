using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class PODetailReceivesController : BaseController
{
    public PODetailReceivesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPODetailReceives()
    {
        var poDetailReceives = await _unitOfWork.PODetailReceives.GetAll();
        var poDetailReceivesDto = _mapper.Map<IEnumerable<PODetailReceiveResponse>>(poDetailReceives);
        
        return Ok(poDetailReceivesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPODetailReceive(int id)
    {
        var poDetailReceive = await _unitOfWork.PODetailReceives.GetById(id);
        var poDetailReceiveDto = _mapper.Map<PODetailReceiveResponse>(poDetailReceive);
        
        return Ok(poDetailReceiveDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePODetailReceive(PODetailReceiveCreateRequest poDetailReceiveRequest)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided");
            
            var poDetailReceive = _mapper.Map<PODetailReceive>(poDetailReceiveRequest);
            
            await _unitOfWork.PODetailReceives.Add(poDetailReceive);
            await _unitOfWork.CompleteAsync();
            
            return Ok("PODetailReceive created successfully");
        }
        catch (Exception e)
        {
            return BadRequest($"Errror: {e.Message}");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePODetailReceive(int id, PODetailReceiveUpdateRequest poDetailReceiveRequest)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided");
            
            var poDetailReceive = await _unitOfWork.PODetailReceives.GetById(id);
            
            if (poDetailReceive == null)
                return NotFound("PODetailReceive not found");
            
            var poDetailReceiveToUpdate = _mapper.Map<PODetailReceive>(poDetailReceiveRequest);
            await _unitOfWork.PODetailReceives.Update(poDetailReceiveToUpdate);
            await _unitOfWork.CompleteAsync();
            
            return Ok("PODetailReceive updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest($"Errror: {e.Message}");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePODetailReceive(int id)
    {
        var poDetailReceive = await _unitOfWork.PODetailReceives.GetById(id);
        
        if(poDetailReceive == null)
            return NotFound("PODetailReceive not found");
        
        await _unitOfWork.PODetailReceives.Delete(poDetailReceive.Id);
        await _unitOfWork.CompleteAsync();
        
        return Ok("PODetailReceive deleted successfully");
    }
    
}