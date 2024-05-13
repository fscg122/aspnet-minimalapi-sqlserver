# Minimal API with Basic Authentication

This project showcases a Minimal API developed using ASP.NET. It provides simple RESTful endpoints to manage contacts stored in a SQL Server database. The API is secured using Basic Authentication for accessing sensitive data.

## Features

- **Basic Authentication**: Secures API endpoints with username and password authentication.
- **SQL Server Integration**: Connects seamlessly to a SQL Server database for data storage and retrieval.
- **Environment Variables**: Utilizes environment variables to configure sensitive information like connection strings.
- **Efficient and Lightweight**: Built with ASP.NET Minimal API, ensuring minimal overhead and maximum performance.

## Getting Started

1. **Clone the Repository**: Clone this repository to your local machine.
2. **Configure Environment Variables**: Set up environment variables for the database connection string and authentication credentials.
3. **Build and Run**: Build and run the project using your preferred IDE or command-line tools.

## Usage

### Endpoints

- `GET /api/contact`: Retrieves all contacts from the database.
- `GET /api/contact/{id}`: Retrieves a specific contact by ID.
- `POST /api/contact`: Creates a new contact.
- `PUT /api/contact/{id}`: Updates an existing contact.
- `DELETE /api/contact/{id}`: Deletes a contact by ID.

### Authentication

- Basic Authentication is required to access the API endpoints.
- Username and password are provided as environment variables or configured in the Azure App Service where the API is deployed.

## License

This project is licensed under the [MIT License](LICENSE).
