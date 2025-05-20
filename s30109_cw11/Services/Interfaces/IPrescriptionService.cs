using s30109_cw11.DTOs;
using s30109_cw11.Models;

namespace s30109_cw11.Services.Interfaces;

public interface IPrescriptionService
{
    Task<string> AddPrescriptionAsync(PrescriptionRequestDto request);
}