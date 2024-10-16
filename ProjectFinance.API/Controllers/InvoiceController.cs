using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Dtos.Responses.invoice;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class InvoiceController : BaseController
   {
       public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
       {
       }
       
       [HttpGet("")]
       
       public async Task<IActionResult> GetAllInvoices()
       {
           var invoices = await _unitOfWork.Invoices.GetAll();
           
           Console.WriteLine(invoices);
           // var invoicesDto = _mapper.Map<IEnumerable<CommonResponse>>(invoices);
           
           return Ok(invoices);
       }
       
       [HttpGet("{id}")]
       
       public async Task<IActionResult> GetAnInvoice(int id)
       {
           var invoice = await _unitOfWork.Invoices.GetById(id);
           var invoiceDto = _mapper.Map<InvoiceResponse>(invoice);
           
           if(invoiceDto == null)
               return NotFound("Invoice not found");
           
           return Ok(invoiceDto);
       }
       
       [HttpPost]
       
       public async Task<IActionResult> CreateInvoice(CommonCreateRequest createInvoiceRequest)
       {
           if(!ModelState.IsValid)
               return BadRequest("Invalid data provided");
   
           try
           {
               var invoice = _mapper.Map<Invoice>(createInvoiceRequest);
   
               if (invoice.SupplierId != null)
               {
                   var invoiceInDb = await _unitOfWork.Invoices.GetById(invoice.Id);
                   if (invoiceInDb != null)
                       return BadRequest("Invoice already exists");
               }
   
               await _unitOfWork.Invoices.Add(invoice);
               await _unitOfWork.CompleteAsync();
           
               return Ok("Invoice created successfully");
               
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               throw;
           }
       }
       
         [HttpPut("{id}")]
         
         public async Task<IActionResult> UpdateInvoice(InvoiceResponse updateInvoiceRequest)
         {
             if (!ModelState.IsValid)
                 return BadRequest("Invalid data provided");
     
             try
             {
                 var invoice = _mapper.Map<Invoice>(updateInvoiceRequest);
     
                 await _unitOfWork.Invoices.Update(invoice);
                 await _unitOfWork.CompleteAsync();
             
                 return Ok("Invoice updated successfully");
                 
             }
             catch (Exception e)
             {
                 Console.WriteLine(e);
                 throw;
             }
         }
         
         [HttpDelete("{id}")]
         
            public async Task<IActionResult> DeleteInvoice(int id)
            {
                var invoice = await _unitOfWork.Invoices.GetById(id);
                if (invoice == null)
                    return NotFound("Invoice not found");
        
                await _unitOfWork.Invoices.Delete(id);
                await _unitOfWork.CompleteAsync();
        
                return Ok("Invoice deleted successfully");
            }
       
}