using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Dtos.Responses.client;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;

public class ClientController : BaseController
{
    public ClientController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllClients()
    {
        var clients = await _unitOfWork.Clients.GetAll();
        var clientsDto = _mapper.Map<IEnumerable<ClientResponse>>(clients);
        
        return Ok(clientsDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAClient(int id)
    {
        var client = await _unitOfWork.Clients.GetById(id);
        var clientDto = _mapper.Map<CommonResponse>(client);
        
        if(clientDto == null)
            return NotFound("Client not found");
        
        return Ok(clientDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateClient(CommonCreateRequest createClientRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var client = _mapper.Map<Client>(createClientRequest);

            if (client.Code != null)
            {
                var clientInDb = await _unitOfWork.Clients.GetByCode(client.Code);
                if (clientInDb != null)
                    return BadRequest("Client already exists");
            }

            await _unitOfWork.Clients.Add(client);
            await _unitOfWork.CompleteAsync();
        
            return Ok("Client created successfully");
            
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while creating the client");
        }
    }
    
    [HttpPut("{id}")]
    
    public async Task<IActionResult> UpdateClient(int id, ClientResponse CommonUpdateRequest)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invalid data provided");

        try
        {
            var client = await _unitOfWork.Clients.GetById(id);

            if (client == null)
                return NotFound("Client not found");

            _mapper.Map(CommonUpdateRequest, client);

            await _unitOfWork.CompleteAsync();
        
            return Ok("Client updated successfully");
            
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while updating the client");
        }
    }
    
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _unitOfWork.Clients.GetById(id);
        
        if(client == null)
            return NotFound("Client not found");

        await _unitOfWork.Clients.Delete(id);
        await _unitOfWork.CompleteAsync();
        
        return Ok("Client deleted successfully");
    }
}