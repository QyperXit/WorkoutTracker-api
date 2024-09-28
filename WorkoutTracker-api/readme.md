# My First ASP.NET Core API - Workout Tracker

Welcome to my first ASP.NET Core API project! This repository contains a simple API built using ASP.NET Core to help me learn and practice building RESTful services. The API is a **Workout Tracker**, and currently includes user management with basic CRUD operations and authentication features.

## Project Overview

This API is designed to manage users and will eventually include features for tracking workouts and other fitness-related data. The current features include:

- **User Management**: Create, Read, Update, and Delete (CRUD) operations for users.
- **Authentication**: User login with JWT (JSON Web Token) for secure access.

### Features

- **CRUD Operations for Workouts**:
  - `GET /api/Workout`: Retrieve a list of all workouts.
  - `GET /api/Workout/{id}`: Retrieve details of a specific workout by ID.
  - `POST /api/Workout`: Create a new workout.
  - `PUT /api/Workout/{id}`: Update an existing workout's details.
  - `DELETE /api/Workout/{id}`: Delete a workout by ID.
- **CRUD Operations for Exercises**:
  - `GET /api/Exercise`: Retrieve a list of all exercises.
  - `GET /api/Exercise/{id}`: Retrieve details of a specific exercise by ID.
  - `POST /api/Exercise`: Create a new exercise.
  - `PUT /api/Exercise/{id}`: Update an existing exercise's details.
  - `DELETE /api/Exercise/{id}`: Delete an exercise by ID.
- **CRUD Operations for Exercise Equipment**:

  - `GET /api/ExerciseEquipment`: Retrieve a list of all exercise equipment.
  - `GET /api/ExerciseEquipment/{id}`: Retrieve details of a specific exercise equipment by ID.
  - `POST /api/ExerciseEquipment`: Create a new exercise equipment.
  - `PUT /api/ExerciseEquipment/{id}`: Update an existing exercise equipment's details.
  - `DELETE /api/ExerciseEquipment/{id}`: Delete an exercise equipment by ID.

- **User Authentication**:
  - `POST /api/User/login`: Authenticate a user and receive a JWT token.

### Future Features

- **Integration with Third-Party Services**: Integration with other fitness apps or services for enhanced functionality.
- **Frontend Client**: Building a frontend client using React/Next.js or similar technology for a user-friendly interface.

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
   git clone https://github.com/QyperXit/WorkoutTracker-api
   cd WorkoutTracker-api
   ```
