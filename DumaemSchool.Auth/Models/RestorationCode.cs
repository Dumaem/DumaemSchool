namespace DumaemSchool.Auth.Models;

public sealed class RestorationCode
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public int Code { get; set; }

    public DateTime? DateCreated { get; set; }
    
    public Guid SessionId { get; set; }
}