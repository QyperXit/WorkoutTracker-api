# My First ASP.NET Core API - Workout Tracker

Welcome to my first ASP.NET Core API project! This repository contains a simple API built using ASP.NET Core to help me learn and practice building RESTful services. The API is a **Workout Tracker**, and currently includes user management with basic CRUD operations and authentication features.

## Project Overview

This API is designed to manage users and will eventually include features for tracking workouts and other fitness-related data. The current features include:

# Features

## User Management
- 👤 **User registration and authentication**
- 🔐 **JWT-based authentication**
- 👑 **Role-based access control** (Admin and User roles)
- ✏️ **User profile management** (view, update, delete)
- 🔒 **Secure password handling**

## Workout Management
- 📝 **Create, read, update, and delete workouts**
- 🔒 **User-specific workout access control**
- 📊 **View all workouts for logged-in user**
- 🎯 **Individual workout details retrieval**

## Exercise Management
- 💪 **Comprehensive exercise library**
- ➕ **Add new exercises to the system**
- 🔄 **Update existing exercises**
- ❌ **Remove exercises from the system**
- 🔍 **Search and retrieve exercise details**

## Equipment Management
- 🏋️ **Equipment library maintenance**
- ➕ **Add new equipment**
- ✏️ **Update equipment details**
- ❌ **Remove equipment**
- 📃 **List all available equipment**

## Workout-Exercise Relationship
- 📋 **Add exercises to workouts**
- 🔄 **Update exercise details within workouts**
- ❌ **Remove exercises from workouts**
- 📊 **View all exercises in a specific workout**
- 💪 **Track sets, reps, and weights for each exercise in a workout**

## Exercise-Equipment Relationship
- 🔗 **Link exercises with required equipment**
- 🎯 **Map multiple pieces of equipment to exercises**
- ❌ **Remove equipment associations**
- 📃 **View equipment needed for specific exercises**

## Security Features
- 🔐 **Token-based authentication** for all protected endpoints
- 👀 **User data isolation** (users can only access their own workouts)
- ✅ **Input validation and sanitization**
- ❌ **Error handling** and appropriate status codes
- 🛡️ **Protection against unauthorized access**

## API Features
- 🔄 **RESTful API design**
- 📝 **Comprehensive CRUD operations**
- 🎯 **Specific endpoint routing**
- 📊 **Proper HTTP status code implementation**
- ✅ **Request validation**
- 🔍 **Detailed error messages**


### Future Features
- **Integration with Third-Party Services**: Integration with other fitness apps or services for enhanced functionality.
- **Frontend Client**: Building a frontend client using React/Next.js or similar technology for a user-friendly interface.
- **Additional Security for Equipment and Exercise Controllers**: Implementing security features to restrict access and enhance protection.

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
