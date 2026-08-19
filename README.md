# DevFile Manager

**DevFile Manager** is a command-line file management application developed in **C# and .NET**.

The project was created as part of my transition from **Java/Spring Boot to the .NET ecosystem**, with the goal of strengthening my knowledge of C#, .NET architecture, file system operations, dependency injection, exception handling, and software design principles before moving into ASP.NET Core development.

The application provides a custom command-line interface that allows users to navigate and interact with the local file system.

---


### Currently Implemented

* Display the current working directory
* List files and directories
* Navigate between directories
* Navigate to parent directories
* Create directories
* Create empty files
* Command parsing
* Basic error handling
* Help command
* Custom CLI
* Manual dependency injection

### Planned Features

* Read and write text files
* Copy files and directories
* Move files and directories
* Rename files
* Delete files and directories
* Recursive file search
* Search filters using LINQ
* File metadata
* SHA-256 hashing
* Duplicate file detection
* ZIP compression and extraction
* File system monitoring
* Command history
* Favorites
* JSON persistence
* Logging
* Async file operations
* Cancellation tokens
* Progress reporting
* Unit testing

---

##  Technologies

* C#
* .NET
* Visual Studio
* System.IO
* LINQ
* Git
* GitHub

No database, ORM, web framework, or external file-management library is used for the core functionality.

---

##  Project Architecture

```text
DevFileManager.sln
│
├── DevFileManager.Console
│   ├── Program.cs
│   ├── ConsoleApplication.cs
│   └── Presentation
│
├── DevFileManager.Application
│   ├── Commands
│   ├── Interfaces
│   ├── Models
│   └── Parsing
│
└── DevFileManager.Infrastructure
    └── FileSystem
```

### DevFileManager.Console

Responsible for the application's command-line interface and application startup.

It receives user input, displays results, and connects the different application components.

### DevFileManager.Application

Contains the application's abstractions and core contracts.

This layer includes:

* Interfaces
* Models
* Command definitions
* Command parsing
* Application rules

It does not depend directly on infrastructure implementations.

### DevFileManager.Infrastructure

Contains implementations that interact with the operating system and file system.

Examples include:

* File creation
* Directory creation
* File enumeration
* Path resolution

---

##  Dependency Flow

```text
Console
   │
   ├──────────────► Application
   │
   └──────────────► Infrastructure
                         │
                         ▼
                    Application
```

The project uses abstractions to reduce coupling between application logic and infrastructure.

Example:

```csharp
public interface IFileSystemService
{
    IReadOnlyList<string> ListEntries(string path);

    void CreateDirectory(string path);

    void CreateFile(string path);

    bool DirectoryExists(string path);
}
```

The `Infrastructure` layer provides the implementation of this contract.

---

##  Available Commands

| Command            | Description                            |
| ------------------ | -------------------------------------- |
| `pwd`              | Displays the current working directory |
| `list`             | Lists files and directories            |
| `ls`               | Alias for `list`                       |
| `cd <path>`        | Changes the current directory          |
| `cd ..`            | Navigates to the parent directory      |
| `mkdir <name>`     | Creates a directory                    |
| `touch <filename>` | Creates an empty file                  |
| `help`             | Displays available commands            |
| `exit`             | Closes the application                 |

---

## ▶️ Example

```text
========================================
            DEVFILE MANAGER
========================================
Type 'help' to see available commands.

Current directory:
C:\Development

devfile> mkdir MyProject

Directory created successfully.

devfile> cd MyProject

devfile> touch notes.txt

File created successfully.

devfile> list

[FILE] notes.txt

devfile> pwd

C:\Development\MyProject

devfile> cd ..

devfile> list

[DIR] MyProject

devfile> exit
```

---

##  Getting Started

### Requirements

Make sure you have installed:

* .NET SDK
* Visual Studio 2022 or later

Recommended Visual Studio workload:

```text
.NET desktop development
```

### Clone the Repository

```bash
git clone <repository-url>
```

Navigate into the project:

```bash
cd DevFileManager
```

Restore dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Run the console project:

```bash
dotnet run --project DevFileManager.Console
```

