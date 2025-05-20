namespace s30109_cw11.Models;

public class Doctor
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    
    public ICollection<Prescription> Perscriptions { get; set; }
}