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
[![Swashbuckle.AspNetCore](https://img.shields.io/nuget/v/Swashbuckle.AspNetCore?label=Swashbuckle.AspNetCore&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)
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

### Cấu trúc thư mục
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
   ├─ Auths/
   ├─ Extensions/
   ├─ Middlewares/
   ├─ Sessions/
   ├─ Settings/
   └─ Users/
```

### Giải thích thư mục chính
- Root
  - .gitattributes: Thiết lập thuộc tính Git cho repo.
  - .gitignore: Khai báo các tệp/thư mục bỏ qua khi commit.
  - WebDotNetCore.sln: Solution file cho Visual Studio.
  - README.md: Tài liệu dự án.

- LearnASPDotNet/ (dự án Web API)
  - Program.cs: Khởi động ứng dụng; đăng ký Swagger, MongoDB, JWT, middleware, controllers.
  - LearnASPDotNet.csproj: Khai báo `net8.0` và các package NuGet.
  - LearnASPDotNet.http: Mẫu request HTTP để thử API.
  - .env.example: Ví dụ biến môi trường cho `dotenv.net`.
  - appsettings*.json: Cấu hình ứng dụng (chung và Development).
  - Properties/: Thiết lập khởi chạy (launch settings) cho môi trường dev.

  - Auths/: Xác thực và phát hành token
    - AuthController.cs: Endpoint đăng ký/đăng nhập, phát hành JWT.
    - JwtService.cs: Tạo/kiểm tra JWT.
    - Dtos/: DTO cho request/response liên quan auth.

  - Users/: Quản lý người dùng
    - UserController.cs: Endpoint người dùng.
    - UserService.cs: Nghiệp vụ người dùng.
    - Models/, Dtos/: Khai báo model và DTO người dùng.

  - Sessions/: Quản lý phiên
    - SessionService.cs: Nghiệp vụ phiên (tạo/kiểm tra/hủy).
    - Models/, Dtos/: Thực thể và DTO phiên.

  - Middlewares/: Middleware tuỳ chỉnh
    - MiddlewareException.cs: Bắt và chuẩn hóa lỗi (đặc biệt JWT).

  - Extensions/: Tiện ích đăng ký dịch vụ (Dependency Injection)
    - JwtAuthentication/: Cấu hình JWT (`AddJwtAuthentication()`).
    - MongoDB/: Cấu hình MongoDB (`AddMongoDb()`).
    - Swaggers/: Cấu hình Swagger/OpenAPI (`AddSwagger()`).

  - Settings/: Cấu hình strongly-typed
    - MongoDbSettings.cs: Thiết lập kết nối MongoDB.

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
ASP.NET (C#) Web API with sub-project `LearnASPDotNet`. Integrates JWT authentication, MongoDB, and Swagger/OpenAPI for API documentation.

### Quick Start
```bash
cd LearnASPDotNet
dotnet restore
dotnet build
dotnet run
```

### Folder Tree
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
   ├─ Auths/
   ├─ Extensions/
   ├─ Middlewares/
   ├─ Sessions/
   ├─ Settings/
   └─ Users/
```

### Folder Explanations
- Root: Git configs and solution.
- LearnASPDotNet/: app bootstrap, project file, HTTP samples, env example, app settings, Properties.
- Auths/: authentication (controller, JWT service, DTOs).
- Users/: user management (controller, service, models/DTOs).
- Sessions/: session lifecycle (service, models/DTOs).
- Middlewares/: custom exception handling.
- Extensions/: DI helpers for JWT, MongoDB, Swagger.
- Settings/: strongly-typed settings (MongoDB).

### API Documentation
- Swagger UI (if enabled): visit `/swagger`

### Contributing
- Fork, feature branch, pull request.

### License
Not set yet.