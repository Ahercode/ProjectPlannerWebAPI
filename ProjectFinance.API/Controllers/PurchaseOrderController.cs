using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses.purchaseorder;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class PurchaseOrderController : BaseController
{
   public PurchaseOrderController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
   {
   }
   
   [HttpGet("")]
   
   public Task<IActionResult> GetAllPurchaseOrders()
   {
       var purchaseOrders =  _unitOfWork.PurchaseOrders.GetAll().Result.Join(
               _unitOfWork.Projects.GetAll().Result,
               purchaseOrder => purchaseOrder.ProjectId,
               project => project.Id,
               (purchaseOrder, project) => new { purchaseOrder, project })
           .Join(
               _unitOfWork.Activities.GetAll().Result,
               purchaseOrderProject => purchaseOrderProject.purchaseOrder.ActivityId,
               activity => activity.Id,
               (purchaseOrderProject, activity) => new { purchaseOrderProject, activity })
           .Join(
               _unitOfWork.Suppliers.GetAll().Result,
               purchaseOrderProjectActivity =>
                   purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.SupplierId,
               supplier => supplier.Id,
               (purchaseOrderProjectActivity, supplier) => new PurchaseOrderResponse()
               {
                    Id = purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.Id,
                    ProjectId = purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.ProjectId,
                    ProjectName = purchaseOrderProjectActivity.purchaseOrderProject.project.Name,
                    ActivityId = purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.ActivityId,
                    ActivityName = purchaseOrderProjectActivity.activity.Name,
                    SupplierId = purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.SupplierId,
                    SupplierName = supplier.Name,
                    Date = purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.Date,
                    PONumber = purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.PONumber,
                    Reference = purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.Reference
                    
               });
       
       return Task.FromResult<IActionResult>(Ok(purchaseOrders));
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetAPurchaseOrder(int id)
   {
       var purchaseOrder = await _unitOfWork.PurchaseOrders.GetById(id);
       var purchaseOrderDto = _mapper.Map<PurchaseOrderResponse>(purchaseOrder);

       if (purchaseOrderDto == null)
           return NotFound("PurchaseOrder not found");

       return Ok(purchaseOrderDto);

   }

   [HttpPost]

   public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderCreateRequest createPurchaseOrderCreateRequest)
   {
       if (!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var purchaseOrder = _mapper.Map<PurchaseOrder>(createPurchaseOrderCreateRequest);
           
           await _unitOfWork.PurchaseOrders.Add(purchaseOrder);
           await _unitOfWork.CompleteAsync();
           
           return Ok("PurchaseOrder created successfully");
       }
       catch (Exception e)
       {
           return BadRequest(e.Message);
       }
   }
   
   [HttpPut("{id}")]
   
   public async Task<IActionResult> UpdatePurchaseOrder(int id, UpdatePurchaseOrderRequest updatePurchaseOrderRequest)
   {
       if (!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var purchaseOrder = await _unitOfWork.PurchaseOrders.GetById(id);
           if (purchaseOrder == null)
               return NotFound("PurchaseOrder not found");

           var updatedPurchaseOrder = _mapper.Map<PurchaseOrder>(updatePurchaseOrderRequest);
           
           await _unitOfWork.PurchaseOrders.Update(updatedPurchaseOrder);
           await _unitOfWork.CompleteAsync();
       }
       catch (Exception e)
       {
           return BadRequest(e.Message);
       }

       return Ok();
   }
   
   [HttpDelete("{id}")]
   
   public async Task<IActionResult> DeletePurchaseOrder(int id)
   {
       try
       {
           var purchaseOrder = await _unitOfWork.PurchaseOrders.GetById(id);
           if (purchaseOrder == null)
               return NotFound("PurchaseOrder not found");

           await _unitOfWork.PurchaseOrders.Delete(id);
           await _unitOfWork.CompleteAsync();
       }
       catch (Exception e)
       {
           return BadRequest(e.Message);
       }

       return Ok();
   }
}