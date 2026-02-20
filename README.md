# WebAPI_ASPDotNet

- Chủ sở hữu / Owner: [@LienThuan04](https://github.com/LienThuan04)
- Repository: [LienThuan04/WebAPI_ASPDotNet](https://github.com/LienThuan04/WebAPI_ASPDotNet)

Ngôn ngữ:  [Tiếng Việt](#tiếng-việt) | [English](#english)

## Languages / Ngôn ngữ
Theo GitHub Languages:  C#
  
[![C#](https://img.shields.io/badge/C%23-language-239120?logo=csharp&logoColor=white)](https://github.com/LienThuan04/WebAPI_ASPDotNet)

## Tech Stack / Công nghệ
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core)
[![Swagger UI](https://img.shields.io/badge/Swagger-UI-85EA2D?logo=swagger&logoColor=black)](https://swagger.io/tools/swagger-ui/)
[![OpenAPI](https://img.shields.io/badge/OpenAPI-3.x-6BA539?logo=openapiinitiative&logoColor=white)](https://www.openapis.org/)
[![JWT](https://img.shields.io/badge/Auth-JWT-000000?logo=jsonwebtokens&logoColor=white)](https://jwt.io/)
[![MongoDB](https://img.shields.io/badge/Database-MongoDB-47A248?logo=mongodb&logoColor=white)](https://www.mongodb.com/)
[![Supabase](https://img.shields.io/badge/Storage-Supabase-3ECF8E?logo=supabase&logoColor=white)](https://supabase.com/)

## Packages / Gói thư viện
[![BCrypt.Net-Next](https://img.shields.io/nuget/v/BCrypt.Net-Next?label=BCrypt.Net-Next&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/BCrypt.Net-Next/)
[![dotenv.net](https://img.shields.io/nuget/v/dotenv.net?label=dotenv.net&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/dotenv.net/)
[![JwtBearer](https://img.shields.io/nuget/v/Microsoft.AspNetCore.Authentication.JwtBearer?label=JwtBearer&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/)
[![MongoDB.Driver](https://img.shields.io/nuget/v/MongoDB.Driver?label=MongoDB.Driver&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/MongoDB.Driver/)
[![Swashbuckle.AspNetCore](https://img.shields.io/nuget/v/Swashbuckle.AspNetCore?label=Swashbuckle.AspNetcore&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)
[![Microsoft.OpenApi](https://img.shields.io/nuget/v/Microsoft.OpenApi?label=Microsoft.OpenApi&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/Microsoft.OpenApi/)
[![supabase-csharp](https://img.shields.io/nuget/v/supabase-csharp?label=supabase-csharp&logo=nuget&color=0B5FFF)](https://www.nuget.org/packages/supabase-csharp/)

---

## Tiếng Việt

### Tổng quan
Dự án Web API xây dựng bằng ASP.NET (C#) nhằm cung cấp các endpoint RESTful cho ứng dụng/dịch vụ phía client.  Dự án con chính:  `LearnASPDotNet`. Tích hợp xác thực (JWT), MongoDB, Supabase Storage và Swagger/OpenAPI.

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
├── 📄 README.md                          # Tài liệu dự án
├── 📄 WebDotNetCore.sln                  # Solution file
├── 📄 .gitignore                         
├── 📄 .gitattributes
│
└── 📁 LearnASPDotNet/                    # Main project
    │
    ├── 📄 Program.cs                     # Entry point & DI configuration
    ├── 📄 LearnASPDotNet.csproj          # Project file
    ├── 📄 LearnASPDotNet.http            # HTTP request samples
    ├── 📄 .env.example                   # Environment variables template
    ├── 📄 appsettings.json               # App configuration
    ├── 📄 appsettings.Development.json   # Development config
    │
    ├── 📁 Properties/
    │   ├── launchSettings.json           # Launch profiles
    │   ├── serviceDependencies.json
    │   └── serviceDependencies.local.json
    │
    ├── 📁 Extensions/                    # Service extensions
    │   ├── 📁 JwtAuthentication/
    │   │   └── JwtServiceExtensions.cs   # JWT config
    │   ├── 📁 MongoDB/
    │   │   └── MongoDbServiceExtensions.cs # MongoDB config
    │   ├── 📁 Supabase/
    │   │   └── SupabaseServiceExtensions.cs # Supabase config
    │   └── 📁 Swaggers/
    │       ├── SwaggerServiceExtensions.cs     # Swagger service registration
    │       └── SwaggerApplicationExtensions.cs # Swagger middleware config
    │
    ├── 📁 Middlewares/
    │   └── MiddlewareException.cs        # Global exception handler
    │
    ├── 📁 Filters/                       # Custom filters
    │   └── ValidationFilter.cs           # Request validation filter
    │
    ├── 📁 Settings/
    │   ├── ConfigCors.cs                 # CORS configuration
    │   └── MongoDbSettings.cs            # MongoDB settings model
    │
    └── 📁 Features/                      # Feature-based organization
        │
        ├── 📁 Auths/                     # Authentication feature
        │   ├── AuthController.cs         # Auth endpoints
        │   ├── AuthDependency.cs         # DI registration
        │   ├── 📁 Services/
        │   │   ├── AuthService.cs        # Business logic
        │   │   ├── IAuthService.cs       # Service interface
        │   │   └── JwtService.cs         # JWT token handling
        │   ├── 📁 Repositories/
        │   │   ├── AuthRepository.cs     # Data access
        │   │   └── IAuthRepository.cs    # Repository interface
        │   └── 📁 Dtos/
        │       ├── LoginRequestDto.cs
        │       ├── RegisterRequestDto.cs
        │       ├── AuthResponseDto.cs
        │       └── JwtPayloadDto.cs
        │
        ├── 📁 Users/                     # User management feature
        │   ├── UserController.cs         # User endpoints
        │   ├── UserDependency.cs         # DI registration
        │   ├── 📁 Services/
        │   │   ├── UserService.cs        # Business logic
        │   │   └── IUserService.cs       # Service interface
        │   ├── 📁 Repositories/
        │   │   ├── UserRepository.cs     # Data access
        │   │   └── IUserRepository.cs    # Repository interface
        │   ├── 📁 Models/
        │   │   └── User.cs               # User entity
        │   └── 📁 Dtos/
        │       ├── UserDto.cs
        │       ├── UserResponseDto.cs
        │       ├── CreateUserDto.cs
        │       └── UpdateUserDto.cs
        │
        ├── 📁 Sessions/                  # Session management feature
        │   ├── SessionDependency.cs      # DI registration
        │   ├── 📁 Services/
        │   │   ├── SessionService.cs     # Business logic
        │   │   └── ISessionService.cs    # Service interface
        │   ├── 📁 Repositories/
        │   │   ├── SessionRepository.cs  # Data access
        │   │   └── ISessionRepository.cs # Repository interface
        │   ├── 📁 Models/
        │   │   └── Session.cs            # Session entity (with TTL)
        │   └── 📁 Dtos/
        │       ├── CreateSessionDto.cs
        │       └── SessionRequestDto.cs
        │
        └── 📁 Files/                     # File upload feature (Supabase)
            ├── FileController.cs         # File endpoints
            ├── FileDependency.cs         # DI registration
            ├── 📁 Services/
            │   ├── FileService.cs        # Business logic
            │   └── IFileService.cs       # Service interface
            ├── 📁 Repositories/
            │   ├── FileRepository.cs     # Data access (MongoDB metadata)
            │   └── IFileRepository.cs    # Repository interface
            ├── 📁 Models/
            │   └── FileMetadata.cs       # File metadata entity
            └── 📁 Dtos/
                ├── FileUploadRequestDto.cs
                ├── FileUploadResponseDto.cs
                └── FileTypes.cs          # File type constants
```

### Giải thích thư mục chính
- **Root**: cấu hình Git, solution và README.
- **LearnASPDotNet/**: dự án Web API. 
  - `Program.cs`: khởi động ứng dụng; đăng ký Swagger, MongoDB, JWT, Supabase, middleware, controllers.
  - `LearnASPDotNet.csproj`: target `net8.0` và các package NuGet.
  - `LearnASPDotNet.http`: mẫu request HTTP để thử API.
  - `.env.example`: ví dụ biến môi trường cho `dotenv.net`.
  - `appsettings*.json`: cấu hình chung và cho môi trường Development.
  - **Properties/**: thiết lập khởi chạy và phụ thuộc dịch vụ (launchSettings, serviceDependencies).
  - **Extensions/**: DI helpers cho JWT, MongoDB, Supabase, Swagger.
  - **Middlewares/**: Global exception handler.
  - **Filters/**: Custom filters cho validation và xử lý request.
  - **Settings/**: cấu hình strongly-typed (MongoDB).
  - **Features/**: tập trung các module
    - **Auths/**: xác thực và phát hành token (controller, service, repository, interfaces, DTOs).
    - **Users/**: quản lý người dùng (controller, service, repository, interfaces, models, DTOs).
    - **Sessions/**: quản lý phiên (service, repository, interfaces, models, DTOs).
    - **Files/**: quản lý upload file lên Supabase Storage (controller, service, repository, interfaces, models, DTOs).

### Tính năng chính
- ✅ **JWT Authentication** - Access Token & Refresh Token với HttpOnly cookies
- ✅ **User Management** - CRUD operations cho users
- ✅ **Session Management** - Quản lý refresh token với TTL Index trong MongoDB
- ✅ **File Upload** - Upload files (avatar, documents, images) lên Supabase Storage
  - Avatar tự động ghi đè khi upload mới
  - Metadata lưu trong MongoDB
  - Public URL cho truy cập file
- ✅ **Request Validation** - Custom filters để validate request data

### Tài liệu API
- Swagger UI (nếu bật): truy cập `/swagger`

### Đóng góp
- Fork repo, tạo nhánh tính năng, mở pull request mô tả thay đổi. 
- Tuân thủ quy ước code và tiêu chuẩn đặt tên của dự án. 

### Giấy phép
Chưa thiết lập. 

---

## English

### Overview
ASP.NET (C#) Web API with sub-project `LearnASPDotNet`. Integrates JWT authentication, MongoDB, Supabase Storage, and Swagger/OpenAPI. 

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
├── 📄 README.md                          # Project documentation
├── 📄 WebDotNetCore.sln                  # Solution file
├── 📄 .gitignore                         
├── 📄 .gitattributes
│
└── 📁 LearnASPDotNet/                    # Main project
    │
    ├── 📄 Program.cs                     # Entry point & DI configuration
    ├── 📄 LearnASPDotNet.csproj          # Project file
    ├── 📄 LearnASPDotNet.http            # HTTP request samples
    ├── 📄 .env.example                   # Environment variables template
    ├── 📄 appsettings.json               # App configuration
    ├── 📄 appsettings.Development.json   # Development config
    │
    ├── 📁 Properties/
    │   ├── launchSettings.json           # Launch profiles
    │   ├── serviceDependencies.json
    │   └── serviceDependencies.local.json
    │
    ├── 📁 Extensions/                    # Service extensions
    │   ├── 📁 JwtAuthentication/
    │   │   └── JwtServiceExtensions.cs   # JWT config
    │   ├── 📁 MongoDB/
    │   │   └── MongoDbServiceExtensions.cs # MongoDB config
    │   ├── 📁 Supabase/
    │   │   └── SupabaseServiceExtensions.cs # Supabase config
    │   └── 📁 Swaggers/
    │       ├── SwaggerServiceExtensions.cs     # Swagger service registration
    │       └── SwaggerApplicationExtensions.cs # Swagger middleware config
    │
    ├── 📁 Middlewares/
    │   └── MiddlewareException.cs        # Global exception handler
    │
    ├── 📁 Filters/                       # Custom filters
    │   └── ValidationFilter.cs           # Request validation filter
    │
    ├── 📁 Settings/
    │   ├── ConfigCors.cs                 # CORS configuration
    │   └── MongoDbSettings.cs            # MongoDB settings model
    │
    └── 📁 Features/                      # Feature-based organization
        │
        ├── 📁 Auths/                     # Authentication feature
        │   ├── AuthController.cs         # Auth endpoints
        │   ├── AuthDependency.cs         # DI registration
        │   ├── 📁 Services/
        │   │   ├── AuthService.cs        # Business logic
        │   │   ├── IAuthService.cs       # Service interface
        │   │   └── JwtService.cs         # JWT token handling
        │   ├── 📁 Repositories/
        │   │   ├── AuthRepository.cs     # Data access
        │   │   └── IAuthRepository.cs    # Repository interface
        │   └── 📁 Dtos/
        │       ├── LoginRequestDto.cs
        │       ├── RegisterRequestDto.cs
        │       ├── AuthResponseDto.cs
        │       └── JwtPayloadDto.cs
        │
        ├── 📁 Users/                     # User management feature
        │   ├── UserController.cs         # User endpoints
        │   ├── UserDependency.cs         # DI registration
        │   ├── 📁 Services/
        │   │   ├── UserService.cs        # Business logic
        │   │   └── IUserService.cs       # Service interface
        │   ├── 📁 Repositories/
        │   │   ├── UserRepository.cs     # Data access
        │   │   └── IUserRepository.cs    # Repository interface
        │   ├── 📁 Models/
        │   │   └── User.cs               # User entity
        │   └── 📁 Dtos/
        │       ├── UserDto.cs
        │       ├── UserResponseDto.cs
        │       ├── CreateUserDto.cs
        │       └── UpdateUserDto.cs
        │
        ├── 📁 Sessions/                  # Session management feature
        │   ├── SessionDependency.cs      # DI registration
        │   ├── 📁 Services/
        │   │   ├── SessionService.cs     # Business logic
        │   │   └── ISessionService.cs    # Service interface
        │   ├── 📁 Repositories/
        │   │   ├── SessionRepository.cs  # Data access
        │   │   └── ISessionRepository.cs # Repository interface
        │   ├── 📁 Models/
        │   │   └── Session.cs            # Session entity (with TTL)
        │   └── 📁 Dtos/
        │       ├── CreateSessionDto.cs
        │       └── SessionRequestDto.cs
        │
        └── 📁 Files/                     # File upload feature (Supabase)
            ├── FileController.cs         # File endpoints
            ├── FileDependency.cs         # DI registration
            ├── 📁 Services/
            │   ├── FileService.cs        # Business logic
            │   └── IFileService.cs       # Service interface
            ├── 📁 Repositories/
            │   ├── FileRepository.cs     # Data access (MongoDB metadata)
            │   └── IFileRepository.cs    # Repository interface
            ├── 📁 Models/
            │   └── FileMetadata.cs       # File metadata entity
            └── 📁 Dtos/
                ├── FileUploadRequestDto.cs
                ├── FileUploadResponseDto.cs
                └── FileTypes.cs          # File type constants
```

### Folder Explanations
- **Root**: Git configs, solution and README. 
- **LearnASPDotNet/**: web API project bootstrap, packages, HTTP samples, env example, app settings, Properties. 
- **Extensions/**: DI helpers (JWT, MongoDB, Supabase, Swagger).
- **Middlewares/**: Global exception handler.
- **Filters/**: Custom filters for validation and request handling.
- **Settings/**: strongly-typed settings (MongoDB).
- **Features/**: feature-oriented modules
  - **Auths/**: authentication (controller, service, repository, interfaces, DTOs).
  - **Users/**: user management (controller, service, repository, interfaces, models, DTOs).
  - **Sessions/**: session management (service, repository, interfaces, models, DTOs).
  - **Files/**: file upload to Supabase Storage (controller, service, repository, interfaces, models, DTOs).

### Key Features
- ✅ **JWT Authentication** - Access & Refresh Tokens with HttpOnly cookies
- ✅ **User Management** - CRUD operations
- ✅ **Session Management** - Refresh token management with TTL Index in MongoDB
- ✅ **File Upload** - Upload files (avatars, documents, images) to Supabase Storage
  - Auto-overwrite avatar on new upload
  - Metadata stored in MongoDB
  - Public URL for file access
- ✅ **Request Validation** - Custom filters for request data validation

### API Documentation
- Swagger UI (if enabled): visit `/swagger`

### Contributing
- Fork, feature branch, pull request. 

### License
Not set yet. 