# Unity + AWS + ASP.NET Template

A clean, scalable template to connect Unity projects with an ASP.NET Web API backend deployed on AWS.

This repository is **game-agnostic** and focuses on:
- Clean architecture
- Cloud-ready backend
- Reusable Unity API client
- Clear documentation

---

## Features

- ASP.NET Web API (.NET 8)
- Clean Architecture (Domain / Application / Infrastructure)
- AWS-ready configuration (EC2, RDS, S3)
- Unity reusable REST API client
- No gameplay code
- Template

---

## Architecture Overview

Unity Client
↓ HTTP / JSON
ASP.NET Web API (EC2)
↓
PostgreSQL (RDS)
↓
S3 (Cloud Storage)

Unity acts strictly as a **client**.  
The backend owns infrastructure and cloud concerns.

---

## Repository Structure

unity-aws-aspnet-template/
├── backend/
│ └── src/
│ ├── Backend.Api
│ ├── Backend.Application
│ ├── Backend.Domain
│ └── Backend.Infrastructure
│
├── unity-client/
│ └── Assets/
│ └── Scripts/
│ └── Networking/
│
├── docs/
│ ├── aws-env-vars.md
│ └── decisions.md
│
└── README.md

---

## Getting Started

### Backend (Local)

bash
cd backend
dotnet run --project src/Backend.Api
Test: http://localhost:5000/ping

## Unity

Copy unity-client/Assets/Scripts/Networking into your Unity project

Create an ApiConfig ScriptableObject

Set BaseUrl (e.g. http://localhost:5000)

Use ApiClient to communicate with the backend

## AWS Deployment

This template is prepared for AWS but does not deploy automatically.

Supported services:
EC2 (API hosting)
RDS (PostgreSQL)
S3 (Cloud files)
IAM Roles (no hardcoded credentials)

See: docs/aws-env-vars.md