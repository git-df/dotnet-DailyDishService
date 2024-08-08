# Daily Dish Service

The Daily Dish API is designed to manage daily dish information from various restaurants. It provides functionalities to retrieve and cache daily dish data, ensuring efficient and quick access to the latest dish information.

## Technologies
* Background Jobs
* Rest API

Project is created with:
- Hangfire (1.8.14)
- MediatR (11.0.0)
- FusionCache (1.3.0)

Project Structure
The project consists of the following main components:

## Getting Started

### Prerequisites
- .NET Core SDK 8

### Installation
1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/daily-dish-api.git

2. Navigate to the project directory:

   ```bash
   cd daily-dish-api
   
3. Restore dependencies:

   ```bash
   dotnet restore
  
4. Update the configuration settings in appsettings.json

- Configure cache and background job settings.

### Running the Application
1. Build the project:

   ```bash
   dotnet build

2. Run the project:

   ```bash
   dotnet run

