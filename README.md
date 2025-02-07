# ASP.NET Core Web API with Identity and JWT Authentication

## 📌 Overview
This project is designed as a **learning resource** to understand and implement **JWT-based authentication and refresh tokens** for securing an API. The goal is to build a **robust and secure back-end** that can be used with a **front-end application for taking notes with user authentication**.

Through this project, you'll applied:
- **How to implement authentication & authorization using JWT**
- **How to manage user identities with ASP.NET Identity**
- **How to use refresh tokens for secure session management**
- **How to integrate a secure API with a front-end application**

The API is structured to handle **user authentication**, **token refresh**, and **role-based access control**, making it ideal for building applications where users need **secure, authenticated access** to their own data.

## 🛠 Technologies
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **ASP.NET Identity**
- **JWT Authentication**
- **SQL Server**

---

## 📦 Installation

### 1️⃣ Clone the Repository
```sh
git clone https://github.com/yourusername/your-repo.git
cd your-repo
```

## Set up the Database
###Update the connection string in appsettings.json:

```sh
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YourDatabase;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

###Run database migrations:
```sh
dotnet ef database update
```

###Run the application
```sh
dotnet run
```

## 🔑 Authentication & Authorization

###1️⃣ User Registration

###2️⃣ User Login

###3️⃣ Access Protected Route

### 📂 Project Structure

📦 NotesAPI  
 ┃ ┣ 📜 JwtSettings.cs  
 ┣ 📜 appsettings.json  
 ┣ 📜 Program.cs  
 ┣ 📜 Startup.cs  
 ┣ 📂 Controllers  
 ┃ ┣ 📜 AuthController.cs  
 ┃ ┣ 📜 LoginController.cs  
 ┃ ┣ 📜 NoteController.cs  
 ┃ ┣ 📜 TagController.cs  
 ┃ ┣ 📜 UserController.cs  
 ┣ 📂 DTO  
 ┃ ┣ 📜 LoginRequest.cs  
 📦 NotesAPI.DataAccess.EF  
 ┣ 📂 Context  
 ┃ ┣ 📜 NotesAPIDbContext.cs  
 ┣ 📂 Models  
 ┃ ┣ 📜 Note.cs  
 ┃ ┣ 📜 NoteTag.cs  
 ┃ ┣ 📜 RefreshTokenRequest.cs  
 ┃ ┣ 📜 Tag.cs  
 ┃ ┣ 📜 User.cs  
 ┣ 📂 Repositories  
 ┃ ┣ 📜 NoteRepository.cs  
 ┃ ┣ 📜 TagRepository.cs  
 ┃ ┣ 📜 userRepository.cs  
 📦 NotesAPI.DataAccess.EF  
 ┃ ┣ 📜 AuthControllerTest.cs  
 ┃ ┣ 📜 LoginControllerTest.cs  
 ┃ ┣ 📜 NoteControllerTest.cs  
 ┃ ┣ 📜 TagControllerTest.cs  
 ┃ ┣ 📜 UserControllerTest.cs  

### 🔗 Useful Commands

## Create a migration:
```sh
dotnet ef migrations add InitialCreate
```

## Update the database:
```sh
dotnet ef database update
```

## Run the project:
```sh
dotnet run
```

### 🚀 Next Steps

✅ Add role-based authorization
✅ Improve error handling
✅ Integrate with a front-end application


### 📝 License
