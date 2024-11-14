using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
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
   public  Task<IActionResult> GetAllFinanceOptionSchedules()
   {
       var financeOptionSchedules = _unitOfWork.FinanceOptionSchedules.GetAll().Result.Join(
           _unitOfWork.FinanceOptions.GetAll().Result,
           financeOptionSchedule => financeOptionSchedule.FinanceOptionId,
           financeOp => financeOp.Id,
           (financeOptionSchedule, financeOp) => new FinanceOptionScheduleResponse
           {
               Id = financeOptionSchedule.Id,
               FinanceOptionId = financeOptionSchedule.FinanceOptionId,
               FinanceOptionName = financeOp.Description,
               Date = financeOptionSchedule.Date,
               Cost = financeOptionSchedule.Cost,
               Repayment = financeOptionSchedule.Repayment,
               Disbursement = financeOptionSchedule.Disbursement
       
           }
       );
       
       return Task.FromResult<IActionResult>(Ok(financeOptionSchedules));
       // var financeOptionSchedules = await _unitOfWork.FinanceOptionSchedules.GetAll();
       //   var financeOptionSchedulesDto = _mapper.Map<IEnumerable<FinanceOptionScheduleResponse>>(financeOptionSchedules);
       //      return Ok(financeOptionSchedulesDto);
   }
   
   [HttpGet("{id}")]
   public async Task<IActionResult> GetAFinanceOptionSchedule(int id)
   {
       var financeOptionSchedule = await _unitOfWork.FinanceOptionSchedules.GetById(id);
       var financeOptionScheduleDto = _mapper.Map<FinanceOptionScheduleResponse>(financeOptionSchedule);
       
       if(financeOptionScheduleDto == null)
           return NotFound("FinanceOptionSchedule not found");
       
       return Ok(financeOptionScheduleDto);
   }
   
   [HttpPost]
   public async Task<IActionResult> CreateFinanceOptionSchedule(FinanceOptionScheduleCreateRequest createFinanceOptionScheduleRequest)
   {
       if(!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var financeOptionSchedule = _mapper.Map<FinanceOptionSchedule>(createFinanceOptionScheduleRequest);

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
   public async Task<IActionResult> UpdateFinanceOptionSchedule(int id, UpdateFinanceOptionScheduleRequest updateFinanceOptionScheduleRequest)
   {
       if(!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var financeOptionSchedule = await _unitOfWork.FinanceOptionSchedules.GetById(id);
           if(financeOptionSchedule == null)
               return NotFound("FinanceOptionSchedule not found");
           
           var financeOptionScheduleDto = _mapper.Map<FinanceOptionSchedule>(updateFinanceOptionScheduleRequest);
           
           await _unitOfWork.FinanceOptionSchedules.Update(financeOptionScheduleDto);
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

        await _unitOfWork.FinanceOptionSchedules.Delete(financeOptionSchedule.Id);
        await _unitOfWork.CompleteAsync();
       
       return Ok("FinanceOptionSchedule deleted successfully");
   }
   
}