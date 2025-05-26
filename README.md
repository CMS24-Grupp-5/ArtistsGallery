# 🎨 Artists Gallery Microservice

## 📡 API Endpoints


<pre>
GET /api/artists
GET /api/artists/{id}
POST /api/artists
PUT /api/artists/{id}
DELETE /api/artists/{id}
</pre>

````
### GET /api/artists  
[
  {
    "id": "string",
    "name": "string",
    "description": "string",
    "urlImage": "string"
  }
]
````

---

### GET /api/artists/{id}

**Response:** 200 OK

```json
{
  "id": "string",
  "name": "string",
  "description": "string",
  "urlImage": "string"
}
```

---

### POST /api/artists

**Request:**

```json
{
  "name": "string",
  "description": "string",
  "urlImage": "string"
}
```

**Response:** 201 Created

```json
{
  "id": "generated-string",
  "name": "string",
  "description": "string",
  "urlImage": "string"
}
```

---

### PUT /api/artists/{id}

**Request:**

```json
{
  "name": "string",
  "description": "string",
  "urlImage": "string"
}
```

**Response:** 200 OK (updated artist object) or 404 Not Found if artist does not exist

---

### DELETE /api/artists/{id}

**Response:** 204 No Content or 404 Not Found if artist does not exist

---

## 🧪 Testing

To run unit tests:

```bash
dotnet test
```

```
