# Console Inventory Manager

A simple C# console application for managing product inventory with a lightweight text-based user interface.

## Overview

Console Inventory Manager is a small inventory tracking system built in C#. It lets you:
- add products
- delete products
- restock inventory
- record sales
- view all products
- view transaction history
- manage product categories

The project uses a simple terminal UI (TUI-like) approach with console prompts, screen clears, and rendered product lists to simulate a small interactive dashboard.

## Features

- Product creation and deletion
- Inventory quantity tracking
- Product pricing support
- Category-based product organization
- Sale and restock transaction logic
- Transaction history tracking

## Project Structure

```text
Console-InventoryManager/
├── InventoryManager/
│   ├── Models/
│   │   ├── Enums.cs
│   │   ├── Product.cs
│   │   └── Transaction.cs
│   ├── Services/
│   │   └── InventoryService.cs
│   ├── Program.cs
│   ├── Reanderer.cs
│   ├── InventoryManager.csproj
│   └── ...
├── .gitignore
└── README.md
```

## Tech Stack

- C#
- .NET
- Console Application
- Simple terminal user interface

## Prerequisites
- .NET SDK 

## Getting Started

Clone the repo:

```bash
git clone https://github.com/QusaiCode/Console-InventoryManager.git
cd Console-InventoryManager
```

Run the app:

```bash
dotnet run --project InventoryManager
```

## Available Commands

The app supports commands such as:

- `add` — add a new product
- `products` — display product list
- `restock` — add stock to an item
- `sell` — reduce stock from an item
- `transactions` — view transaction log
- `delete` — remove a product
- `exit` — close the application

## Example Usage

```text
Enter Command: add
Product Name: Wireless Mouse
Product Categories:
 1 ] Electronics
 2 ] Clothing
 3 ] Food
 4 ] Other
Category ID: 1
Product Initial Quantity: 30
Product Price: 19.99
```

Then:

```text
Enter Command: products
```

or:

```text
Enter Command: sell
Product ID: 1
Product Quantity: 2
```

## Core Logic

The app is organized into a few main parts:

- `Product` — product data model
- `Transaction` — sales/restock record model
- `ProductCategory` and `TransactionType` — enums for product and transaction classification
- `InventoryService` — handles business logic
- `Program` — manages the console flow and user interaction
- `Renderer` — displays inventory and transaction outputs in the terminal

## Notes

This project is a beginner-friendly example of:
- C# console app development
- business logic separation
- model/service architecture
- simple text-based UI design
- transaction-based inventory management

## Contributing

Feel free to fork the project, improve the interface, add validation, or expand inventory features.
