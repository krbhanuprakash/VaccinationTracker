# Vaccination Tracker

## Overview
Vaccination Tracker is a web application designed to manage vaccination schedules, notifications, and user roles. It provides features for both administrators and regular users to streamline the vaccination process.

## Features
- **Admin Features:**
  - Manage vaccination schedules for all users.
  - Dashboard for Visualize user-wise vaccination schedules     and  statuses with interactive charts.
  - Assign roles to users.
- **User Features:**
  - View personal vaccination schedules.
  - View and manage notifications.
  - Receive notifications about upcoming vaccinations.
- **Dynamic UI:**
  - Navbar and pages adapt based on user roles (Admin or Regular User).

## Technologies Used
- **Backend:** ASP.NET Core 8.0
- **Frontend:** Razor Pages, HTML, CSS, JavaScript
- **Database:** Entity Framework Core with SQL Server
- **Authentication:** ASP.NET Identity

## Project Structure
- **Controllers:** Handles the logic for various features (e.g., `VaccinationSchedulesController`, `NotificationsController`).
- **Models:** Defines the data structure (e.g., `VaccinationSchedule`, `Notification`).
- **Views:** Contains Razor Pages for the UI (e.g., `Edit.cshtml`, `Create.cshtml`).
- **Data:** Manages database context and migrations.

## Setup Instructions
1. Clone the repository.
2. Navigate to the project directory:
   ```bash
   cd VaccinationTracker
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Apply migrations to set up the database:
   ```bash
   dotnet ef database update
   ```
5. Run the application:
   ```bash
   dotnet run
   ```
6. Open the application in your browser at `http://localhost:5000`.

## Contribution Guidelines
1. Fork the repository.
2. Create a new branch for your feature or bug fix:
   ```bash
   git checkout -b feature-name
   ```
3. Commit your changes and push to your fork.
4. Submit a pull request with a detailed description of your changes.

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Contact
For any questions or feedback, please contact the project maintainer at [email@example.com].

## Problem Addressed
Vaccination Tracker addresses the challenge of managing vaccination schedules and notifications for both administrators and regular users. It ensures that users are informed about their vaccination schedules and provides administrators with tools to manage and oversee the vaccination process efficiently.

## How It Works
The application is built using ASP.NET Core and Razor Pages. It features role-based access control, allowing administrators to manage schedules and users, while regular users can view their schedules and receive notifications. The dynamic UI adapts based on the user's role, providing a tailored experience. The backend uses Entity Framework Core for database interactions, and ASP.NET Identity handles authentication and authorization.

## Use of AI Tools in Development
AI tools were instrumental in the development process by:
- Assisting in generating boilerplate code for controllers, models, and views.
- Providing suggestions for optimizing code structure and improving readability.
- Automating repetitive tasks, such as creating migrations and setting up role-based access control.
- Offering insights and recommendations for implementing best practices in ASP.NET Core development.
