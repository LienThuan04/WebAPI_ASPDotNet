# WebAPI_ASPDotNet

- Chủ sở hữu / Owner: [@LienThuan04](https://github.com/LienThuan04)
- Repository: [LienThuan04/WebAPI_ASPDotNet](https://github.com/LienThuan04/WebAPI_ASPDotNet)

Ngôn ngữ: [Tiếng Việt](#tiếng-việt) | [English](#english)

## Languages / Ngôn ngữ
Theo GitHub Languages: C#
  
[![C#](https://img.shields.io/badge/C%23-language-239120?logo=csharp&logoColor=white)](https://github.com/LienThuan04/WebAPI_ASPDotNet)

## Tech Stack / Công nghệ
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core)
[![Swagger UI](https://img.shields.io/badge/Swagger-UI-85EA2D?logo=swagger&logoColor=black)](https://swagger.io/tools/swagger-ui/)
[![OpenAPI](https://img.shields.io/badge/OpenAPI-3.x-6BA539?logo=openapiinitiative&logoColor=white)](https://www.openapis.org/)
[![JWT](https://img.shields.io/badge/Auth-JWT-000000?logo=jsonwebtokens&logoColor=white)](https://jwt.io/)
[![MongoDB](https://img.shields.io/badge/Database-MongoDB-47A248?logo=mongodb&logoColor=white)](https://www.mongodb.com/)

## Packages / Gói thư viện
[![BCrypt.Net-Next](https://img.shields.io/nuget/v/BCrypt.Net-Next?label=BCrypt.Net-Next&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/BCrypt.Net-Next/)
[![dotenv.net](https://img.shields.io/nuget/v/dotenv.net?label=dotenv.net&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/dotenv.net/)
[![JwtBearer](https://img.shields.io/nuget/v/Microsoft.AspNetCore.Authentication.JwtBearer?label=JwtBearer&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/)
[![MongoDB.Driver](https://img.shields.io/nuget/v/MongoDB.Driver?label=MongoDB.Driver&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/MongoDB.Driver/)
[![Swashbuckle.AspNetCore](https://img.shields.io/nuget/v/Swashbuckle.AspNetCore?label=Swashbuckle.AspNetcore&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)
[![Microsoft.OpenApi](https://img.shields.io/nuget/v/Microsoft.OpenApi?label=Microsoft.OpenApi&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/Microsoft.OpenApi/)

---

## Tiếng Việt

### Tổng quan
Dự án Web API xây dựng bằng ASP.NET (C#) nhằm cung cấp các endpoint RESTful cho ứng dụng/dịch vụ phía client. Dự án con chính: `LearnASPDotNet`. Tích hợp xác thực (JWT), MongoDB và Swagger/OpenAPI.

### Hướng dẫn chạy nhanh
```bash
cd LearnASPDotNet
dotnet restore
dotnet build
dotnet run
```

Sau khi khởi động, ứng dụng sẽ lắng nghe theo cấu hình trong `Properties/launchSettings.json`.  
Nếu bật Swagger, truy cập `/swagger` trên địa chỉ localhost để xem và thử nghiệm API.

### Cấu trúc thư mục (cập nhật theo commit mới nhất)
```
WebAPI_ASPDotNet/
├─ .gitattributes
├─ .gitignore
├─ WebDotNetCore.sln
├─ README.md
└─ LearnASPDotNet/
   ├─ Program.cs
   ├─ LearnASPDotNet.csproj
   ├─ LearnASPDotNet.http
   ├─ .env.example
   ├─ appsettings.json
   ├─ appsettings.Development.json
   ├─ Properties/
   │  ├─ launchSettings.json
   │  ├─ serviceDependencies.json
   │  └─ serviceDependencies.local.json
   ├─ Extensions/
   │  ├─ JwtAuthentication/
   │  │  └─ JwtServiceExtensions.cs
   │  ├─ MongoDB/
   │  │  └─ MongoDbServiceExtensions.cs
   │  └─ Swaggers/
   │     └─ SwaggerServiceExtensions.cs
   ├─ Middlewares/
   │  └─ MiddlewareException.cs
   ├─ Settings/
   │  └─ MongoDbSettings.cs
   └─ Features/
      ├─ Auths/
      │  ├─ AuthController.cs
      │  ├─ AuthDependency.cs
      │  ├─ AuthRepository.cs
      │  ├─ AuthService.cs
      │  ├─ IAuthRepository.cs
      │  ├─ IAuthService.cs
      │  ├─ JwtService.cs
      │  └─ Dtos/
      ├─ Users/
      │  ├─ UserController.cs
      │  ├─ UserDependency.cs
      │  ├─ UserRepository.cs
      │  ├─ UserService.cs
      │  ├─ IUserRepository.cs
      │  ├─ IUserService.cs
      │  ├─ Models/
      │  └─ Dtos/
      └─ Sessions/
         ├─ Models/
         └─ Dtos/
```

### Giải thích thư mục chính
- Root: cấu hình Git, solution và README.
- LearnASPDotNet/: dự án Web API.
  - Program.cs: khởi động ứng dụng; đăng ký Swagger, MongoDB, JWT, middleware, controllers.
  - LearnASPDotNet.csproj: target `net8.0` và các package NuGet.
  - LearnASPDotNet.http: mẫu request HTTP để thử API.
  - .env.example: ví dụ biến môi trường cho `dotenv.net`.
  - appsettings*.json: cấu hình chung và cho môi trường Development.
  - Properties/: thiết lập khởi chạy và phụ thuộc dịch vụ (launchSettings, serviceDependencies).
  - Extensions/: DI helpers cho JWT, MongoDB, Swagger.
  - Settings/: cấu hình strongly-typed (MongoDB).
  - Features/: tập trung các module
    - Auths/: xác thực và phát hành token (controller, service, repository, interfaces, DTOs).
    - Users/: quản lý người dùng (controller, service, repository, interfaces, models, DTOs).
    - Sessions/: quản lý phiên (models, DTOs).

### Tài liệu API
- Swagger UI (nếu bật): truy cập `/swagger`
- Postman Collection/Docs (nếu có): thêm liên kết tại đây

### Đóng góp
- Fork repo, tạo nhánh tính năng, mở pull request mô tả thay đổi.
- Tuân thủ quy ước code và tiêu chuẩn đặt tên của dự án.

### Giấy phép
Chưa thiết lập.

---

## English

### Overview
ASP.NET (C#) Web API with sub-project `LearnASPDotNet`. Integrates JWT authentication, MongoDB, and Swagger/OpenAPI.

### Quick Start
```bash
cd LearnASPDotNet
dotnet restore
dotnet build
dotnet run
```

### Folder Tree (updated to latest commit)
```
WebAPI_ASPDotNet/
├─ .gitattributes
├─ .gitignore
├─ WebDotNetCore.sln
├─ README.md
└─ LearnASPDotNet/
   ├─ Program.cs
   ├─ LearnASPDotNet.csproj
   ├─ LearnASPDotNet.http
   ├─ .env.example
   ├─ appsettings.json
   ├─ appsettings.Development.json
   ├─ Properties/
   │  ├─ launchSettings.json
   │  ├─ serviceDependencies.json
   │  └─ serviceDependencies.local.json
   ├─ Extensions/
   │  ├─ JwtAuthentication/
   │  │  └─ JwtServiceExtensions.cs
   │  ├─ MongoDB/
   │  │  └─ MongoDbServiceExtensions.cs
   │  └─ Swaggers/
   │     └─ SwaggerServiceExtensions.cs
   ├─ Middlewares/
   │  └─ MiddlewareException.cs
   ├─ Settings/
   │  └─ MongoDbSettings.cs
   └─ Features/
      ├─ Auths/
      │  ├─ AuthController.cs
      │  ├─ AuthDependency.cs
      │  ├─ AuthRepository.cs
      │  ├─ AuthService.cs
      │  ├─ IAuthRepository.cs
      │  ├─ IAuthService.cs
      │  ├─ JwtService.cs
      │  └─ Dtos/
      ├─ Users/
      │  ├─ UserController.cs
      │  ├─ UserDependency.cs
      │  ├─ UserRepository.cs
      │  ├─ UserService.cs
      │  ├─ IUserRepository.cs
      │  ├─ IUserService.cs
      │  ├─ Models/
      │  └─ Dtos/
      └─ Sessions/
         ├─ Models/
         └─ Dtos/
```

### Folder Explanations
- Root: Git configs, solution and README.
- LearnASPDotNet/: web API project bootstrap, packages, HTTP samples, env example, app settings, Properties.
- Extensions/: DI helpers (JWT, MongoDB, Swagger).
- Settings/: strongly-typed settings (MongoDB).
- Features/: feature-oriented modules
  - Auths/: authentication (controller, service, repository, interfaces, DTOs).
  - Users/: user management (controller, service, repository, interfaces, models, DTOs).
  - Sessions/: session entities and DTOs.

### API Documentation
- Swagger UI (if enabled): visit `/swagger`

### Contributing
- Fork, feature branch, pull request.

### License
Not set yet.