namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateStaffRequest
{
    public int Id { get; set; }
    
    public string StaffID { get; set; }
    
    public string Address { get; set; }
}