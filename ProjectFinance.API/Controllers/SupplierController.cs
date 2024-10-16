using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Responses.supplier;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class SupplierController : BaseController
{
 public SupplierController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
 {
 }
 
 [HttpGet("")]
 
 public async Task<IActionResult> GetAllSuppliers()
 {
     var suppliers = await _unitOfWork.Suppliers.GetAll();
     var suppliersDto = _mapper.Map<IEnumerable<SupplierResponse>>(suppliers);
     
     return Ok(suppliersDto);
 }
 
 [HttpGet("{id}")]
 
 public async Task<IActionResult> GetASupplier(int id)
 {
     var supplier = await _unitOfWork.Suppliers.GetById(id);
     var supplierDto = _mapper.Map<SupplierResponse>(supplier);
     
     if(supplierDto == null)
         return NotFound("Supplier not found");
     
     return Ok(supplierDto);
 }

 [HttpPost]

 public async Task<IActionResult> CreateSupplier(SupplierResponse createSupplierRequest)
 {
     if (!ModelState.IsValid)
         return BadRequest("Invalid data provided");

     try
     {
         var supplier = _mapper.Map<Supplier>(createSupplierRequest);

         if (supplier.Id != null)
         {
             var supplierInDb = await _unitOfWork.Suppliers.GetById(supplier.Id);
             if (supplierInDb != null)
                 return BadRequest("Supplier already exists");
         }

         await _unitOfWork.Suppliers.Add(supplier);
         await _unitOfWork.CompleteAsync();
     }
     catch (Exception e)
     {
         return BadRequest(e.Message);
     }

     return Ok();
 }
 
 [HttpPut("{id}")]
 
 public async Task<IActionResult> UpdateSupplier(int id, SupplierResponse updateSupplierRequest)
 {
     if (!ModelState.IsValid)
         return BadRequest("Invalid data provided");

     try
     {
         var supplier = await _unitOfWork.Suppliers.GetById(id);
         if (supplier == null)
             return NotFound("Supplier not found");

         _mapper.Map(updateSupplierRequest, supplier);
         await _unitOfWork.CompleteAsync();
     }
     catch (Exception e)
     {
         return BadRequest(e.Message);
     }

     return Ok();
 }
 
 [HttpDelete("{id}")]
 
 public async Task<IActionResult> DeleteSupplier(int id)
 {
     try
     {
         var supplier = await _unitOfWork.Suppliers.GetById(id);
         if (supplier == null)
             return NotFound("Supplier not found");

         await _unitOfWork.Suppliers.Delete(id);
         await _unitOfWork.CompleteAsync();
     }
     catch (Exception e)
     {
         return BadRequest(e.Message);
     }

     return Ok();
 }
 
}