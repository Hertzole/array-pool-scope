# Array Pool Scope

Array Pool Scope allows you to use ArrayPool in a scope-like manner.

## 💨 Quick Start

```csharp
using Hertzole.Buffers;

int length = 10;
// Use a using statement to automatically return the array to the pool.
using (ArrayPoolScope<int> pool = new ArrayPoolScope<int>(length))
{
    // For loop
    for (int i = 0; i < pool.Count; i++)
    {
        pool.Array[i] = i;
        Console.WriteLine(pool.Array[i]);
    }

    // Foreach loop
    foreach (int item in pool)
    {
        Console.WriteLine(item);
    }
}

// Manully return the array to the pool.
ArrayPoolScope<int> pool = new ArrayPoolScope<int>(length);
pool.Dispose();

// Provide your own pool.
ArrayPool<int> customPool = ArrayPool<int>.Create();
using ArrayPoolScope<int> pool = new ArrayPoolScope<int>(length, customPool);

// Get directly from pool
using ArrayPoolScope<int> pool = ArrayPool<int>.Shared.RentScope(length);

// As span and memory
using ArrayPoolScope<int> pool = ArrayPool<int>.Shared.RentScope(length);
Span<int> span = pool.AsSpan();
Memory<int> memory = pool.AsMemory();
```

## 📦 Installation

You can install the package via NuGet. The package supports .NET Standard 2.0 and up.

```bash
dotnet add package Hertzole.ArrayPoolScope
```

### Unity Installation

#### OpenUPM (Recommended)

The minimum Unity version for Array Pool Scope is 2021.3.

You can install the package through [OpenUPM](https://openupm.com/) by using the [OpenUPM CLI](https://github.com/openupm/openupm-cli#openupm-cli).

```bash
openupm add se.hertzole.array-pool-scope
```

If you don't have the CLI installed, you can follow these steps:

1. Open Edit/Project Settings/Package Manager
2. Add a new Scoped Registry (or edit the existing OpenUPM entry)   
     Name: `package.openupm.com`  
     URL: `https://package.openupm.com`  
     Scope: `se.hertzole.array-pool-scope`
3. Click `Save` (or `Apply`)
4. Open Window/Package Manager
5. Click `+`
6. Select `Add package by name...` or `Add package from git URL...`
7. Paste `se.hertzole.array-pool-scope` into name 
8. Click `Add`

#### Unity Package Manager

You can install the package through the Unity Package Manager.

1. Open `Window/Package Manager`
2. Click the `+` in the top left corner
3. Select `Add package from git URL...`
4. Paste `https://github.com/Hertzole/array-pool-scope.git#upm`

## 💻 Development

### Requirements

For standard .NET development, you need the following:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

For Unity development, you need the following:
- [Unity 2021.3.0f1](https://unity.com/releases/editor/whats-new/2021.3.0)

### Building

To build the project, you can use the `dotnet` CLI.

```bash
dotnet build
```

### Testing

To run the tests, you can use the `dotnet` CLI.

```bash
dotnet test
```

### Unity

The main SDK should be the "single source of truth". This means that all code should be written in the main project and then copied over to the Unity project. 

To open the project in Unity, you need to open the `Unity` folder as a project.

## 🤝 Contributing

Contributions, issues and feature requests are welcome!

Please make sure your pull requests are made to the `develop` branch and that you have tested your changes. If you're adding a new feature, please also add tests for it.

Your code should follow the [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions). Your commits should follow the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) standard.

## 📃 License

[MIT](https://github.com/Hertzole/array-pool-scope/blob/master/LICENSE)