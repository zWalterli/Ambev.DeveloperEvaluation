[Back to README](../README.md)

## Project Structure

The project should be structured as follows:

```
ABI-GTH-OMNIA-DEVELOPER-EVALUATION
├── .doc/
├── .vscode/
├── backend/
│   ├── src/
│   │   ├── Ambev.DeveloperEvaluation.Application/
│   │   ├── Ambev.DeveloperEvaluation.Common/
│   │   ├── Ambev.DeveloperEvaluation.Domain/
│   │   ├── Ambev.DeveloperEvaluation.IoC/
│   │   ├── Ambev.DeveloperEvaluation.ORM/
│   │   └── Ambev.DeveloperEvaluation.WebApi/
│   ├── src.sln
│   └── tests/
├── consumer/
│   └── DeveloperEvaluation.EventConsumer/
│       ├── .vscode/
│       ├── Models/
│       ├── Utils/
│       ├── appsettings.Development.json
│       ├── appsettings.json
│       ├── DeveloperEvaluation.EventConsumer.csproj
│       ├── DeveloperEvaluation.EventConsumer.sln
│       ├── Dockerfile
│       └── Program.cs
├── .dockerignore
├── .dockerignore copy
├── .editorconfig
├── Ambev.DeveloperEvaluation.sln
├── Ambev.DeveloperEvaluation.sln.DotSettings.user
├── coverage-report.bat
├── coverage-report.sh
├── docker-compose.dcproj
├── docker-compose.override.yml
├── docker-compose.yml
└── launchSettings.json
```
