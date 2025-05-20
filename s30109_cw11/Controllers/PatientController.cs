using Microsoft.AspNetCore.Mvc;
using s30109_cw11.Services.Interfaces;

namespace s30109_cw11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _service;

    public PatientController(IPatientService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var result = await _service.GetPatientDetailsAsync(id);
        return result == null ? NotFound() : Ok(result);
    }
}