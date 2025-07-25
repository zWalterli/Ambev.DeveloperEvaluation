[Back to README](../README.md)

## How to Run the Project

To get the project running locally, follow the steps below:

### Prerequisites

Ensure the following tools are installed on your machine:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [PostgreSQL](https://www.postgresql.org/download/) running on port `5432`
- [RabbitMQ](https://www.rabbitmq.com/) running on port `9820`

### Running the Application

#### Option 1: Using Docker Compose

You can spin up the full environment using Docker Compose:

1. Navigate to the project root:

```bash
cd abi-gth-omnia-developer-evaluation/backend/
```

1. Run Docker:

```bash
docker-compose up -d
```

> This will start the API and the necessary services.

### Accessing Swagger

Once the application is running, Swagger can be accessed at:

```
http://localhost:9820/swagger
```

> Make sure the container exposes port 9820.

You will see the full API documentation where you can test the endpoints.

---

<div style="display: flex; justify-content: space-between;">
  <a href="../README.md">Previous: Read Me</a>
  <a href="./project-structure.md">Next: Project Structure</a>
</div>
