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
    public async Task<IActionResult> GetAllCostDetails()
    {
        var costDetails = await _unitOfWork.CostDetails.GetAll();
        var costDetailsDto = _mapper.Map<IEnumerable<CostDetailResponse>>(costDetails);
        
        return Ok(costDetailsDto);
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
    
    public async Task<IActionResult> CreateCostDetail(CommonCreateRequest createCostDetailRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var costDetail = _mapper.Map<CostDetail>(createCostDetailRequest);

            if (costDetail.Code != null)
            {
                var costDetailInDb = await _unitOfWork.CostDetails.GetByCode(costDetail.Code);
                if (costDetailInDb != null)
                    return BadRequest("CostDetail already exists");
            }

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
    
    public async Task<IActionResult> UpdateCostDetail(int id, CommonUpdateRequest updateCostDetailRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        var costDetail = await _unitOfWork.CostDetails.GetById(id);

        if(costDetail == null)
            return NotFound("CostDetail not found");

        _mapper.Map(updateCostDetailRequest, costDetail);

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