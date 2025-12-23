# Unity + AWS + ASP.NET Core Template

Plantilla mínima para iniciar proyectos de Unity que se integran con AWS y un backend en ASP.NET Core Web API. Incluye configuración base, endpoints de salud/configuración y un pequeño “ping” para verificar que todo responde.

## Estructura

- `backend/Backend.sln`: solución con el Web API.
- `backend/src/Backend.Api`: proyecto principal de la API.
- (Agrega tu proyecto de Unity en la raíz o en `unity/` según prefieras).

## Qué trae listo

- ASP.NET Core 8 con Swagger en desarrollo.
- Health check (`/health`).
- Endpoint de ping (`/ping`).
- Endpoint para exponer configuración hacia el cliente Unity (`/api/config`).
- Endpoint para validar conexión a AWS S3 y credenciales (`/api/config/aws/status`).
- CORS preconfigurado para editores WebGL/localhost.

## Prerrequisitos

- .NET 8 SDK.
- Cuenta y credenciales de AWS (perfil o variables de entorno) si quieres probar las integraciones.
- Unity (editor 2022 LTS o superior recomendado) para consumir la API.

## Configuración rápida

Los parámetros principales viven en `backend/src/Backend.Api/appsettings*.json`:

```jsonc
"AWS": {
  "Region": "us-east-1",
  "Profile": "",
  "AssetBucket": ""
},
"UnityClient": {
  "AllowedOrigins": "http://localhost:3000;http://127.0.0.1:3000",
  "Environment": "dev",
  "ApiBaseUrl": "http://localhost:5000"
}
```

- `AWS.Region`: región por defecto para los SDK.
- `AWS.Profile`: nombre del perfil de credenciales (deja vacío para usar variables de entorno o roles).
- `AWS.AssetBucket`: bucket de ejemplo para assets/descargas.
- `UnityClient.AllowedOrigins`: orígenes permitidos (separados por `;`) para CORS.
- `UnityClient.Environment`: etiqueta del entorno que verá el cliente.
- `UnityClient.ApiBaseUrl`: URL pública que Unity usará para llamar a la API.

## Ejecutar el backend

```bash
cd backend
dotnet restore
dotnet run --project src/Backend.Api/Backend.Api.csproj
```

Visita `http://localhost:5000/swagger` en desarrollo.

## Endpoints útiles

- `GET /health` — health check.
- `GET /ping` — respuesta rápida con timestamp UTC.
- `GET /api/config` — configuración que Unity puede leer (environment, base URL, bucket).
- `GET /api/config/aws/status` — prueba de credenciales/permiso contra S3 (lista buckets si es posible).

## Cómo consumir desde Unity

1. Crea un script de configuración que haga `UnityWebRequest.Get($"{baseUrl}/api/config")`.
2. Serializa la respuesta al DTO `UnityClientConfig` equivalente en C#:

   ```csharp
   public class UnityClientConfig
   {
       public string environment;
       public string apiBaseUrl;
       public string assetBucket;
   }
   ```

3. Usa `apiBaseUrl` para construir el resto de tus requests y `assetBucket` para S3 (SDK, CloudFront o presigned URLs).
4. Si compilas WebGL, agrega el origen del build a `UnityClient.AllowedOrigins` para evitar problemas de CORS.

## Próximos pasos sugeridos

- Añadir autenticación (Cognito/JWT) y autorización granular.
- Incluir pipelines de CI/CD (GitHub Actions) para build/test.
- Crear un proyecto de muestra en Unity que consuma estos endpoints (e.g., panel de status y descarga de assets).

¡Listo para extender y lanzar! Si publicas el repo, recuerda ocultar claves y usar perfiles/roles seguros en AWS.
