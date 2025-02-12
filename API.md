# **Tome API Documentation**

## **Overview**
The Tome API provides endpoints for managing **characters, items, modifiers, and their relationships**. The API supports CRUD operations for all entities and allows for **assigning modifiers to characters, items, fields, and character types**.

---

## **Authentication**
All endpoints require a **Bearer Token** for authentication. Include the token in the `Authorization` header:
```http
Authorization: Bearer YOUR_ACCESS_TOKEN
```

---

# **Endpoints**
## **Items API**
### **1. Create an Item**
**`POST /api/items`**

#### **Request Body**
```json
{
  "name": "Sword of Eternal Flames",
  "description": "A flaming sword that might burn out but probably not.",
  "rarity": "Legendary"
}
```
#### **Response (201 Created)**
```json
{
  "id": "41a032bb-f922-4c45-a2f9-f62906015fa5",
  "name": "Sword of Eternal Flames",
  "description": "A flaming sword that might burn out but probably not.",
  "rarity": "Legendary"
}
```

---
### **2. Get Item by ID**
**`GET /api/items/{id}`**
#### **Response (200 OK)**
```json
{
  "id": "41a032bb-f922-4c45-a2f9-f62906015fa5",
  "name": "Sword of Eternal Flames",
  "description": "A flaming sword that might burn out but probably not.",
  "rarity": "Legendary",
  "modifiers": [
    {
      "modifierId": "8b1f3caa-2f99-4b62-8c0e-66e23499a5e6",
      "name": "Flame Damage",
      "value": 5.0,
      "operation": "add"
    }
  ]
}
```

---
### **3. Update an Item**
**`PUT /api/items/{id}`**

#### **Request Body**
```json
{
  "name": "Blade of Inferno",
  "description": "A legendary sword engulfed in eternal flames.",
  "rarity": "Mythic",
  "modifiers": [
    {
      "modifierId": "8b1f3caa-2f99-4b62-8c0e-66e23499a5e6"
    }
  ]
}
```

#### **Response (204 No Content)**

---
### **4. Delete an Item**
**`DELETE /api/items/{id}`**
#### **Response (204 No Content)**

---

## **Modifiers API**
### **1. Create a Modifier**
**`POST /api/modifiers`**

#### **Request Body**
```json
{
  "name": "Flame Damage",
  "value": 5.0,
  "operation": "add",
  "condition": "if enemy is frozen",
  "duration": 10
}
```
#### **Response (201 Created)**
```json
{
  "id": "8b1f3caa-2f99-4b62-8c0e-66e23499a5e6",
  "name": "Flame Damage",
  "value": 5.0,
  "operation": "add",
  "condition": "if enemy is frozen",
  "duration": 10
}
```

---
### **2. Get Modifier by ID**
**`GET /api/modifiers/{id}`**
#### **Response (200 OK)**
```json
{
  "id": "8b1f3caa-2f99-4b62-8c0e-66e23499a5e6",
  "name": "Flame Damage",
  "value": 5.0,
  "operation": "add",
  "condition": "if enemy is frozen",
  "duration": 10
}
```

---
### **3. Update a Modifier**
**`PUT /api/modifiers/{id}`**

#### **Request Body**
```json
{
  "name": "Burning Effect",
  "value": 3.0,
  "operation": "multiply",
  "duration": 15
}
```
#### **Response (204 No Content)**

---
### **4. Delete a Modifier**
**`DELETE /api/modifiers/{id}`**
#### **Response (204 No Content)**

---

## **Modifier Assignments API**
### **1. Assign a Modifier to an Entity**
**`POST /api/modifierassignments`**

#### **Request Body**
```json
{
  "modifierId": "8b1f3caa-2f99-4b62-8c0e-66e23499a5e6",
  "characterId": "97fdc3aa-4a12-48a5-b19e-93e234bc5e4f"
}
```
#### **Response (201 Created)**
```json
{
  "id": "aadf3bba-2c34-4a12-87f2-6e89b4cde3aa",
  "modifierId": "8b1f3caa-2f99-4b62-8c0e-66e23499a5e6",
  "characterId": "97fdc3aa-4a12-48a5-b19e-93e234bc5e4f"
}
```

---
### **2. Remove a Modifier from an Entity**
**`DELETE /api/modifierassignments/{id}`**
#### **Response (204 No Content)**

---

## **Testing and Validation**
- Successfully tested item creation, updates, and retrieval.
- Verified modifier assignment logic across multiple entities.
- Ensured proper database relationships with `modifier_assignments`.

---

## **Next Steps**
- Expand items to support **inventory systems and equippable gear**.
- Implement **stacking rules for modifiers**.
- Further integration with **character stats calculations**.

---

## **Final Thoughts**
This API provides a **robust system for character customization** via **modifiers, items, and dynamic stat changes**. Future updates will refine how modifiers are applied and enhance gameplay interactions.

**Happy coding!**

