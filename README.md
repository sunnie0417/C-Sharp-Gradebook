# Grade Book

## Objective
Create a database-driven Grade Book API that will allow for CRUD operations on multiple database tables using .NET and the Entity Framework.  

The Grade Book API have the following endpoints to perform the following actions:  

Students  
Endpoint	Action  
GET: /api/students	returns all students  
GET: /api/students/{id}	returns a single student  
GET: /api/students/{id}/grades	returns all grades for a single student  
POST: /api/students	creates a new student  
PUT: /api/students/{id}	updates a student  
DELETE: /api/students/{id}	deletes a student  

Assignments  
Endpoint	Action  
GET: /api/assignments	returns all assignments  
GET: /api/assignments/{id}	returns a single assignment  
GET: /api/assignments/{id}/grades	returns all grades for a single assignment  
POST: /api/assignments	creates a new assignment  
PUT: /api/assignments/{id}	updates an assignment  
DELETE: /api/assignments/{id}	deletes an assignment  

Grades  
Endpoint	Action  
GET: /api/grades	returns all assignments  
GET: /api/grades/{id}	returns a single grades  
POST: /api/grades	creates a new grade  
PUT: /api/grades/{id}	updates an grades  
DELETE: /api/grades/{id}	deletes an grades  
