using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Responses.staff;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class StaffController : BaseController
{
    public StaffController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    
    public async Task<IActionResult> GetAllStaffs()
    {
        var staffs = await _unitOfWork.Staffs.GetAll();
        var staffsDto = _mapper.Map<IEnumerable<StaffResponse>>(staffs);
        
        return Ok(staffsDto);
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetAStaff(int id)
    {
        var staff = await _unitOfWork.Staffs.GetById(id);
        var staffDto = _mapper.Map<StaffResponse>(staff);
        
        if(staffDto == null)
            return NotFound("Staff not found");
        
        return Ok(staffDto);
    }
    
    [HttpPost]
    
    public async Task<IActionResult> CreateStaff(StaffResponse createStaffRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var staff = _mapper.Map<Staff>(createStaffRequest);

            if (staff.Id != null)
            {
                var staffInDb = await _unitOfWork.Staffs.GetById(staff.Id);
                if (staffInDb != null)
                    return BadRequest("Staff already exists");
            }

            await _unitOfWork.Staffs.Add(staff);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok();
    }
    
    [HttpPut("{id}")]
    
    public async Task<IActionResult> UpdateStaff(int id, StaffResponse updateStaffRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var staff = await _unitOfWork.Staffs.GetById(id);
            if (staff == null)
                return NotFound("Staff not found");

            _mapper.Map(updateStaffRequest, staff);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteStaff(int id)
    {
        var staff = await _unitOfWork.Staffs.GetById(id);
        if (staff == null)
            return NotFound("Staff not found");

        // _unitOfWork.Staffs.Remove(staff);
        await _unitOfWork.CompleteAsync();

        return Ok();
    }
}

   