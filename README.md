# Insurance API

ASP.NET Core Web API for creating a customer, pricing an insurance product, issuing a quote, and turning an accepted quote into a policy.

Data is stored in memory. Restarting the API clears customers, quotes, and policies.

## Run

From the repo root:

```powershell
dotnet run --launch-profile http
```

Swagger UI: [http://localhost:5141/swagger](http://localhost:5141/swagger)

The HTTPS profile serves Swagger at [https://localhost:7247/swagger](https://localhost:7247/swagger).

Enum values are JSON strings, for example `"Car"` and `"Draft"`.

## Intended flow

1. Create a customer.
2. Optionally ask for a price without saving a quote.
3. Create a quote. It starts as `Draft`, with an expiration date one year after the effective date.
4. Change the quote while it is still `Draft`, if you need to.
5. Accept the quote. Status becomes `Quoted`.
6. Create a policy from that quote. Status becomes `Issued`.

A quote can be updated or accepted only while it is `Draft`. A policy can be created only from a quote that is `Quoted`.

## Shared insurance object

Quote, update, and price requests send an `insuranceRequest`. Send `insuranceType` plus the fields for that product.

Supported types are `Accident`, `Car`, `House`, `Pet`, `Spacecraft`, and `Travel`. `Motorcycle` is rejected.

```json
{
  "insuranceType": "Car",
  "carKilometersDriven": 120000,
  "houseYearBuilt": null,
  "houseArea": null,
  "petName": null,
  "petSpecies": null,
  "petDateOfBirth": null,
  "travelDurationDays": null,
  "spacecraftYearlyFlightHours": null
}
```

| Type | Fields | Base price |
| --- | --- | --- |
| `Accident` | none | 400 |
| `Car` | `carKilometersDriven` | 800 |
| `House` | `houseYearBuilt`, `houseArea` | 1500 |
| `Pet` | `petName`, `petSpecies`, `petDateOfBirth` | 300 |
| `Travel` | `travelDurationDays` | 350 |
| `Spacecraft` | `spacecraftYearlyFlightHours` | 3500 |

`petName` is stored and returned. It does not change the price.

Create, update, and accept quote, and create policy, return this insurance shape. Unused product fields are `null`. The spacecraft value is returned as `spacecraftFlightHours`.

Get quote and get policy return the stored product as `insuranceType` and `basePrice` only.

## Pricing

Age is applied first, then the product rules. Under 25 multiplies the base price by 1.5. Over 60 multiplies it by 1.2. A missing product field skips that surcharge.

Only the highest matching band applies. House area and building age stack. Pet age and species stack.

| Product | Surcharge |
| --- | --- |
| Car kilometers | 100,000 ×1.10, 150,000 ×1.15, 200,000 ×1.20 |
| House area (m²) | 80 ×1.05, 150 ×1.15, 250 ×1.30 |
| House age (years) | 25 ×1.10, 45 ×1.15, 75 ×1.25 |
| Pet age | under 1 ×1.15, 7 ×1.20, 10 ×1.40 |
| Pet species | `dog` ×1.20, `horse` ×1.50 |
| Accident occupation | driver, farmer, mechanic, police, electrician ×1.15; construction, miner, mining, pilot, firefighter, fisher, offshore, roofer ×1.35 |
| Travel days | 14 ×1.10, 30 ×1.25, 90 ×1.50 |
| Spacecraft hours per year | 5,000 ×1.10, 10,000 ×1.20, 20,000 ×1.30 |

Occupation matching is case-insensitive and looks for those words inside the occupation text.

## Rules

- A customer must be at least 18, and `customerId` must be unique.
- `effectiveDate` cannot be in the past. `expirationDate` must be after `effectiveDate`.
- Spacecraft insurance requires the customer to be at least 20.
- Accident insurance requires the customer to be 79 or younger.

## Customers

### Create customer

`POST /customer`

```json
{
  "customerId": "C001",
  "name": "John Doe",
  "dateOfBirth": "2000-04-12",
  "occupation": "Teacher",
  "yearsOfDrivingExperience": 8,
  "numberOfDrivingfAccidents": 0
}
```

`customerId`, `name`, and `dateOfBirth` are required. The property name `numberOfDrivingfAccidents` is spelled that way in the API.

`201 Created`

```json
{
  "customerId": "C001",
  "name": "John Doe",
  "dateOfBirth": "2000-04-12",
  "occupation": "Teacher",
  "yearsOfDrivingExperience": 8,
  "numberOfDrivingfAccidents": 0
}
```

### List customers

`GET /customer`

`200 OK` with an array of customer objects.

### Get customer

`GET /customer/{customerId}`

`200 OK` with one customer object. Unknown ids return `404` and the text `Customer not found`.

### Update customer

`PUT /customer/{customerId}`

```json
{
  "name": "John Smith",
  "dateOfBirth": "2000-04-12"
}
```

`name` and `dateOfBirth` are optional. An omitted value keeps the current one. Occupation, driving experience, and accident count can be sent, but this call does not save them. `204 No Content`.

## Price

`POST /price?customerId=C001`

The customer id is a query parameter. The body is an insurance request.

```json
{
  "insuranceType": "Car",
  "carKilometersDriven": 120000
}
```

`200 OK`

```json
880
```

For this customer (age 26) the car base price is 800, and 120,000 km applies ×1.10. Unknown customers return `404`.

## Quotes

### Create quote

`POST /quote`

```json
{
  "customerId": "C001",
  "effectiveDate": "2026-11-01",
  "insuranceRequest": {
    "insuranceType": "Car",
    "carKilometersDriven": 120000
  }
}
```

`customerId`, `effectiveDate`, and `insuranceRequest.insuranceType` are required. The customer must already exist.

`201 Created`

```json
{
  "quoteId": "3f1c2a0e-7b6d-4e1a-9c4f-2a8b6d0e1f23",
  "customer": {
    "customerId": "C001",
    "name": "John Doe",
    "dateOfBirth": "2000-04-12",
    "occupation": "Teacher",
    "yearsOfDrivingExperience": 8,
    "numberOfDrivingfAccidents": 0
  },
  "effectiveDate": "2026-11-01",
  "expirationDate": "2027-11-01",
  "insurance": {
    "insuranceType": "Car",
    "houseYearBuilt": null,
    "houseArea": null,
    "petName": null,
    "petSpecies": null,
    "petDateOfBirth": null,
    "carKilometersDriven": 120000,
    "travelDurationDays": null,
    "spacecraftFlightHours": null
  },
  "price": 880
}
```

This response does not include `quoteStatus`. The stored status is `Draft`. Copy `quoteId` for the later calls.

### List quotes

`GET /quote`

`200 OK` with an array of stored quotes. Each item has the shape below, including `quoteStatus` and `createdAt`.

### Get quote

`GET /quote/{quoteId}`

`200 OK`

```json
{
  "quoteId": "3f1c2a0e-7b6d-4e1a-9c4f-2a8b6d0e1f23",
  "customer": {
    "customerId": "C001",
    "name": "John Doe",
    "dateOfBirth": "2000-04-12",
    "occupation": "Teacher",
    "yearsOfDrivingExperience": 8,
    "numberOfDrivingfAccidents": 0
  },
  "effectiveDate": "2026-11-01",
  "expirationDate": "2027-11-01",
  "price": 880,
  "insurance": {
    "insuranceType": "Car",
    "basePrice": 800
  },
  "quoteStatus": "Draft",
  "createdAt": "2026-10-18T13:00:00Z"
}
```

Unknown ids return `404` and the text `Quote not found`.

### Update quote

`PUT /quote/{quoteId}`

Only a `Draft` quote can be updated. Every field is optional. Send `insuranceRequest` only when replacing the whole product. The price is calculated again.

```json
{
  "effectiveDate": "2026-12-01",
  "expirationDate": "2027-12-01",
  "insuranceRequest": {
    "insuranceType": "House",
    "houseYearBuilt": 1960,
    "houseArea": 160
  }
}
```

`200 OK` with the same quote summary shape as create. For this house, area is in the 150 m² band (×1.15) and the building is older than 45 years (×1.15), so the price is 1500 × 1.15 × 1.15 = 1983.75 when the customer has no age surcharge.

### Accept quote

`PUT /quote/{quoteId}/accept`

No body. The quote must be `Draft`. Status becomes `Quoted`.

`200 OK` with the quote summary. That body still has no `quoteStatus`. `GET /quote/{quoteId}` returns `"quoteStatus": "Quoted"`.

### Delete quote

`DELETE /quote/{quoteId}`

`204 No Content`.

## Policies

### Create policy

`POST /policy`

The quote must be `Quoted`. Creating the policy sets that quote to `Issued`.

```json
{
  "quoteId": "3f1c2a0e-7b6d-4e1a-9c4f-2a8b6d0e1f23"
}
```

`201 Created`

```json
{
  "policyNumber": 1,
  "customer": {
    "customerId": "C001",
    "name": "John Doe",
    "dateOfBirth": "2000-04-12",
    "occupation": "Teacher",
    "yearsOfDrivingExperience": 8,
    "numberOfDrivingfAccidents": 0
  },
  "insurance": {
    "insuranceType": "Car",
    "houseYearBuilt": null,
    "houseArea": null,
    "petName": null,
    "petSpecies": null,
    "petDateOfBirth": null,
    "carKilometersDriven": 120000,
    "travelDurationDays": null,
    "spacecraftFlightHours": null
  },
  "effectiveDate": "2026-11-01T00:00:00",
  "expirationDate": "2027-11-01T00:00:00",
  "price": 880
}
```

Policy numbers start at 1 and increase by one for each policy created in the current process.

### List policies

`GET /policy`

`200 OK` with an array of stored policies, in the shape below.

### Get policy

`GET /policy/{policyNumber}`

`200 OK`

```json
{
  "policyNumber": 1,
  "customer": {
    "customerId": "C001",
    "name": "John Doe",
    "dateOfBirth": "2000-04-12",
    "occupation": "Teacher",
    "yearsOfDrivingExperience": 8,
    "numberOfDrivingfAccidents": 0
  },
  "insurance": {
    "insuranceType": "Car",
    "basePrice": 800
  },
  "effectiveDate": "2026-11-01T00:00:00",
  "expirationDate": "2027-11-01T00:00:00",
  "price": 880
}
```

Unknown numbers return `404` and the text `Policy not found`.

## Errors

Missing required JSON fields, a duplicate `customerId`, a customer under 18, a past `effectiveDate`, and an unsupported insurance type return `400` with a message string.

`GET` of an unknown customer, quote, or policy returns `404` with a message string.

Other rule failures — a quote for a customer that does not exist, accepting or issuing a quote in the wrong status, and the spacecraft or accident age limits — currently return `500`.
