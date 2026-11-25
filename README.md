# Lista Zadan

`Lista Zadan` is a cross-platform task-manager application built with .NET MAUI. It provides a login and registration flow, stores users and tasks in a local SQLite database, and allows tasks to be added, edited, completed, and deleted.


## Features

- User registration and sign-in screens
- Local SQLite storage
- Create tasks with:
  - Name
  - Description
  - Due date
  - Due time
  - Completion status
- Edit existing tasks
- Delete tasks
- Mark tasks as completed
- Cross-platform MAUI targets for Android, iOS, Mac Catalyst, and Windows

## Technology

- .NET 8
- .NET MAUI
- C#
- XAML
- MVVM via CommunityToolkit.Mvvm
- sqlite-net-pcl
- SQLitePCLRaw

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- .NET MAUI workload
- Visual Studio 2022 with the **.NET Multi-platform App UI development** workload, or the appropriate MAUI CLI tools
- Android, iOS, Mac Catalyst, or Windows build requirements for the selected target platform

The project is configured for Windows with Visual Studio and also declares Android, iOS, and Mac Catalyst target frameworks.

## Install the MAUI workload

```powershell
dotnet workload install maui
```

## Project structure

```text
ListaZadan_2/
|-- ListaZadan_2.sln
`-- ListaZadan_2/
    |-- App.xaml
    |-- AppShell.xaml
    |-- MauiProgram.cs
    |-- MainPage.xaml(.cs)
    |-- RegisterPage.xaml(.cs)
    |-- ForgotPage.xaml(.cs)
    |-- Tasks.xaml(.cs)
    |-- ModelView/
    |   |-- UserModelView.cs
    |   `-- UserTasksModelView.cs
    |-- Models/
    |   |-- User.cs
    |   `-- UserTasks.cs
    |-- Services/
    |   `-- SQLiteDB.cs
    |-- Platforms/
    `-- Resources/
```

## Database

The application creates a SQLite database named `ListaZadan.db3` in the platform-specific application data directory.

The database contains two models:

### `User`

| Property | Description |
| --- | --- |
| `Id` | Auto-incrementing primary key |
| `Name` | Username |
| `Email` | Email address |
| `Password` | Password |

### `UserTasks`

| Property | Description |
| --- | --- |
| `Id` | Auto-incrementing primary key |
| `UserId` | Owner of the task |
| `TaskName` | Task title |
| `Description` | Task description |
| `DueDate` | Due date and time |
| `IsCompleted` | Completion status |

Database operations are centralized in `SQLiteDB.cs`, which automatically creates tables when needed and supports create, read, update, and delete operations.

## Build

From the repository root:

```powershell
dotnet restore ListaZadan_2.sln
dotnet build ListaZadan_2.sln
```

Build a specific target framework when needed:

```powershell
dotnet build ListaZadan_2/ListaZadan_2.csproj -f net8.0-windows10.0.19041.0
dotnet build ListaZadan_2/ListaZadan_2.csproj -f net8.0-android
dotnet build ListaZadan_2/ListaZadan_2.csproj -f net8.0-maccatalyst
```

iOS builds require a compatible Apple development environment and provisioning configuration.

## Run

Open `ListaZadan_2.sln` in Visual Studio 2022, select an Android, iOS, Mac Catalyst, or Windows device/target, and start the application.

Using the .NET CLI:

```powershell
dotnet run --project ListaZadan_2/ListaZadan_2.csproj -f net8.0-windows10.0.19041.0
```

## Usage

1. Open the app.
2. Register a user with a username and password.
3. Sign in.
4. Use the task panel to enter a task name, description, date, and time.
5. Select **Add Task** to save the task.
6. Use **Edit** to load a task into the form, modify it, and save it.
7. Use **Del** to remove a task.
8. Use the checkboxes to change completion status.

## Architecture

- `AppShell.xaml` defines the application shell and initial login route.
- `UserModelView` manages user registration and authentication operations.
- `UserTasksModelView` manages task loading, selection, saving, updating, and deletion.
- `SQLiteDB` provides generic asynchronous SQLite operations.
- `Models` contain the persisted data structures.
- XAML pages provide the user interface and bind to MVVM view models.

## Known limitations

- Password recovery UI is currently present, but its recovery action is not implemented.
- Authentication currently stores and compares passwords directly in the database; passwords are not hashed.
- The login flow and task list are not yet fully isolated by logged-in user.
- The login and task operations contain limited error handling.
