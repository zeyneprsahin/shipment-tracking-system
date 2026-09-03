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

    [HttpGet("{trackingNumber}")]
    public async Task<ActionResult<CustomerTrackingDto>> Track(
    string trackingNumber,
    CancellationToken cancellationToken)
    => Ok(await _service.GetCustomerTrackingAsync(
        trackingNumber,
        cancellationToken));
}
