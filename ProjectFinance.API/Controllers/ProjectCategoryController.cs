using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Dtos.Responses.projectcategory;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ProjectCategoryController : BaseController
{
  public ProjectCategoryController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }
  
  [HttpGet("")]
  
  public async Task<IActionResult> GetAllProjectCategories()
  {
    var projectCategories = await _unitOfWork.ProjectCategories.GetAll();
    var projectCategoriesDto = _mapper.Map<IEnumerable<CommonResponse>>(projectCategories);
    
    return Ok(projectCategoriesDto);
  }
  
  [HttpGet("{id}")]
  
  public async Task<IActionResult> GetAProjectCategory(int id)
  {
    var projectCategory = await _unitOfWork.ProjectCategories.GetById(id);
    var projectCategoryDto = _mapper.Map<CommonResponse>(projectCategory);
    
    if(projectCategoryDto == null)
      return NotFound("ProjectCategory not found");
    
    return Ok(projectCategoryDto);
  }

  [HttpPost]

  public async Task<IActionResult> CreateProjectCategory(CommonCreateRequest createProjectCategoryRequest)
  {
    if (!ModelState.IsValid)
      return BadRequest("Invalid data provided");

    try
    {
      var projectCategory = _mapper.Map<ProjectCategory>(createProjectCategoryRequest);

      await _unitOfWork.ProjectCategories.Add(projectCategory);
      await _unitOfWork.CompleteAsync();
      
      return Ok("ProjectCategory created successfully");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateProjectCategory(int id, CommonUpdateRequest updateProjectCategoryRequest)
  {
    if (!ModelState.IsValid)
      return BadRequest("Invalid data provided");

    try
    {
     
      var projectCategoryInDb = await _unitOfWork.ProjectCategories.GetById(id);
      
      if (projectCategoryInDb == null)
        return BadRequest("ProjectCategory does not exist");
      
      var projectCategory = _mapper.Map<ProjectCategory>(updateProjectCategoryRequest);
      
      await _unitOfWork.ProjectCategories.Update(projectCategory);
      await _unitOfWork.CompleteAsync();
      
      return Ok("ProjectCategory updated successfully");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteProjectCategory(int id)
  {
    var projectCategory = await _unitOfWork.ProjectCategories.GetById(id);
    
    if (projectCategory == null)
      return NotFound("ProjectCategory not found");
    
    await _unitOfWork.ProjectCategories.Delete(id);
    await _unitOfWork.CompleteAsync();
    
    return Ok("ProjectCategory deleted successfully");
  }

}
