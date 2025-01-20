# BloggingApp

**BloggingApp** is a simple blogging platform built with **ASP.NET Core MVC** and **Entity Framework Core**. The application allows users to register, log in, create blog posts, edit their profiles, and manage their content in a user-friendly environment.

---

## Features

- **User Authentication**
  - Register and log in with username and password.
  - Session-based authentication for secure access.

- **User Profiles**
  - Edit user details such as name and email.
  - View all blog posts authored by the user.

- **Blog Management**
  - Create, read, update, and delete blog posts.
  - View blog posts by other users with details like author and creation date.

- **Responsive Design**
  - Minimal and simple design using Bootstrap and custom CSS.

---

## Tech Stack

- **Backend**: ASP.NET Core MVC, C#
- **Frontend**: Razor Views, Bootstrap, Custom CSS
- **Database**: MySQL (via Entity Framework Core)
- **Tools**: Visual Studio, Git

---

## Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/) with ASP.NET and .NET Core workloads installed
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MySQL Server](https://dev.mysql.com/downloads/)
- [Git](https://git-scm.com/)

---

## Installation

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/BloggingApp.git
cd BloggingApp
```

### 2. Configure the Database

- Update the `appsettings.json` file with your MySQL connection string:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=blogdb;User=root;Password=yourpassword;"
  }
  ```

### 3. Apply Database Migrations

```bash
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run
```

5. Open the application in your browser at [http://localhost:5000](http://localhost:5000).

---

## Folder Structure

```
BloggingApp/
├── Controllers/
│   ├── AccountController.cs    # Handles login, registration, and logout
│   ├── BlogController.cs       # CRUD operations for blog posts
│   ├── ProfileController.cs    # Profile management
├── Models/
│   ├── AppUser.cs              # Represents a user
│   ├── BlogPost.cs             # Represents a blog post
│   ├── BloggingContext.cs      # Entity Framework DbContext
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml
│   │   ├── Register.cshtml
│   ├── Blog/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   ├── Profile/
│       ├── Index.cshtml
│       ├── Edit.cshtml
├── wwwroot/
│   ├── css/                    # Custom CSS for styling
│   ├── lib/                    # Bootstrap and jQuery libraries
├── appsettings.json            # Configuration for database and other settings
├── Program.cs                  # Application startup
```

---

## Features Walkthrough

### 1. User Authentication
- Register and log in with session-based authentication for secure access.

### 2. User Profiles
- View and edit user profiles, including their email and name.
- View all blog posts created by the logged-in user.

### 3. Blog Management
- Create, update, and delete blog posts with a simple UI.
- View blog posts authored by other users.

---

## Screenshots

### 1. Login Page
![Login Page](imgs/img1.PNG)

### 2. Main Feed
![Blog Dashboard](imgs/img2.PNG)

### 3. Create New Post
![Create](imgs/img3.PNG)

### 4. View Profile
![Profile](imgs/img4.PNG)

### 5. Edit Profile
![Edit](imgs/img5.PNG)

---

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature-name
   ```
3. Make your changes and commit them:
   ```bash
   git commit -m "Add your message here"
   ```
4. Push to the branch:
   ```bash
   git push origin feature-name
   ```
5. Open a Pull Request.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.



