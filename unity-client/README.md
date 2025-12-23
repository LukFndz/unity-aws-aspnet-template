# Unity Client Template

This folder contains a reusable networking layer for Unity projects.

## Key Principles

- No backend assumptions
- No gameplay logic
- No AWS dependencies
- Explicit configuration

## How to Use

1. Copy `Assets/Scripts/Networking` into your Unity project
2. Create an `ApiConfig` ScriptableObject
3. Set the backend base URL
4. Use `ApiClient` to perform HTTP requests

## Notes

- HTTP is allowed only in development
- HTTPS is required for WebGL and mobile builds