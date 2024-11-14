using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class StakeHoldersController: BaseController
{
    public StakeHoldersController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> GetStakeHolders()
    {
        var stakeHolders = await _unitOfWork.StakeHolders.GetAll();
        return Ok(stakeHolders);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStakeHolder(int id)
    {
        var stakeHolder = await _unitOfWork.StakeHolders.GetById(id);
        return Ok(stakeHolder);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStakeHolder(StakeHolderCreateRequest stakeHolder)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");
        
        var stakeHolderDto = _mapper.Map<StakeHolder>(stakeHolder);
        
        await _unitOfWork.StakeHolders.Add(stakeHolderDto);
        await _unitOfWork.CompleteAsync();
        return Ok("StakeHolder created successfully");
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStakeHolder(int id, StakeHolderUpdateRequest stakeHolder)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");
        
        var stakeHolderDto = await _unitOfWork.StakeHolders.GetById(id);
        
        if (stakeHolderDto == null)
            return NotFound("StakeHolder not found");
        
        var stakeHolderEntity = _mapper.Map<StakeHolder>(stakeHolder);
        
        await _unitOfWork.StakeHolders.Update(stakeHolderEntity);
        await _unitOfWork.CompleteAsync();
        return Ok("StakeHolder updated successfully");
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStakeHolder(int id)
    {
        var stakeHolder = await _unitOfWork.StakeHolders.GetById(id);
        
        if (stakeHolder == null)
            return NotFound("StakeHolder not found");
        
        await _unitOfWork.StakeHolders.Delete(stakeHolder.Id);
        await _unitOfWork.CompleteAsync();
        return Ok("StakeHolder deleted successfully");
    }
}