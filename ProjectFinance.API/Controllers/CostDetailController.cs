using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses.costdetail;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class CostDetailController : BaseController
{
    public CostDetailController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    public Task<IActionResult> GetAllCostDetails()
    {
        var costDetails =  _unitOfWork.CostDetails.GetAll().Result.Join(
            
            _unitOfWork.CostCategories.GetAll().Result,
            costDetail => costDetail.CostCategoryId,
            costCategory => costCategory.Id,
            (costDetail, costCategory) => new CostDetailResponse
            {
                Id = costDetail.Id,
                Code = costDetail.Code,
                Name = costDetail.Name,
                CostCategoryId = costDetail.CostCategoryId,
                CostCategoryName = costCategory.Name
            }
        );
        
        
        return Task.FromResult<IActionResult>(Ok(costDetails));
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetACostDetail(int id)
    {
        var costDetail = await _unitOfWork.CostDetails.GetById(id);
        var costDetailDto = _mapper.Map<CostDetailResponse>(costDetail);
        
        if(costDetailDto == null)
            return NotFound("CostDetail not found");
        
        return Ok(costDetailDto);
    }
    
    [HttpPost]
    
    public async Task<IActionResult> CreateCostDetail(CostDetailCreateRequest createCostDetailRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var costDetail = _mapper.Map<CostDetail>(createCostDetailRequest);

            await _unitOfWork.CostDetails.Add(costDetail);
            await _unitOfWork.CompleteAsync();
        
            return Ok("CostDetail created successfully");
            
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while creating the CostDetail");
        }
    }
    
    [HttpPut("{id}")]
    
    public async Task<IActionResult> UpdateCostDetail(int id, CostDetailUpdateRequest updateCostDetailRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        var costDetail = await _unitOfWork.CostDetails.GetById(id);

        if(costDetail == null)
            return NotFound("CostDetail not found");

        var costDetailToUpdate = _mapper.Map<CostDetail>(updateCostDetailRequest);
        
        await _unitOfWork.CostDetails.Update(costDetailToUpdate);
        await _unitOfWork.CompleteAsync();

        return Ok("CostDetail updated successfully");
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteCostDetail(int id)
    {
        var costDetail = await _unitOfWork.CostDetails.GetById(id);
        
        if(costDetail == null)
            return NotFound("CostDetail not found");

        await _unitOfWork.CostDetails.Delete(id);
        await _unitOfWork.CompleteAsync();

        return Ok("CostDetail deleted successfully");
    }
}