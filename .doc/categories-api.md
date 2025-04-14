### categories

#### GET /api/categories
- Description: Retrieve a list of all categories
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "title desc" or "title asc")
- Response: 
  ```json
  {
    "data": [
      {
        "id": "string",
        "title": "string",
    ],
    "totalItems": "integer",
    "currentPage": "integer",
    "totalPages": "integer"
  }
  ```
- Filtering example:
  ```json
  GET /api/categories?title=men's clothing
  ```
  To filter partial matches for string fields, use an asterisk (*) before, after or between the value.
  Example:
  ```json
  GET /api/categories?title=Fjallraven*
  GET /api/categories?title=*clothing
  GET /api/categories?title=*cl*thing*
  ```


#### POST /api/categories
- Description: Add a new category
- Request Body:
  ```json
  {
    "title": "string"
  }
  ```
- Response: 
  ```json
  {
    "id": "string",
    "title": "string"
  }
  ```

#### GET /api/categories/{id}
- Description: Retrieve a specific category by ID
- Path Parameters:
  - `id`: category ID
- Response: 
  ```json
  {
    "id": "string",
    "title": "string"
  }
  ```

#### PUT /api/categories/{id}
- Description: Update a specific category
- Path Parameters:
  - `id`: category ID
- Request Body:
  ```json
  {
    "title": "string"
  }
  ```
- Response: 
  ```json
  {
    "id": "string",
    "title": "string"
  }
  ```

#### DELETE /api/categories/{id}
- Description: Delete a specific category
- Path Parameters:
  - `id`: category ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```
