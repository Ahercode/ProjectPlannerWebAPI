using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
   
   public async Task<IActionResult> GetAllPurchaseOrders()
   {
       var purchaseOrders = await _unitOfWork.PurchaseOrders.GetAll();
       var purchaseOrdersDto = _mapper.Map<IEnumerable<PurchaseOrderResponse>>(purchaseOrders);
       
       return Ok(purchaseOrdersDto);
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

   public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderResponse createPurchaseOrderRequest)
   {
       if (!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var purchaseOrder = _mapper.Map<PurchaseOrder>(createPurchaseOrderRequest);

           if (purchaseOrder.Id != null)
           {
               var purchaseOrderInDb = await _unitOfWork.PurchaseOrders.GetById(purchaseOrder.Id);
               if (purchaseOrderInDb != null)
                   return BadRequest("PurchaseOrder already exists");
           }

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
   
   public async Task<IActionResult> UpdatePurchaseOrder(int id, PurchaseOrderResponse updatePurchaseOrderRequest)
   {
       if (!ModelState.IsValid)
           return BadRequest("Invalid data provided");

       try
       {
           var purchaseOrder = await _unitOfWork.PurchaseOrders.GetById(id);
           if (purchaseOrder == null)
               return NotFound("PurchaseOrder not found");

           _mapper.Map(updatePurchaseOrderRequest, purchaseOrder);
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