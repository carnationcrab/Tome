### **Tome API & Database Documentation**
> **Base URL:** `http://localhost:5237/api`

---

## **1 Authentication**
### **Register a User**
**Endpoint:** `POST /auth/register`  
Registers a new user.

#### **Request Body:**
```json
{
  "username": "testuser",
  "email": "test@example.com",
  "password": "SecurePass123!"
}
```
#### **Response (201 Created)**
```json
{
  "message": "User registered successfully."
}
```

---

### **Login a User**
**Endpoint:** `POST /auth/login`  
Authenticates a user and returns a JWT token.

#### **Request Body:**
```json
{
  "username": "testuser",
  "password": "SecurePass123!"
}
```
#### **Response (200 OK)**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR..."
}
```

---

## **2? Universes**
### **Get All Universes**
**Endpoint:** `GET /universes`  
Requires authentication.

#### **Headers:**
```json
{
  "Authorization": "Bearer <token>"
}
```
#### **Response (200 OK)**
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "Fantasy World",
    "description": "A medieval fantasy setting."
  }
]
```

---

### **Create a Universe**
**Endpoint:** `POST /universes`  
Requires authentication.

#### **Request Body:**
```json
{
  "name": "Sci-Fi Universe",
  "description": "A futuristic space opera."
}
```
#### **Response (201 Created)**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440001",
  "name": "Sci-Fi Universe",
  "description": "A futuristic space opera."
}
```

---

### **Get Universe by ID**
**Endpoint:** `GET /universes/{id}`  
Requires authentication.

#### **Response (200 OK)**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "Fantasy World",
  "description": "A medieval fantasy setting."
}
```

---

### **Update a Universe**
**Endpoint:** `PUT /universes/{id}`  
Requires authentication.

#### **Request Body:**
```json
{
  "name": "Updated Universe Name",
  "description": "Updated description."
}
```
#### **Response (200 OK)**
```json
{
  "message": "Universe updated successfully."
}
```

---

### **Delete a Universe**
**Endpoint:** `DELETE /universes/{id}`  
Requires authentication.

#### **Response (204 No Content)**

---

## **3? Characters**
### **Get All Characters in a Universe**
**Endpoint:** `GET /universes/{universeId}/characters`  
Requires authentication.

#### **Response (200 OK)**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Elias Mars",
    "description": "A wanderer from Mars."
  }
]
```

---

### **Create a Character**
**Endpoint:** `POST /universes/{universeId}/characters`  
Requires authentication.

#### **Request Body:**
```json
{
  "name": "Elias Mars",
  "description": "A wanderer from Mars.",
  "characterTypeId": "b2d1d43c-5f0b-4e2c-b3e2-826c4d7337a3"
}
```
#### **Response (201 Created)**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Elias Mars",
  "description": "A wanderer from Mars."
}
```

---

### **Update a Character**
**Endpoint:** `PUT /universes/{universeId}/characters/{characterId}`  
Requires authentication.

#### **Request Body:**
```json
{
  "name": "Elias Mars",
  "description": "Updated character description."
}
```
#### **Response (200 OK)**
```json
{
  "message": "Character updated successfully."
}
```

---

### **Delete a Character**
**Endpoint:** `DELETE /universes/{universeId}/characters/{characterId}`  
Requires authentication.

#### **Response (204 No Content)**

---

## **4? Character Types**
### **Get All Character Types**
**Endpoint:** `GET /universes/{universeId}/characterTypes`  
Requires authentication.

#### **Response (200 OK)**
```json
[
  {
    "id": "b2d1d43c-5f0b-4e2c-b3e2-826c4d7337a3",
    "name": "Mage"
  }
]
```

---

### **Create a Character Type**
**Endpoint:** `POST /universes/{universeId}/characterTypes`  
Requires authentication.

#### **Request Body:**
```json
{
  "name": "Mage",
  "fields": [
    { "name": "Mana", "type": "integer" }
  ]
}
```
#### **Response (201 Created)**
```json
{
  "id": "b2d1d43c-5f0b-4e2c-b3e2-826c4d7337a3",
  "name": "Mage"
}
```

---

## **5? Database Schema**
```sql
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_name TEXT UNIQUE NOT NULL,
    normalized_user_name TEXT UNIQUE NOT NULL,
    email TEXT UNIQUE NOT NULL,
    normalized_email TEXT UNIQUE NOT NULL,
    email_confirmed BOOLEAN DEFAULT FALSE,
    password_hash TEXT NOT NULL,
    security_stamp TEXT,
    concurrency_stamp TEXT,
    phone_number TEXT,
    phone_number_confirmed BOOLEAN DEFAULT FALSE,
    two_factor_enabled BOOLEAN DEFAULT FALSE,
    lockout_end TIMESTAMP NULL,
    lockout_enabled BOOLEAN DEFAULT FALSE,
    access_failed_count INT DEFAULT 0,
    created_at TIMESTAMP DEFAULT NOW()
);

CREATE TABLE universes (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    description TEXT
);

CREATE TABLE characters (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    universe_id UUID REFERENCES universes(id) ON DELETE CASCADE,
    name TEXT NOT NULL,
    description TEXT
);

CREATE TABLE character_types (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    universe_id UUID REFERENCES universes(id) ON DELETE CASCADE,
    name TEXT NOT NULL
);

CREATE TABLE fields (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    character_type_id UUID REFERENCES character_types(id) ON DELETE CASCADE,
    name TEXT NOT NULL,
    type TEXT NOT NULL
);
```

---

### **Notes **
- Authentication is **JWT-based**.
- Endpoints requiring authentication **must include** the `Authorization: Bearer <token>` header.
- The database uses **UUIDs** for primary keys.
- All table and column names are **lowercased** to avoid PostgreSQL case-sensitivity issues.

---
