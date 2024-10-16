using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Dtos.Responses.financeoption;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class FinanceOptionController : BaseController
{
    public FinanceOptionController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllFinanceOptions()
    {
        var financeOptions = await _unitOfWork.FinanceOptions.GetAll();
        var financeOptionsDto = _mapper.Map<IEnumerable<CommonResponse>>(financeOptions);
        
        return Ok(financeOptionsDto);
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetAFinanceOption(int id)
    {
        var financeOption = await _unitOfWork.FinanceOptions.GetById(id);
        var financeOptionDto = _mapper.Map<CommonResponse>(financeOption);
        
        if(financeOptionDto == null)
            return NotFound("FinanceOption not found");
        
        return Ok(financeOptionDto);
    }
    
    [HttpPost]
    
    public async Task<IActionResult> CreateFinanceOption(FinanceOptionResponse createFinanceOptionRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var financeOption = _mapper.Map<FinanceOption>(createFinanceOptionRequest);

            if (financeOption.BankId != null)
            {
                var financeOptionInDb = await _unitOfWork.FinanceOptions.GetByBankId(financeOption.BankId);
                if (financeOptionInDb != null)
                    return BadRequest("FinanceOption already exists");
            }

            await _unitOfWork.FinanceOptions.Add(financeOption);
            await _unitOfWork.CompleteAsync();
        
            return Ok("FinanceOption created successfully");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPut]
    
    public async Task<IActionResult> UpdateFinanceOption(FinanceOptionResponse updateFinanceOptionRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var financeOption = _mapper.Map<FinanceOption>(updateFinanceOptionRequest);

            if (financeOption.Id == null)
                return BadRequest("FinanceOption does not exist");

            var financeOptionInDb = await _unitOfWork.FinanceOptions.GetById(financeOption.Id);
            if (financeOptionInDb == null)
                return BadRequest("FinanceOption does not exist");

            await _unitOfWork.FinanceOptions.Update(financeOption);
            await _unitOfWork.CompleteAsync();
        
            return Ok("FinanceOption updated successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteFinanceOption(int id)
    {
        var financeOption = await _unitOfWork.FinanceOptions.GetById(id);
        if (financeOption == null)
            return NotFound("FinanceOption not found");

        await _unitOfWork.FinanceOptions.Delete(id);
        await _unitOfWork.CompleteAsync();
        
        return Ok("FinanceOption deleted successfully");
    }
    
}