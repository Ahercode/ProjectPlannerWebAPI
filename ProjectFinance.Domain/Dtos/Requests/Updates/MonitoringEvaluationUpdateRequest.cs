using Microsoft.AspNetCore.Mvc;

namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class MonitoringEvaluationUpdateRequest 
{
   public int Id { get; set; }

   public int? ActivityId { get; set; }

   public string? Note { get; set; }
   
   public string? workDone { get; set; }

   public int? ProjectId { get; set; }
}