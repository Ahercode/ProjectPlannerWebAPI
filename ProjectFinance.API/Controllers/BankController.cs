using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class BankController : BaseController
{
    public BankController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllBanks()
    {
        var banks = await _unitOfWork.Banks.GetAll();
        var banksDto = _mapper.Map<IEnumerable<CommonResponse>>(banks);
        
        return Ok(banksDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetABank(int id)
    {
        var bank = await _unitOfWork.Banks.GetById(id);
        var bankDto = _mapper.Map<CommonResponse>(bank);
        
        if(bankDto == null)
            return NotFound("Bank not found");
        
        return Ok(bankDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBank(CommonCreateRequest createBankRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var bank = _mapper.Map<Bank>(createBankRequest);

            if (bank.Code != null)
            {
                var bankInDb = await _unitOfWork.Banks.GetByCode(bank.Code);
                if (bankInDb != null)
                    return BadRequest("Bank already exists");
            }

            await _unitOfWork.Banks.Add(bank);
            await _unitOfWork.CompleteAsync();
        
            return Ok("Bank created successfully");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBank(int id, CommonUpdateRequest updateBankRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var bankInDb = await _unitOfWork.Banks.GetById(id);
            if (bankInDb == null)
                return BadRequest("Bank not found");
            
            var bank = _mapper.Map<Bank>(updateBankRequest);
            
            await _unitOfWork.Banks.Update(bank);
            await _unitOfWork.CompleteAsync();
        
            return Ok("Bank updated successfully");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteBank(int id)
    {
        var bank = await _unitOfWork.Banks.GetById(id);
        
        if(bank == null)
            return NotFound("Bank not found");
        
        await _unitOfWork.Banks.Delete(id);
        await _unitOfWork.CompleteAsync();
        
        return Ok("Bank deleted successfully");
    }
}