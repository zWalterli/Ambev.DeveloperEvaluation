[Back to README](../README.md)

### Users API

#### POST /api/users

- **Description**: Creates a new user
- **Request Body**:
  ```json
  {
    "username": "string",
    "password": "string",
    "email": "string",
    "phone": "string",
    "status": "string (enum: Active, Inactive, Suspended)",
    "role": "string (enum: Customer, Manager, Admin)"
  }
  ```
- **Response** `201 Created`:
  ```json
  {
    "success": true,
    "message": "User created successfully",
    "data": {
      "id": "guid",
      "name": "string",
      "email": "string",
      "phone": "string",
      "role": "string (Customer, Manager, Admin)",
      "status": "string (Active, Inactive, Suspended)"
    }
  }
  ```
- **Error Response** `400 Bad Request`:
  ```json
  {
    "success": false,
    "message": "Validation errors",
    "errors": [{ "field": "email", "message": "Invalid email format" }]
  }
  ```

---

#### GET /api/users/{id}

- **Description**: Retrieves a user by their ID
- **Path Parameters**:
  - `id`: (Guid) Unique user identifier
- **Response** `200 OK`:
  ```json
  {
    "success": true,
    "message": "User retrieved successfully",
    "data": {
      "id": "guid",
      "name": "string",
      "email": "string",
      "phone": "string",
      "role": "string (Customer, Manager, Admin)",
      "status": "string (Active, Inactive, Suspended)"
    }
  }
  ```
- **Error Response** `400 Bad Request`:

  ```json
  {
    "success": false,
    "message": "Invalid ID"
  }
  ```

- **Error Response** `404 Not Found`:
  ```json
  {
    "success": false,
    "message": "User not found"
  }
  ```

---

#### DELETE /api/users/{id}

- **Description**: Deletes a user by their ID
- **Path Parameters**:
  - `id`: (Guid) Unique user identifier
- **Response** `200 OK`:
  ```json
  {
    "success": true,
    "message": "User deleted successfully"
  }
  ```
- **Error Response** `400 Bad Request`:

  ```json
  {
    "success": false,
    "message": "Invalid ID"
  }
  ```

- **Error Response** `404 Not Found`:
  ```json
  {
    "success": false,
    "message": "User not found"
  }
  ```

---

<div style="display: flex; justify-content: space-between;">
  <a href="./carts-api.md">Previous: Carts API</a>
  <a href="./auth-api.md">Next: Auth API</a>
</div>
