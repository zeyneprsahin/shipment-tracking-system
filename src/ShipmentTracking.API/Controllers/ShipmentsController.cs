using Microsoft.AspNetCore.Mvc;
using ShipmentTracking.Application.DTOs;
using ShipmentTracking.Application.Services;
using ShipmentTracking.Domain.Enums;

namespace ShipmentTracking.API.Controllers;

[ApiController]
[Route("api/shipments")]
public sealed class ShipmentsController : ControllerBase
{
    private readonly ShipmentService _service;

    public ShipmentsController(ShipmentService service) => _service = service;

    [HttpPost]
    public async Task<ActionResult<ShipmentDto>> Create(CreateShipmentRequest request, CancellationToken cancellationToken)
    {
        var shipment = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetByTrackingNumber), new { trackingNumber = shipment.TrackingNumber }, shipment);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ShipmentDto>>> GetAll([FromQuery] ShipmentStatus? status, CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(status, cancellationToken));

    [HttpGet("{trackingNumber}")]
    public async Task<ActionResult<ShipmentDto>> GetByTrackingNumber(string trackingNumber, CancellationToken cancellationToken)
        => Ok(await _service.GetByTrackingNumberAsync(trackingNumber, cancellationToken));

    [HttpPut("{trackingNumber}/status")]
    public async Task<ActionResult<ShipmentDto>> ChangeStatus(string trackingNumber, ChangeShipmentStatusRequest request, CancellationToken cancellationToken)
        => Ok(await _service.ChangeStatusAsync(trackingNumber, request, cancellationToken));
}
