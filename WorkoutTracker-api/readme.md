# My First ASP.NET Core API - Workout Tracker

Welcome to my first ASP.NET Core API project! This repository contains a simple API built using ASP.NET Core to help me learn and practice building RESTful services. The API is a **Workout Tracker**, and currently includes user management with basic CRUD operations and authentication features.

## Project Overview

This API is designed to manage users and will eventually include features for tracking workouts and other fitness-related data. The current features include:

- **User Management**: Create, Read, Update, and Delete (CRUD) operations for users.
- **Authentication**: User login with JWT (JSON Web Token) for secure access.

### Features

- **CRUD Operations for Users**:
    - `GET /api/User`: Retrieve a list of all users.
    - `GET /api/User/{id}`: Retrieve details of a specific user by ID.
    - `POST /api/User`: Create a new user.
    - `PUT /api/User/{id}`: Update an existing user's details.
    - `DELETE /api/User/{id}`: Delete a user by ID.

- **User Authentication**:
    - `POST /api/User/login`: Authenticate a user and receive a JWT token.

### Future Features

- **Workout Tracking**: Adding endpoints to track workouts, including exercise details, sets, reps, and more.
- **Progress Tracking**: Features to track progress over time with various metrics.
- **Integration with Third-Party Services**: Integration with other fitness apps or services for enhanced functionality.

### Technologies Used

- **ASP.NET Core**: The web framework used to build the API.
- **Entity Framework Core**: ORM used for data access.
- **JWT**: For secure user authentication.
- **BCrypt**: For password hashing.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (5.0 or later)
- [MySQL or MariaDB](https://dev.mysql.com/downloads/installer/) (or any compatible database)
- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/)

### Setup

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/yourusername/your-repo-name.git
   cd your-repo-name

