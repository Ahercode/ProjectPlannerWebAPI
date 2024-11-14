using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class PODetailsController : BaseController
{
    public PODetailsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPODetails()
    {
        var poDetails = await _unitOfWork.PODetails.GetAll();
        var poDetailsDto = _mapper.Map<IEnumerable<PODetailResponse>>(poDetails);
        
        return Ok(poDetailsDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPODetail(int id)
    {
        var poDetail = await _unitOfWork.PODetails.GetById(id);
        var poDetailDto = _mapper.Map<PODetailResponse>(poDetail);
        
        return Ok(poDetailDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePODetail(PODetailCreateRequest poDetailRequest)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided");
            
            var poDetail = _mapper.Map<PODetail>(poDetailRequest);
            
            await _unitOfWork.PODetails.Add(poDetail);
            await _unitOfWork.CompleteAsync();
            
            return Ok("PODetail created successfully");
        }
        catch (Exception e)
        {
            return BadRequest($"Errror: {e.Message}");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePODetail(int id, PODetailUpdateRequest poDetailRequest)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided");
            
            var poDetail = await _unitOfWork.PODetails.GetById(id);
            
            if (poDetail == null)
                return NotFound("PODetail not found");
            
            var poDetailToUpdate = _mapper.Map<PODetail>(poDetailRequest);
            await _unitOfWork.PODetails.Update(poDetailToUpdate);
            await _unitOfWork.CompleteAsync();
            
            return Ok("PODetail updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest($"Errror: {e.Message}");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePODetail(int id)
    {
        var poDetail = await _unitOfWork.PODetails.GetById(id);
        
        if(poDetail == null)
            return NotFound("PODetail not found");
        
        await _unitOfWork.PODetails.Delete(poDetail.Id);
        await _unitOfWork.CompleteAsync();
        
        return Ok("PODetail deleted successfully");
    }
}