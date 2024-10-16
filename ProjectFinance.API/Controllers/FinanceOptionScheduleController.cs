using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Dtos.Responses.financeoption;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class FinanceOptionSchedulesController : BaseController
{
    public FinanceOptionSchedulesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
   
   [HttpGet("")]
   public async Task<IActionResult> GetAllFinanceOptionSchedules()
   {
       var financeOptionSchedules = await _unitOfWork.FinanceOptionSchedules.GetAll();
       var financeOptionSchedulesDto = _mapper.Map<IEnumerable<CommonResponse>>(financeOptionSchedules);
       
       return Ok(financeOptionSchedulesDto);
   }
   
   [HttpGet("{id}")]
   
   public async Task<IActionResult> GetAFinanceOptionSchedule(int id)
   {
       var financeOptionSchedule = await _unitOfWork.FinanceOptionSchedules.GetById(id);
       var financeOptionScheduleDto = _mapper.Map<CommonResponse>(financeOptionSchedule);
       
       if(financeOptionScheduleDto == null)
           return NotFound("FinanceOptionSchedule not found");
       
       return Ok(financeOptionScheduleDto);
   }
   
   [HttpPost]
   
   public async Task<IActionResult> CreateFinanceOptionSchedule(CommonCreateRequest createFinanceOptionScheduleRequest)
   {
       if(!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var financeOptionSchedule = _mapper.Map<FinanceOptionSchedule>(createFinanceOptionScheduleRequest);

           if (financeOptionSchedule.FinanceOptionId != null)
           {
               var financeOptionScheduleInDb = await _unitOfWork.FinanceOptionSchedules.GetByFinanceOptionId(financeOptionSchedule.FinanceOptionId);
               if (financeOptionScheduleInDb != null)
                   return BadRequest("FinanceOptionSchedule already exists");
           }

           await _unitOfWork.FinanceOptionSchedules.Add(financeOptionSchedule);
           await _unitOfWork.CompleteAsync();
       
           return Ok("FinanceOptionSchedule created successfully");
       }
       catch (Exception ex)
       {
           return BadRequest(ex.Message);
       }
   }
   
   [HttpPut("{id}")]
   
   public async Task<IActionResult> UpdateFinanceOptionSchedule(int id, FinanceOptionResponse updateFinanceOptionScheduleRequest)
   {
       if(!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var financeOptionSchedule = await _unitOfWork.FinanceOptionSchedules.GetById(id);
           if(financeOptionSchedule == null)
               return NotFound("FinanceOptionSchedule not found");

           _mapper.Map(updateFinanceOptionScheduleRequest, financeOptionSchedule);
           await _unitOfWork.CompleteAsync();
       
           return Ok("FinanceOptionSchedule updated successfully");
       }
       catch (Exception ex)
       {
           return BadRequest(ex.Message);
       }
   }
   
   [HttpDelete("{id}")]
   
   public async Task<IActionResult> DeleteFinanceOptionSchedule(int id)
   {
       var financeOptionSchedule = await _unitOfWork.FinanceOptionSchedules.GetById(id);
       if(financeOptionSchedule == null)
           return NotFound("FinanceOptionSchedule not found");

       // await _unitOfWork.FinanceOptionSchedules.Delete(financeOptionSchedule);
       await _unitOfWork.CompleteAsync();
       
       return Ok("FinanceOptionSchedule deleted successfully");
   }
   
}