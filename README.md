# MemePlatform 🎭

A **cross-platform meme editor & social feed** built with **.NET MAUI Blazor Hybrid** (iOS, Android, Windows, macOS) and **Blazor WebAssembly** (web). Users can create memes (image + text overlays), share them, like/comment on posts, and explore a social-style feed. The backend is powered by **ASP.NET Core Web API** with **Entity Framework Core** and **JWT authentication**.

---

## ✨ Features
- 📱 Cross-platform UI  
  - Native app via .NET MAUI Blazor Hybrid  
  - Web app via Blazor WebAssembly  
- 🖼️ Meme Editor  
  - Add text overlays, stickers, and combine images  
  - Save to device or upload to feed  
- 💬 Social Feed  
  - Browse memes from other users  
  - Like & comment functionality  
- 🔐 Authentication  
  - User registration & login  
  - JWT-based API authentication  
- 🗄️ Backend API  
  - Built with ASP.NET Core Web API  
  - Database persistence via Entity Framework Core  

---

## 🛠 Tech Stack
- **Frontend**
  - .NET MAUI Blazor Hybrid (mobile & desktop)
  - Blazor WebAssembly (web)
  - Shared Razor Class Library for reusable UI
- **Backend**
  - ASP.NET Core Web API
  - Entity Framework Core (SQL Server, PostgreSQL, or SQLite)
  - JWT Authentication
- **Other**
  - RESTful API architecture
  - HttpClient for API communication
  - Dependency Injection throughout

---

## 📂 Project Structure
- MemePlatform.Shared – Razor Class Library (UI components, models)  
- MemePlatform.App – .NET MAUI Blazor Hybrid app  
- MemePlatform.Web – Blazor WebAssembly app  
- MemePlatform.Api – ASP.NET Core Web API (EF Core + Auth)  
- MemePlatform.Client – Optional API client library (DTOs & services)  

---

## 🏗️ Architecture
**Frontend**  
- .NET MAUI Blazor Hybrid (iOS, Android, Windows, macOS)  
- Blazor WebAssembly (Web Browser)  
- Shared Razor Class Library (UI Components, DTOs)  

**Backend**  
- ASP.NET Core Web API  
- Entity Framework Core  
- Database  

**Flow**  
- MAUI and Blazor Web clients communicate with the Web API using HttpClient and JWT  
- The Web API uses EF Core to persist data to the database  
- Shared Razor Class Library is reused across MAUI and Web  

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK  
- Visual Studio 2022/2023 (with MAUI, ASP.NET, and Blazor workloads installed)  
- SQL Server / PostgreSQL / SQLite for the database  

### Setup
1. Clone the repository:  
   git clone https://github.com/your-username/MemePlatform.git  
   cd MemePlatform/src  

2. Update `appsettings.json` in **MemePlatform.Api** with your database connection string.  

3. Apply EF Core migrations:  
   dotnet ef database update --project MemePlatform.Api  

4. Run the backend API:  
   dotnet run --project MemePlatform.Api  

5. Run the frontend apps:  
   - Web: dotnet run --project MemePlatform.Web  
   - MAUI (mobile/desktop): Open **MemePlatform.App** in Visual Studio and run on emulator/device  

---

## 🔐 Authentication Flow
1. User registers or logs in via `/api/auth/*` endpoints.  
2. API returns a JWT token.  
3. MAUI/Web apps store the token (SecureStorage for MAUI, localStorage for Web).  
4. All API requests include `Authorization: Bearer <token>`.  

---

## 📈 Roadmap
- [ ] Meme editor with image layers & drag/drop text  
- [ ] User profiles & avatars  
- [ ] Social feed with infinite scroll  
- [ ] Push notifications (MAUI)  
- [ ] Deployment pipelines (CI/CD to Azure/AWS)  

---

## 🤝 Contributing
Contributions, issues, and feature requests are welcome!  
Feel free to open a PR or file an issue.  

---

## 📜 License
MIT License. See LICENSE for details.  
