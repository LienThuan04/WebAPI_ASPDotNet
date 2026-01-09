# WebAPI_ASPDotNet

- Chủ sở hữu / Owner: [@LienThuan04](https://github.com/LienThuan04)
- Repository: [LienThuan04/WebAPI_ASPDotNet](https://github.com/LienThuan04/WebAPI_ASPDotNet)

Ngôn ngữ: [Tiếng Việt](#tiếng-việt) | [English](#english)

---

## Tiếng Việt

### Tổng quan
Dự án Web API xây dựng bằng ASP.NET (C#) nhằm cung cấp các endpoint RESTful cho ứng dụng/dịch vụ phía client. Có thể mở rộng để tích hợp cơ sở dữ liệu, xác thực, logging, v.v.

### Yêu cầu hệ thống
- .NET SDK (phiên bản phù hợp với dự án)
- IDE/Editor: Visual Studio, Rider hoặc VS Code (cài C# extension)

### Hướng dẫn chạy nhanh
```bash
# 1) Khôi phục gói
dotnet restore

# 2) Biên dịch
dotnet build

# 3) Chạy ứng dụng
dotnet run
```

Sau khi khởi động, ứng dụng sẽ lắng nghe theo cấu hình trong `Properties/launchSettings.json`.  
Nếu bật Swagger, truy cập `/swagger` trên địa chỉ localhost để xem và thử nghiệm API.

### Cấu trúc thư mục (mẫu)
Lưu ý: Đây là ví dụ để tham khảo/chuẩn hóa. Hãy cập nhật cho đúng với dự án thực tế.
```
WebAPI_ASPDotNet/
├─ Controllers/        # Các controller (endpoint)
├─ Models/             # Model/DTO
├─ Services/           # Tầng nghiệp vụ
├─ Repositories/       # Truy cập dữ liệu
├─ Program.cs          # Điểm khởi động
├─ appsettings.json    # Cấu hình ứng dụng
└─ Properties/
   └─ launchSettings.json
```

### Tài liệu API
- Swagger UI (nếu bật): truy cập `/swagger`
- Postman Collection/Docs (nếu có): thêm liên kết tại đây

### Đóng góp
- Fork repo, tạo nhánh tính năng, mở pull request mô tả thay đổi.
- Tuân thủ quy ước code và tiêu chuẩn đặt tên của dự án.

### Giấy phép
Chưa thiết lập. Vui lòng thêm tệp LICENSE nếu cần.

---

## English

### Overview
An ASP.NET (C#) Web API project providing RESTful endpoints for client applications/services. It can be extended with database integration, authentication, logging, etc.

### Requirements
- .NET SDK (version compatible with the project)
- IDE/Editor: Visual Studio, Rider, or VS Code (with C# extension)

### Quick Start
```bash
# 1) Restore packages
dotnet restore

# 2) Build
dotnet build

# 3) Run
dotnet run
```

After starting, the app listens per `Properties/launchSettings.json`.  
If Swagger is enabled, navigate to `/swagger` on localhost to explore and test the API.

### Folder Structure (example)
Note: This is a sample structure. Adjust to match the actual project layout.
```
WebAPI_ASPDotNet/
├─ Controllers/        # Controllers (endpoints)
├─ Models/             # Models/DTOs
├─ Services/           # Business logic layer
├─ Repositories/       # Data access layer
├─ Program.cs          # Entry point
├─ appsettings.json    # App configuration
└─ Properties/
   └─ launchSettings.json
```

### API Documentation
- Swagger UI (if enabled): visit `/swagger`
- Postman Collection/Docs (if any): add a link here

### Contributing
- Fork the repo, create a feature branch, open a pull request describing your changes.
- Follow the project's coding conventions and naming standards.

### License
Not set yet. Please add a LICENSE file if needed.