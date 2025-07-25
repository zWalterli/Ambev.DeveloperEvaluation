[Back to README](../README.md)

### Authentication

#### POST /auth/login

- Description: Authenticate a user
- Request Body:
  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```
- Response:

  ```json
  {
    "data": {
      "token": "string",
      "email": "string",
      "name": "string",
      "role": "string"
    },
    "success": true,
    "message": "string",
    "errors": []
  }
  ```

<br/>
<div style="display: flex; justify-content: space-between;">
  <a href="./users-api.md">Previous: Users API</a>
  <a href="./project-structure.md">Next: Project Structure</a>
</div>
