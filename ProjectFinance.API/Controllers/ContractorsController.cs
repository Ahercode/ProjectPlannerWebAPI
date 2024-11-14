using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ContractorsController :BaseController
{
    public ContractorsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> GetContractors()
    {
        var contractors = await _unitOfWork.Contractors.GetAll();
        return Ok(contractors);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetContractor(int id)
    {
        var contractor = await _unitOfWork.Contractors.GetById(id);
        return Ok(contractor);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateContractor(ContractorCreateRequest contractor)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");
        
        var contractorDto = _mapper.Map<Contractor>(contractor);
        
        await _unitOfWork.Contractors.Add(contractorDto);
        await _unitOfWork.CompleteAsync();
        return Ok("Contractor created successfully");
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContractor(int id, ContractorUpdateRequest contractorUpdateRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");
        
        var contractorDto = await _unitOfWork.Contractors.GetById(id);
        
        if (contractorDto == null)
            return NotFound("Contractor not found");
        
        var contractorEntity = _mapper.Map<Contractor>(contractorUpdateRequest);
        
        await _unitOfWork.Contractors.Update(contractorEntity);
        await _unitOfWork.CompleteAsync();
        return Ok("Contractor updated successfully");
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContractor(int id)
    {
        var contractor = await _unitOfWork.Contractors.GetById(id);
        
        if (contractor == null)
            return NotFound("Contractor not found");
        
        await _unitOfWork.Contractors.Delete(contractor.id);
        await _unitOfWork.CompleteAsync();
        return Ok("Contractor deleted successfully");
    }
}