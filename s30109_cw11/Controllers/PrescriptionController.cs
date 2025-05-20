using Microsoft.AspNetCore.Mvc;
using s30109_cw11.DTOs;
using s30109_cw11.Services.Interfaces;

namespace s30109_cw11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PrescriptionController(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequestDto request)
    {
        try
        {
            var result = await _service.AddPrescriptionAsync(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}