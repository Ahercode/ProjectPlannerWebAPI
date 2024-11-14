using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
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
    public Task<IActionResult> GetAllFinanceOptions()
    {
        var financeOptions =  _unitOfWork.FinanceOptions.GetAll().Result.Join(
            _unitOfWork.Banks.GetAll().Result,
            financeOption => financeOption.BankId,
            bank => bank.Id,
            (financeOption, bank) => new FinanceOptionResponse
            {
                Id = financeOption.Id,
                BankId = financeOption.BankId,
                BankName = bank.Name,
                Description = financeOption.Description,
                OptionType = financeOption.OptionType,
            }
        );
        
        
        return Task.FromResult<IActionResult>(Ok(financeOptions));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAFinanceOption(int id)
    {
        var financeOption = await _unitOfWork.FinanceOptions.GetById(id);
        var financeOptionDto = _mapper.Map<FinanceOptionResponse>(financeOption);
        
        if(financeOptionDto == null)
            return NotFound("FinanceOption not found");
        
        return Ok(financeOptionDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateFinanceOption(FinanceOptionCreateRequest createFinanceOptionRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var financeOption = _mapper.Map<FinanceOption>(createFinanceOptionRequest);

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
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFinanceOption(int id, UpdateFinanceOptionRequest updateFinanceOptionRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var financeOptionInDb = await _unitOfWork.FinanceOptions.GetById(id);
            
            if (financeOptionInDb == null)
                return BadRequest("FinanceOption does not exist");
            
            var financeOption = _mapper.Map<FinanceOption>(updateFinanceOptionRequest);

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