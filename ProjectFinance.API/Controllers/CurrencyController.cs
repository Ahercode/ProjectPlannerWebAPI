using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class CurrencyController : BaseController
{
       public CurrencyController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
       {
       }
       
       [HttpGet("")]
       
       public async Task<IActionResult> GetAllCurrencies()
       {
           var currencies = await _unitOfWork.Currencies.GetAll();
           var currenciesDto = _mapper.Map<IEnumerable<CommonResponse>>(currencies);
           
           return Ok(currenciesDto);
       }
       
       [HttpGet("{id}")]
       public async Task<IActionResult> GetACurrency(int id)
       {
           var currency = await _unitOfWork.Currencies.GetById(id);
           var currencyDto = _mapper.Map<CommonResponse>(currency);
           
           if(currencyDto == null)
               return NotFound("Currency not found");
           
           return Ok(currencyDto);
       }
       
       
       [HttpPost] 
       public async Task<IActionResult> CreateCurrency(CommonCreateRequest createCurrencyRequest)
     {
         if (!ModelState.IsValid)
             return BadRequest("Invalid data provided");

         try
         {
             var currency = _mapper.Map<Currency>(createCurrencyRequest);

             await _unitOfWork.Currencies.Add(currency);
             await _unitOfWork.CompleteAsync();
         
             return Ok("Currency created successfully");
             
         }
         catch (Exception e)
         {
             return BadRequest("An error occurred while creating the currency");
         }
     }

       [HttpPut("{id}")]
       public async Task<IActionResult> UpdateCurrency(int id, CommonUpdateRequest updateCurrencyRequest)
       {
           if (!ModelState.IsValid)
               return BadRequest("Invalid data provided");

           var currency = await _unitOfWork.Currencies.GetById(id);
           
           if(currency == null)
               return NotFound("Currency not found");
           
           var currencyToUpdate = _mapper.Map<Currency>(updateCurrencyRequest);
              
           await _unitOfWork.Currencies.Update(currencyToUpdate);
           await _unitOfWork.CompleteAsync();
                

           return Ok("Currency updated successfully");

       }
       
       [HttpDelete("{id}")]
       
       public async Task<IActionResult> DeleteCurrency(int id)
       {
           var currency = await _unitOfWork.Currencies.GetById(id);
           
           if(currency == null)
               return NotFound("Currency not found");
           
           await _unitOfWork.Currencies.Delete(id);
           await _unitOfWork.CompleteAsync();
           
           return Ok("Currency deleted successfully");
       }
}