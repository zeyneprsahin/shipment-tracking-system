using Microsoft.AspNetCore.Mvc;
using ShipmentTracking.Application.DTOs;
using ShipmentTracking.Application.Services;

namespace ShipmentTracking.API.Controllers;

[ApiController]
[Route("api/tracking")]
public sealed class CustomerTrackingController : ControllerBase
{
    private readonly ShipmentService _service;

    public CustomerTrackingController(ShipmentService service) => _service = service;

    // Customer side intentionally exposes only read-by-tracking-number.
    [HttpGet("{trackingNumber}")]
    public async Task<ActionResult<ShipmentDto>> Track(string trackingNumber, CancellationToken cancellationToken)
        => Ok(await _service.GetByTrackingNumberAsync(trackingNumber, cancellationToken));
}
