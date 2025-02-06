# Tome API

This is the API for Tome, a full-stack web app built with .NET Core, Knockout.js, and PostgreSQL. The API handles CRUD operations for universes, character types, and events, with flexible field structures for character sheets.

---

## 🛠 Setup
### 1. Clone the Repo
```sh
git clone <repo-url>
cd Tome
```

### 2. Run with Docker
If you're using Docker for PostgreSQL, make sure it's running:
```sh
docker-compose up -d
```

Otherwise, install PostgreSQL locally and create the database manually.

### 3. Install Dependencies
```sh
dotnet restore
```

### 4. Run the API
```sh
dotnet run
```

## 📌 Endpoints

### Universes
- `GET /api/universes` - List universes
- `POST /api/universes` - Create a new universe
- `GET /api/universes/{id}` - Get a universe by ID
- `PUT /api/universes/{id}` - Update a universe
- `DELETE /api/universes/{id}` - Delete a universe

### Character Types
- `GET /api/universes/{universeId}/charactertypes` - List character types in a universe
- `POST /api/universes/{universeId}/charactertypes` - Create a character type
- `GET /api/universes/{universeId}/charactertypes/{id}` - Get a character type by ID
- `PUT /api/universes/{universeId}/charactertypes/{id}` - Update a character type
- `DELETE /api/universes/{universeId}/charactertypes/{id}` - Delete a character type

### Fields
- `GET /api/charactertypes/{characterTypeId}/fields` - List fields for a character type
- `POST /api/charactertypes/{characterTypeId}/fields` - Add a field to a character type
- `PUT /api/charactertypes/{characterTypeId}/fields/{id}` - Update a field
- `DELETE /api/charactertypes/{characterTypeId}/fields/{id}` - Delete a field

---

## ❌ Troubleshooting
### Common Issues & Fixes
**Database connection errors?**
- Check if PostgreSQL is running: `docker ps`
- Verify connection string in `.env`
  
---

### Built With
- **.NET Core 9**
- **Entity Framework Core**
- **PostgreSQL**
- **Knockout.js**

