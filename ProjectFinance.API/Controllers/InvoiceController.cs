using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
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
       try
       {
           var invoiceAll = await _unitOfWork.Invoices.GetAll();
           var invoices =  invoiceAll.Join(
               _unitOfWork.PurchaseOrders.GetAll().Result,
               invoice => invoice.PurchaseOrderId,
               purchaseOrder => purchaseOrder.Id,
               (invoice, purchaseOrder) => new { invoice, purchaseOrder }
           ).Join(
               _unitOfWork.Projects.GetAll().Result,
               invoicePurchaseOrder => invoicePurchaseOrder.purchaseOrder.ProjectId,
               project => project.Id,
               (invoicePurchaseOrder, project) => new {invoicePurchaseOrder, project}).Join(
               _unitOfWork.Suppliers.GetAll().Result,
                 invoicePurchaseOrderProject => invoicePurchaseOrderProject.invoicePurchaseOrder.invoice.SupplierId,
               suplier => suplier.Id,
                 (invoicePurchaseOrderProject, suplier) => new InvoiceResponse
                 {
                      Id = invoicePurchaseOrderProject.invoicePurchaseOrder.invoice.Id,
                      SupplierId = invoicePurchaseOrderProject.invoicePurchaseOrder.invoice.SupplierId,
                      SupplierName = suplier.Name,
                      PurchaseOrderId = invoicePurchaseOrderProject.invoicePurchaseOrder.invoice.PurchaseOrderId,
                      PurchaseOrderNumber = invoicePurchaseOrderProject.invoicePurchaseOrder.purchaseOrder.PONumber,
                      InvoiceNumber = invoicePurchaseOrderProject.invoicePurchaseOrder.invoice.InvoiceNumber,
                      ProjectId = invoicePurchaseOrderProject.project.Id,
                      ProjectName = invoicePurchaseOrderProject.project.Name
                 });
           
            return Ok(invoices);
       }
       catch (Exception e)
       {
           Console.WriteLine(e);
           throw;
       }
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
   public async Task<IActionResult> CreateInvoice(InvoiceCreateRequest createInvoiceRequest)
   {
       if(!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var invoice = _mapper.Map<Invoice>(createInvoiceRequest);
           
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
     public async Task<IActionResult> UpdateInvoice(int id, UpdateInvoiceRequest updateInvoiceRequest)
     {
         if (!ModelState.IsValid)
             return BadRequest("Invalid data provided");

         try
         {
             var invoiceToUpdate = await _unitOfWork.Invoices.GetById(id);
                if (invoiceToUpdate == null)
                    return NotFound("Invoice not found");
                
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