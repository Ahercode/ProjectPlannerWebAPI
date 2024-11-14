using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Dtos.Responses.costcategory;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class CostCategoryController : BaseController
{
    public CostCategoryController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllCostCategories()
    {
        var costs = await _unitOfWork.CostCategories.GetAll();
        var costsDto = _mapper.Map<IEnumerable<CommonResponse>>(costs);
        
        return Ok(costsDto);
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetACost(int id)
    {
        var cost = await _unitOfWork.CostCategories.GetById(id);
        var costDto = _mapper.Map<CommonResponse>(cost);
        
        if(costDto == null)
            return NotFound("Cost not found");
        
        return Ok(costDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCost(CommonCreateRequest createCostRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var cost = _mapper.Map<CostCategory>(createCostRequest);

            await _unitOfWork.CostCategories.Add(cost);
            await _unitOfWork.CompleteAsync();
        
            return Ok("Cost created successfully");
            
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while creating the cost");
        }
    }
    
    [HttpPut("{id}")]
    
    public async Task<IActionResult> UpdateCost(CommonUpdateRequest updateCostRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var cost = _mapper.Map<CostCategory>(updateCostRequest);

            var costInDb = await _unitOfWork.CostCategories.GetById(cost.Id);
            if (costInDb == null)
                return BadRequest("Cost not found");

            var costCategory = _mapper.Map<CostCategory>(updateCostRequest);

            await _unitOfWork.CostCategories.Update(costCategory);
            await _unitOfWork.CompleteAsync();
        
            return Ok("Cost updated successfully");
            
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while updating the cost");
        }
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteCost(int id)
    {
        var cost = await _unitOfWork.CostCategories.GetById(id);
        
        if (cost == null)
            return NotFound("Cost not found");
        
        await _unitOfWork.CostCategories.Delete(id);
        await _unitOfWork.CompleteAsync();
        
        return Ok("Cost deleted successfully");
    }
    
}