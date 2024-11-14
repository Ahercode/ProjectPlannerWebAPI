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
               (purchaseOrderProjectActivity, supplier) => new { purchaseOrderProjectActivity, supplier })
           .Join(
               _unitOfWork.CostDetails.GetAll().Result,
               purchaseOrderProjectActivitySupplier => purchaseOrderProjectActivitySupplier.purchaseOrderProjectActivity
                   .purchaseOrderProject.purchaseOrder.CostDetailId,
               costDetail => costDetail.Id,
               (p, costDetail) => new PurchaseOrderResponse()
               {
                    Id = p.purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.Id,
                    ProjectId = p.purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.ProjectId,
                    ProjectName = p.purchaseOrderProjectActivity.purchaseOrderProject.project.Name,
                    ActivityId = p.purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.ActivityId,
                    ActivityName = p.purchaseOrderProjectActivity.activity.Name,
                    CostDetailId = p.purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.CostDetailId,
                    CostDetailName = costDetail.Name,
                    SupplierId = p.purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.SupplierId,
                    SupplierName = p.supplier.Name,
                    Date = p.purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.Date,
                    Amount = p.purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.Amount,
                    PONumber = p.purchaseOrderProjectActivity.purchaseOrderProject.purchaseOrder.PONumber
                    
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

   public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderRequest createPurchaseOrderRequest)
   {
       if (!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var purchaseOrder = _mapper.Map<PurchaseOrder>(createPurchaseOrderRequest);
           
           await _unitOfWork.PurchaseOrders.Add(purchaseOrder);
           await _unitOfWork.CompleteAsync();
       }
       catch (Exception e)
       {
           return BadRequest(e.Message);
       }

       return Ok();
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