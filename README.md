# Shipment Tracking System

A small .NET 8 Web API for managing shipments and their status transitions. The project follows the approved Clean Architecture design: Domain, Application, Infrastructure, API and Tests.

## Main features
- Create shipments with required recipient/package fields
- Generate a unique tracking number automatically
- List all shipments or filter by status
- Search by tracking number
- Change status only through the domain whitelist
- Store every status change with user and UTC time
- Customer read-only tracking endpoint
- Central exception handling: 400 / 404 / 409 / 500
- Domain unit tests for the core status-transition rules

## Run
Requirements: .NET 8 SDK.

```bash
dotnet restore
dotnet run --project src/ShipmentTracking.API
```

Swagger opens at:
`http://localhost:5094/swagger`

The project uses SQLite for a simple local first version. The database file is created automatically as `shipment-tracking.db`.

## Important endpoints
- `POST /api/shipments`
- `GET /api/shipments`
- `GET /api/shipments?status=Delivered`
- `GET /api/shipments/{trackingNumber}`
- `PUT /api/shipments/{trackingNumber}/status`
- `GET /api/tracking/{trackingNumber}` (customer read-only)

## Example create body
```json
{
  "recipientName": "Ali Yilmaz",
  "address": "Aydin, Turkey",
  "phoneNumber": "05550000000",
  "packageInfo": "Small box",
  "email": null,
  "createdBy": "employee-1"
}
```

## Example status change body
```json
{
  "newStatus": "Shipped",
  "changedBy": "employee-1"
}
```

## Test
```bash
dotnet test
```
