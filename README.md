# Setup
1. Create the Book table in the database
```sql
CREATE TABLE Department (
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Name VARCHAR(50),
Phone VARCHAR(20)
);
CREATE TABLE Employee (
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Name VARCHAR(50),
Surname VARCHAR(50),
Phone VARCHAR(20),
CompanyId INT,
DepartmentId INT,
FOREIGN KEY (DepartmentId) REFERENCES Department(Id)
);
CREATE TABLE Passport (
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Type VARCHAR(50),
Number VARCHAR(50),
EmployeeId INT,
FOREIGN KEY (EmployeeId) REFERENCES Employee(Id)
);
```

2. Update connection string in appsettings.json
```javascript
 "ConnectionStrings": {
    "Default": "Your Connection string"
  }
```

