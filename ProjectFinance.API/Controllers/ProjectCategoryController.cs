using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    var projectCategoriesDto = _mapper.Map<IEnumerable<ProjectCategoryResponse>>(projectCategories);
    
    return Ok(projectCategoriesDto);
  }
  
  [HttpGet("{id}")]
  
  public async Task<IActionResult> GetAProjectCategory(int id)
  {
    var projectCategory = await _unitOfWork.ProjectCategories.GetById(id);
    var projectCategoryDto = _mapper.Map<ProjectCategoryResponse>(projectCategory);
    
    if(projectCategoryDto == null)
      return NotFound("ProjectCategory not found");
    
    return Ok(projectCategoryDto);
  }

  [HttpPost]

  public async Task<IActionResult> CreateProjectCategory(ProjectCategoryResponse createProjectCategoryRequest)
  {
    if (!ModelState.IsValid)
      return BadRequest("Invalid data provided");

    try
    {
      var projectCategory = _mapper.Map<ProjectCategory>(createProjectCategoryRequest);

      if (projectCategory.Id != null)
      {
        var projectCategoryInDb = await _unitOfWork.ProjectCategories.GetById(projectCategory.Id);
        if (projectCategoryInDb != null)
          return BadRequest("ProjectCategory already exists");
      }

      await _unitOfWork.ProjectCategories.Add(projectCategory);
      await _unitOfWork.CompleteAsync();
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }

    return Ok();
  }
  
  [HttpPut]
  
  public async Task<IActionResult> UpdateProjectCategory(ProjectCategoryResponse updateProjectCategoryRequest)
  {
    if (!ModelState.IsValid)
      return BadRequest("Invalid data provided");

    try
    {
      var projectCategory = _mapper.Map<ProjectCategory>(updateProjectCategoryRequest);

      if (projectCategory.Id == null)
        return BadRequest("ProjectCategory does not exist");

      var projectCategoryInDb = await _unitOfWork.ProjectCategories.GetById(projectCategory.Id);
      if (projectCategoryInDb == null)
        return BadRequest("ProjectCategory does not exist");

      _unitOfWork.ProjectCategories.Update(projectCategory);
      await _unitOfWork.CompleteAsync();
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }

    return Ok();
  }
  
  [HttpDelete("{id}")]
  
  public async Task<IActionResult> DeleteProjectCategory(int id)
  {
    var projectCategory = await _unitOfWork.ProjectCategories.GetById(id);
    
    if (projectCategory == null)
      return NotFound("ProjectCategory not found");
    
    await _unitOfWork.ProjectCategories.Delete(id);
    await _unitOfWork.CompleteAsync();
    
    return Ok();
  }

}
