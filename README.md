A simple ASP.NET Core Web API for managing customers, insurance quotes, and policies.

This project is built for learning backend architecture, service design, and REST API patterns in .NET.

---

## 📌 Features

- Create and manage customers
- Generate insurance quotes
- Update quotes (with validation rules)
- Issue policies from approved quotes
- Price calculation based on:
  - Insurance type
  - Customer age
- Strict validation and business rules

---

## 🏗️ Architecture


Controllers → Services → Models
↓
In-memory storage


### Layers

- **Controllers**: HTTP endpoints and response handling
- **Services**: Business logic and orchestration
- **Models**: Domain entities (Customer, Quote, Policy)
- **Storage**: Static in-memory collections (temporary)

---

## 🔍 Design Patterns

### Find vs Get pattern

| Method   | Behavior |
|----------|----------|
| FindX()  | Returns null if not found |
| GetX()   | Throws exception if not found |

Used to separate:
- API-level handling (Find)
- Internal logic safety (Get)

---

## 🔄 Quote Lifecycle


Created → Quoted → Issued


- Quotes are created for a customer
- Can be updated only in `Quoted` state
- Converted into policies when issued

---

# 📡 API Endpoints

## Customers

### Get all customers

```http
GET /insurance/customer
```

**Response**
```json
[
  {
    "customerId": "C001",
    "name": "John Doe",
    "dateOfBirth": "2000-04-12"
  },
  {
    "customerId": "C002",
    "name": "Jane Doe",
    "dateOfBirth": "1995-01-01"
  }
]
```
---
### Get customer by id

```http
GET /insurance/customer/{customerId}
```

**Response**
```json
{
  "customerId": "C001",
  "name": "John Doe",
  "dateOfBirth": "2000-04-12"
}
```
---
### Create customer

```http
POST /insurance/customer
```

**Request**
```json
{
  "customerId": "C001",
  "name": "John Doe",
  "dateOfBirth": "2000-04-12"
}
```

**Response**
```json
{
  "customerId": "C001",
  "name": "John Doe",
  "dateOfBirth": "2000-04-12"
}
```

---

## Quotes

### Create quote

```http
POST /insurance/quote
```

**Request**
```json
{
  "customerId": "C001",
  "insuranceType": "Car",
  "effectiveDate": "2026-04-15"
}
```

**Response**
```json
{
  "quoteId": "guid",
  "customer": {
    "customerId": "C001",
    "name": "John Doe",
    "dateOfBirth": "2000-04-12"
  },
  "insuranceType": "Car",
  "effectiveDate": "2026-04-15",
  "expirationDate": "2027-04-15",
  "price": 1500,
  "quoteStatus": "Quoted",
  "createdAt": "2026-04-12T12:00:00Z"
}
```
---
### Get all quotes

```http
GET /insurance/quote
```

**Response**
```json
[
  {
    "quoteId": "guid",
    "customer": {
      "customerId": "C001",
      "name": "John Doe",
      "dateOfBirth": "2000-04-12"
    },
    "insuranceType": "Car",
    "effectiveDate": "2026-04-15",
    "expirationDate": "2027-04-15",
    "price": 1500,
    "quoteStatus": "Quoted",
    "createdAt": "2026-04-12T12:00:00Z"
  }
]
```
---

### Get quote

```http
GET /insurance/quote/{quoteId}
```

**Response**
```json
{
  "quoteId": "guid",
  "customer": {
    "customerId": "C001",
    "name": "John Doe",
    "dateOfBirth": "2000-04-12"
  },
  "insuranceType": "Car",
  "effectiveDate": "2026-04-15",
  "expirationDate": "2027-04-15",
  "price": 1500,
  "quoteStatus": "Quoted",
  "createdAt": "2026-04-12T12:00:00Z"
}
```

---

## Update quote

```http
PUT /insurance/quote/{quoteId}
```

**Request**
```json
{
  "customerId": "C002",
  "insuranceType": "House",
  "effectiveDate": "2026-05-01",
  "expirationDate": "2027-05-01"
}
```

**Response**
```json
{
  "quoteId": "guid",
  "customer": {
    "customerId": "C002",
    "name": "Jane Doe",
    "dateOfBirth": "1995-01-01"
  },
  "insuranceType": "House",
  "effectiveDate": "2026-05-01",
  "expirationDate": "2027-05-01",
  "price": 500,
  "quoteStatus": "Quoted",
  "createdAt": "2026-04-12T12:00:00Z"
}
```

---

## Policies

### Get all policies

```http
GET /insurance/policy
```

**Response**
```json
[
  {
    "policyNumber": 1,
    "customer": {
      "customerId": "C001",
      "name": "John Doe",
      "dateOfBirth": "2000-04-12"
    },
    "insuranceType": "Car",
    "effectiveDate": "2026-04-15T00:00:00",
    "expirationDate": "2027-04-15T00:00:00",
    "price": 1500
  }
]
```
---
### Get policy by number

```http
GET /insurance/policy/{policyNumber}
```

**Response**
```json
{
  "policyNumber": 1,
  "customer": {
    "customerId": "C001",
    "name": "John Doe",
    "dateOfBirth": "2000-04-12"
  },
  "insuranceType": "Car",
  "effectiveDate": "2026-04-15T00:00:00",
  "expirationDate": "2027-04-15T00:00:00",
  "price": 1500
}
```
---
### Create policy

```http
POST /insurance/policy
```

**Request**
```json
{
  "quoteId": "Q123"
}
```

**Response**
```json
{
  "policyNumber": 1,
  "customer": {
    "customerId": "C001",
    "name": "John Doe",
    "dateOfBirth": "2000-04-12"
  },
  "insuranceType": "Car",
  "effectiveDate": "2026-04-15T00:00:00",
  "expirationDate": "2027-04-15T00:00:00",
  "price": 1500
}
```
