# 🗂️ Blazor AWS S3 File Upload Demo

A lightweight, production-style `.NET 9 Blazor (Interactive Server)` application for uploading multiple documents to Amazon S3. Built with enterprise-grade patterns, this project demonstrates clean architecture, service abstraction, and cloud integration best practices in a modern Blazor application.

---

## 🚀 Features

- 🔐 Upload up to 4 files securely to an S3 bucket
- 🌐 Built using **Blazor Server with .NET 9 (Interactive Server Render Mode)**
- 🧩 Clean layered architecture (Presentation, Business, Data, Shared)
- ☁️ AWS S3 integration using `IAmazonS3`
- ✅ Strongly-typed result pattern for reliable responses (`Result<T>`)
- 📝 Input validation and error display using `EditForm` and `DataAnnotationsValidator`
- 🎨 TailwindCSS-styled UI with a responsive, user-friendly layout

---

## 📁 File Upload Flow

1. **Frontend (Blazor Page)**  
   The user uploads files through a clean UI powered by Blazor Interactive Server.  
   `@page "/UploadFile"` provides a four-file upload form with inline validation and styled dropzones.

2. **Business Layer (`DocumentManager`)**  
   Files are passed to a central manager class that handles:
   - Validation
   - Delegation to an `IFileService`
   - Logging (commented for integration with your favorite provider)
   - Persistence logic if needed

3. **Service Layer (`IFileService`)**  
   Responsible for abstracting AWS S3 upload logic. Modular and easily testable.

4. **Result Pattern**  
   Every operation returns a `Result<T>` model, encapsulating success, data, and error messages cleanly.

---

## 🧠 Code Highlights

### `Result<T>` Model

```csharp
public class Result<T> {
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public string? ErrorMessage { get; private set; }
    
    public static Result<T> Success(T data) => new(true, data, null);
    public static Result<T> Failure(string error) => new(false, default, error);
}
```

### `DocumentManager` Upload Logic (simplified)

```csharp
if (docs is null || docs.File1 is null)
    return Result<DocumentsDTO>.Failure("Documents cannot be empty.");

var upload1 = await _fileService.UploadFile(...);
if (upload1.IsSuccess)
    await RecordDocuments(upload1.Data, ...);
```

---

## 🛠️ Tech Stack

- **.NET 9 / Blazor Server (Interactive)**
- **Amazon AWS SDK for .NET (S3)**
- **EF Core (via `ApplicationDbContext`)**
- **TailwindCSS for responsive design**
- **Dependency Injection, `IOptions<>`, and service interfaces**

---

## 📸 UI Snapshot

> File upload form with error handling and styled dropzones (see `/UploadFile` Razor page)

---

## 🧪 How to Run

1. Clone the repo
2. Set your AWS credentials and S3 bucket in `appsettings.json` or via `S3Settings`
3. Run the project:
   ```bash
   dotnet run
   ```
4. Navigate to `/UploadFile` and start uploading

---

## 🔐 Security Note

Make sure you apply proper authentication/authorization in production environments. This demo assumes a trusted internal context.

---

## 📄 License

MIT — feel free to use, modify, and build upon this project.
