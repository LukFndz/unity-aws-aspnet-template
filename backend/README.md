# Backend – ASP.NET Web API

This backend is a clean, cloud-ready ASP.NET Web API built as a reusable template for Unity projects.

It is intentionally **game-agnostic** and focuses on architecture and infrastructure rather than domain logic.

## 🧱 Architecture

The backend follows Clean Architecture principles:

Backend.Api
├── Controllers (HTTP endpoints)
├── Program.cs (composition root)
│
Backend.Application
├── Use cases
├── DTOs
│
Backend.Domain
├── Pure domain models
│
Backend.Infrastructure
├── Persistence
├── AWS integrations

---

## 🚀 Running Locally
dotnet run --project src/Backend.Api

## Status Check Endpoint
GET /ping

## AWS Deployment Model
Hosted on EC2
Nginx used as reverse proxy
systemd manages process lifecycle
Configuration via environment variables

The backend listens internally on port 5000 and is exposed publicly through Nginx.

## Redeploy Workflow

When backend code changes:
1. Publish locally:
dotnet publish src/Backend.Api -c Release -o publish

2. Copy to EC2:
scp -i your-key.pem -r publish/* ec2-user@YOUR_IP:/var/www/backend-api

3. Restart service:
sudo systemctl restart backend-api