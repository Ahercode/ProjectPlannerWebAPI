// using AutoMapper;
// using Microsoft.AspNetCore.Mvc;
// using ProjectFinance.Domain.Dtos.Responses.monitoringevaluation;
// using ProjectFinance.Domain.Entities;
// using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;
//
// namespace ProjectFinance.API.Controllers;
//
// public class MonitoringEvaluationController : BaseController
// {
//     public MonitoringEvaluationController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
//     {
//     }
//     
//     [HttpGet("")]
//     
//     public async Task<IActionResult> GetAllMonitoringEvaluations()
//     {
//         var monitoringEvaluations = await _unitOfWork.MonitoringEvaluation.GetAll();
//         var monitoringEvaluationsDto = _mapper.Map<IEnumerable<MonitoringEvaluationResponse>>(monitoringEvaluations);
//         
//         return Ok(monitoringEvaluationsDto);
//     }
//     
//     [HttpGet("{id}")]
//     
//     public async Task<IActionResult> GetAMonitoringEvaluation(int id)
//     {
//         var monitoringEvaluation = await _unitOfWork.MonitoringEvaluations.GetById(id);
//         var monitoringEvaluationDto = _mapper.Map<MonitoringEvaluationResponse>(monitoringEvaluation);
//         
//         if(monitoringEvaluationDto == null)
//             return NotFound("Monitoring Evaluation not found");
//         
//         return Ok(monitoringEvaluationDto);
//     }
//     
//     [HttpPost]
//     
//     public async Task<IActionResult> CreateMonitoringEvaluation(MonitoringEvaluationResponse createMonitoringEvaluationRequest)
//     {
//         if(!ModelState.IsValid)
//             return BadRequest("Invalid data provided");
//
//         try
//         {
//             var monitoringEvaluation = _mapper.Map<MonitoringEvaluation>(createMonitoringEvaluationRequest);
//
//             if (monitoringEvaluation.Id != null)
//             {
//                 var monitoringEvaluationInDb = await _unitOfWork.MonitoringEvaluations.GetById(monitoringEvaluation.Id);
//                 if (monitoringEvaluationInDb != null)
//                     return BadRequest("Monitoring Evaluation already exists");
//             }
//
//             await _unitOfWork.MonitoringEvaluations.Add(monitoringEvaluation);
//             await _unitOfWork.CompleteAsync();
//         
//             return Ok("Monitoring Evaluation created successfully");
//         }
//         catch (Exception e)
//         {
//             return BadRequest(e.Message);
//         }
//     }
//     
//     [HttpPut]
//     
//     public async Task<IActionResult> UpdateMonitoringEvaluation(MonitoringEvaluationResponse updateMonitoringEvaluationRequest)
//     {
//         if(!ModelState.IsValid)
//             return BadRequest("Invalid data provided");
//
//         try
//         {
//             var monitoringEvaluation = _mapper.Map<MonitoringEvaluation>(updateMonitoringEvaluationRequest);
//
//             var monitoringEvaluationInDb = await _unitOfWork.MonitoringEvaluations.GetById(monitoringEvaluation.Id);
//             if (monitoringEvaluationInDb == null)
//                 return BadRequest("Monitoring Evaluation does not exist");
//
//             await _unitOfWork.MonitoringEvaluations.Update(monitoringEvaluation);
//             await _unitOfWork.CompleteAsync();
//         
//             return Ok("Monitoring Evaluation updated successfully");
//         }
//         catch (Exception e)
//         {
//             return BadRequest(e.Message);
//         }
//     }
//     
//     [HttpDelete("{id}")]
//     
//     public async Task<IActionResult> DeleteMonitoringEvaluation(int id)
//     {
//         try
//         {
//             var monitoringEvaluation = await _unitOfWork.MonitoringEvaluations.GetById(id);
//             if (monitoringEvaluation == null)
//                 return BadRequest("Monitoring Evaluation does not exist");
//
//             await _unitOfWork.MonitoringEvaluations.Delete(id);
//             await _unitOfWork.CompleteAsync();
//         
//             return Ok("Monitoring Evaluation deleted successfully");
//         }
//         catch (Exception e)
//         {
//             return BadRequest(e.Message);
//         }
//     }
// }