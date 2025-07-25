[Back to README](../README.md)

### Sales

#### POST /sales

- Description: Create a new sale
- Request Body:

```json
{
  "saleNumber": "string",
  "customer": "string",
  "branch": "string",
  "items": [
    {
      "productName": "string",
      "quantity": 0,
      "unitPrice": 0
    }
  ]
}
```

- Response:

```json
{
  "id": "guid"
}
```

#### PUT /sales

- Description: Update a sale
- Request Body:

```json
{
  "id": "guid",
  "saleNumber": "string",
  "customer": "string",
  "branch": "string",
  "items": [
    {
      "id": "guid",
      "productName": "string",
      "quantity": 0,
      "unitPrice": 0
    }
  ]
}
```

- Response:

```json
{
  "id": "guid"
}
```

#### GET /sales

- Description: Retrieve a list of sales with pagination and filtering
- Query Parameters:
  - `id` (optional): Sale ID
  - `saleNumber` (optional): Sale number
  - `customer` (optional): Customer name
  - `branch` (optional): Branch
  - `page` (optional): Page number (default: 1)
  - `pageSize` (optional): Items per page (default: 10)
  - `orderBy` (optional): Sorting order (e.g., "customer desc, branch asc")
- Response:

```json
{
  "data": [
    {
      "id": "guid",
      "saleNumber": "string",
      "saleDate": "datetime",
      "customer": "string",
      "branch": "string",
      "isCancelled": false,
      "itens": [
        {
          "id": "guid",
          "productName": "string",
          "quantity": 0,
          "unitPrice": 0,
          "discountPercentage": 0,
          "total": 0,
          "isCancelled": false
        }
      ]
    }
  ],
  "totalItems": "integer",
  "currentPage": "integer",
  "totalPages": "integer"
}
```

#### POST /sales/{saleId}/cancel

- Description: Cancel a specific sale
- Path Parameters:
  - `saleId`: Sale ID
- Response:

```json
{}
```

#### POST /sales/{saleId}/items/{itemId}/cancel

- Description: Cancel a specific item in a sale
- Path Parameters:
  - `saleId`: Sale ID
  - `itemId`: Item ID
- Response:

```json
{}
```

<br/>
<div style="display: flex; justify-content: space-between;">
  <a href="./auth-api.md">Previous: Auth API</a>
  <a href="./users-api.md">Next: Users API</a>
</div>
