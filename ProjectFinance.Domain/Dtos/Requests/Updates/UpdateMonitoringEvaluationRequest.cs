using Microsoft.AspNetCore.Mvc;

namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateMonitoringEvaluationRequest 
{
   public int Id { get; set; }
   
   public string? Stakeholder { get; set; }
}