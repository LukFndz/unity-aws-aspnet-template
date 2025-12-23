# Unity + AWS + ASP.NET Template

A production-ready template to connect Unity projects with an ASP.NET Web API backend deployed on AWS.

This repository is **game-agnostic** and focuses on clean architecture, cloud readiness, and reusability rather than gameplay-specific logic.

---

## ✨ What This Template Provides

- ASP.NET Web API (.NET 8)
- Clean Architecture (Api / Application / Domain / Infrastructure)
- AWS-ready backend deployment (EC2 + Nginx + systemd)
- Reusable Unity HTTP API client
- Environment-based configuration
- No gameplay assumptions

---

## 🧱 High-Level Architecture

Unity Client
↓ HTTP / HTTPS
Nginx (Reverse Proxy)
↓
ASP.NET Web API (EC2)

- Unity communicates strictly via HTTP
- Backend owns infrastructure concerns
- AWS credentials are never exposed to Unity

---

## 🚀 Getting Started

### Backend (Local)

1. cd backend
2. dotnet run --project src/Backend.Api
3. Test at: http://localhost:5000/ping

---

## Unity Client

1. Copy unity-client/Assets/Scripts/Networking into your Unity project
2. Create an ApiConfig ScriptableObject
3. Set the backend base URL
4. Use ApiClient to communicate with the backend

---

## AWS Deployment

This template has been deployed and tested using:
EC2 (Amazon Linux 2023)
Nginx reverse proxy
systemd service
ASP.NET 8 runtime

Deployment is manual by design to keep the template infrastructure-agnostic.

---

## Security Notes

HTTP is allowed only for local development
HTTPS is required for WebGL and mobile builds
Secrets are provided exclusively via environment variables
No AWS credentials are stored in the repository

--- 

## Out of Scope (By Design)

This template intentionally does NOT include:
Authentication / Authorization
Database schema
Gameplay logic
WebSockets / real-time features
CI/CD pipelines

These concerns vary per project and should be implemented as needed.

---

## 🧭 Branching Strategy

This repository was built incrementally.
Step branches (`step/*`) reflect the development process and can be used as reference points.

The `main` branch always represents the stable template.
