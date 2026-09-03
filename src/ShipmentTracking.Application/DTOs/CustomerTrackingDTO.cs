using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentTracking.Domain.Enums;

namespace ShipmentTracking.Application.DTOs;

public sealed record CustomerTrackingDto(
    string TrackingNumber,
    ShipmentStatus Status,
    DateTime LastUpdatedAtUtc);

//müşterinin takip için ihtiyacı olanlar, ilk sürüm için