🛒 Online Shopping System (E-Commerce Web Application)
Overview

The Online Shopping System is a full-stack web application developed using ASP.NET Core MVC, Entity Framework Core (Database First Approach), Repository Pattern, and SQL Server.

This project simulates a real-world e-commerce platform where users can browse products, manage cart items, and place orders, while administrators manage products and orders through a secure dashboard.

The system is built using layered architecture and repository pattern, ensuring clean separation of concerns and maintainability.

🎯 Project Objective

The objective of this project is to design and implement a scalable e-commerce system that demonstrates:

Real-world online shopping workflow
Database-first development approach
Repository pattern for data access abstraction
Role-based system architecture
Product and order management system
👤 User Roles & Features
🛍️ Customer (User)
Register and login securely
Browse products by category
View product details
Add products to cart
Manage cart (update/remove items)
Place orders
View order history
🛠️ Administrator
Secure admin login
Add, update, and delete products
Manage product categories
View and manage customer orders
Monitor inventory
Manage system data
🧱 Key Features
🔐 Authentication & Authorization
Secure login system using ASP.NET Identity
Role-based access (Admin / Customer)
Protected admin dashboard
🛒 Shopping Cart System
Add/remove products to cart
Quantity management
Real-time cart updates
📦 Order Management
Order placement system
Order tracking
Admin order management
🗂️ Product Management
Category-wise product listing
Admin-controlled product CRUD operations
Product image handling
🏗️ Architecture
Repository Pattern implementation
Service-based separation of logic
Clean and scalable structure
🧰 Technology Stack
Backend
ASP.NET Core MVC
C#
Entity Framework Core (DB First Approach)
LINQ
Frontend
HTML5
CSS3
Bootstrap
JavaScript
Database
SQL Server (Database First Approach)
Architecture
Repository Pattern
MVC Architecture
Tools
Visual Studio
SQL Server Management Studio (SSMS)
Git & GitHub
🗄️ Database Approach (DB First)

This project follows the Database First Approach:

Database is designed first in SQL Server
Entity Framework Core scaffolds models from database
Models are auto-generated from existing tables
Repository layer handles all database operations
🏗️ System Architecture

The project follows a layered architecture:

Models: Auto-generated EF Core entities
Repositories: Data access logic abstraction
Controllers: Application request handling
Views: UI layer for user interaction

This ensures:

Clean code structure
Easy maintenance
Scalability
🛒 Core Modules
User Management
Product Management
Category Management
Shopping Cart Module
Order Processing Module
Admin Dashboard
📸 Screenshots

Add your screenshots here:

## Screenshots

### Home Page
![Home](Screenshots/home.png)

### Product Listing
![Products](Screenshots/products.png)

### Cart Page
![Cart](Screenshots/cart.png)

### Admin Dashboard
![Admin](Screenshots/admin-dashboard.png)