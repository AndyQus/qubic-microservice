# Qubic Microservice

This is an ASP.NET Core Web API microservice that provides an endpoint to retrieve tick events from the Qubic API.

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)

## Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/YourUsername/qubic-microservice.git
    cd qubic-microservice
    ```

2. Install the dependencies:

    ```bash
    dotnet restore
    ```

3. Ensure `Newtonsoft.Json` is installed:

    ```bash
    dotnet add package Newtonsoft.Json
    ```

## Starting the Application

Start the application with:

```bash
dotnet run