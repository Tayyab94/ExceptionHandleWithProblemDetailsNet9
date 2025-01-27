# Problem Details for ASP.NET Core APIs

This repository contains examples and implementations for using **Problem Details** in ASP.NET Core APIs. Problem Details is a standardized way to communicate errors and other problem information in HTTP APIs, as defined in [RFC 7807](https://tools.ietf.org/html/rfc7807).

## What are Problem Details?

Problem Details is a format for describing errors in HTTP APIs. It provides a consistent way to return error information, including:

- A machine-readable `type` URI to identify the problem.
- A human-readable `title` to summarize the problem.
- The HTTP `status` code.
- A detailed `detail` message.
- Additional custom properties for context.

This format improves API usability by making error responses more descriptive and consistent.

## Features

- Implementation of the `ProblemDetails` class in ASP.NET Core.
- Custom middleware for handling exceptions and generating Problem Details responses.
- Examples of returning Problem Details for common HTTP errors (e.g., 400 Bad Request, 404 Not Found, 500 Internal Server Error).
- Support for custom problem types and extensions.

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later.
- An IDE or editor (e.g., Visual Studio, Visual Studio Code, or JetBrains Rider).

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/your-repo-name.git
   ```
2. Navigate to the project directory:
   ```bash
   cd your-repo-name
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

### Usage

1. Start the API and navigate to the endpoints to see Problem Details in action.
2. Use the provided examples to integrate Problem Details into your own ASP.NET Core APIs.

## Example

Here’s an example of a Problem Details response for a `404 Not Found` error:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404,
  "detail": "The requested resource was not found.",
  "instance": "/api/resource/123"
}
```

## Customizing Problem Details

You can extend the `ProblemDetails` class to include additional properties specific to your application:

```csharp
public class CustomProblemDetails : ProblemDetails
{
    public string CustomProperty { get; set; }
}
```

## Contributing

Contributions are welcome! If you have suggestions, bug reports, or feature requests, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

- [RFC 7807](https://tools.ietf.org/html/rfc7807) for defining the Problem Details specification.
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/) for providing the foundation for this implementation.

---
