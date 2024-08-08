# Daily Dish Service

The Daily Dish API is designed to manage daily dish information from various restaurants. It provides functionalities to retrieve and cache daily dish data, ensuring efficient and quick access to the latest dish information.

## Technologies
* Background Jobs
* Rest API

Project is created with:
- Hangfire (1.8.14)
- MediatR (11.0.0)
- FusionCache (1.3.0)

## Background Service

Updates cache with the latest daily dish information from various providers.

![DailyDishService_job](https://dawidflorian.pl/file/DailyDishService_job.png)

## API Specification

### DailyDish/exists

Checks the existence of daily dish data for specified restaurants.

![DailyDishService_exists](https://dawidflorian.pl/file/DailyDishService_exists.png)

### DailyDish/list

Retrieves a list of daily dishes from the cache.

![DailyDishService_list](https://dawidflorian.pl/file/DailyDishService_list.png)

### OpenAPI

```json
{
  "openapi": "3.0.1",
  "info": {
    "title": "Api",
    "version": "1.0"
  },
  "paths": {
    "/api/DailyDish/list": {
      "get": {
        "tags": [
          "DailyDish"
        ],
        "parameters": [
          {
            "name": "Names",
            "in": "query",
            "schema": {
              "type": "array",
              "items": {
                "type": "string"
              }
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "object",
                  "additionalProperties": { }
                }
              },
              "application/json": {
                "schema": {
                  "type": "object",
                  "additionalProperties": { }
                }
              },
              "text/json": {
                "schema": {
                  "type": "object",
                  "additionalProperties": { }
                }
              }
            }
          }
        }
      }
    },
    "/api/DailyDish/exists": {
      "get": {
        "tags": [
          "DailyDish"
        ],
        "parameters": [
          {
            "name": "Names",
            "in": "query",
            "schema": {
              "type": "array",
              "items": {
                "type": "string"
              }
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/ExistsDto"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/ExistsDto"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/ExistsDto"
                  }
                }
              }
            }
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "ExistsDto": {
        "type": "object",
        "properties": {
          "createdDate": {
            "type": "string",
            "format": "date-time"
          },
          "name": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      }
    }
  }
}
```

## Getting Started

### Prerequisites
- .NET Core SDK 8

### Installation
1. Clone the repository:

   ```bash
   git clone https://github.com/git-df/dotnet-DailyDishService.git
   ```

2. Navigate to the project directory:

   ```bash
   cd dotnet-DailyDishService\Api\
   ```
   
3. Restore dependencies:

   ```bash
   dotnet restore
   ```
  
4. Update the configuration settings in appsettings.json

- Configure cache and background job settings.

### Running the Application
1. Build the project:

   ```bash
   dotnet build
   ```

2. Run the project:

   ```bash
   dotnet run
   ```

