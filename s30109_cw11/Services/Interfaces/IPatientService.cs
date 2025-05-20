namespace s30109_cw11.Services.Interfaces;

public interface IPatientService
{
    Task<object?> GetPatientDetailsAsync(int id);
}