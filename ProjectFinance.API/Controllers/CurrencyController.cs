using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
   
   [HttpPost]
       public async Task<IActionResult> GetACurrency(int id)
       {
           var currency = await _unitOfWork.Currencies.GetById(id);
           var currencyDto = _mapper.Map<CommonResponse>(currency);
           
           if(currencyDto == null)
               return NotFound("Currency not found");
           
           return Ok(currencyDto);
       }

       [HttpPut]

       public async Task<IActionResult> UpdateCurrency(CommonResponse updateCurrencyRequest)
       {
           if (!ModelState.IsValid)
               return BadRequest("Invalid data provided");

           try
           {
               var currency = _mapper.Map<Currency>(updateCurrencyRequest);
               var currencyInDb = await _unitOfWork.Currencies.GetById(currency.Id);

               if (currencyInDb == null)
                   return NotFound("Currency not found");
           }
           catch (Exception e)
           {
               return BadRequest("An error occurred while updating the currency");
           }

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