using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses.payment;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class PaymentController : BaseController
{
        public PaymentController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        
        [HttpGet("")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _unitOfWork.Payments.GetAll();
            var paymentsDto = _mapper.Map<IEnumerable<PaymentResponse>>(payments);
            return Ok(paymentsDto);
        }
        
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetAPayment(int id)
        {
            var payment = await _unitOfWork.Payments.GetById(id);
            var paymentDto = _mapper.Map<PaymentResponse>(payment);
            
            if(paymentDto == null)
                return NotFound("Payment not found");
            
            return Ok(paymentDto);
        }
        
        [HttpPost]
        
        public async Task<IActionResult> CreatePayment(PaymentCreateRequest createPaymentRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest("Invalid data provided");
    
            try
            {
                var payment = _mapper.Map<Payment>(createPaymentRequest);
    
                if (payment.Id != null)
                {
                    var paymentInDb = await _unitOfWork.Payments.GetById(payment.Id);
                    if (paymentInDb != null)
                        return BadRequest("Payment already exists");
                }
    
                await _unitOfWork.Payments.Add(payment);
                await _unitOfWork.CompleteAsync();
            
                return Ok("Payment created successfully");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentRequest updatePaymentRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided");
    
            try
            {
                var paymentInDb = await _unitOfWork.Payments.GetById(id);
                if (paymentInDb == null)
                    return BadRequest("Payment does not exist");
                
                var payment = _mapper.Map<Payment>(updatePaymentRequest);
    
                await _unitOfWork.Payments.Update(payment);
                await _unitOfWork.CompleteAsync();
                
                return Ok("Payment updated successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeletePayment(int id)
    {
        var payment = await _unitOfWork.Payments.GetById(id);
        
        if(payment == null)
            return NotFound("Payment not found");
        
        await _unitOfWork.Payments.Delete(id);
        await _unitOfWork.CompleteAsync();
        
        return Ok("Payment deleted successfully");
    }
        
}