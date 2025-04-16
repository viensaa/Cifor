# Cifor Simple API
This is an API that functions for simple Employee management. 
This API is created to meet the needs of the Technical test from Indocyber and Cifor.
This API is useful for employee management equipped with 4 features, namely:
1. Create, Update, Delete, Employee data
2. Retrieve all Employee data
3. Retrieve Employee data based on ID
4. Retrieve Employee data based on Name and Department

# Information
this is table Design
```bash
1.	Employee
| Coloumn   | Type Data |
| --------  | --------- |
|EmployeeID | string    |
|Name       | string    |
|Department |  string   |
|Email      | string    |
```


# You Need To Install First From Nugget Package
```bash
1.	microsorft.entityFrameworkCore.SqlServer
2.	microsorft.entityFrameworkCore.Tools
3.	microsorft.entityFrameworkCore.Design
4.	Install AutoMapper
5.	Install NewtonSoft.Json
```

# How To Run
### Migration Database
In Package Manager Console make sure to migration all service database
```bash
Update-Database
```
### Running Project

```bash
make EmplooyeeWebAPI as start up project and running
Solution -> EmplooyeeWebAPI ->  right klick on wwwroot -> View In browesetr
```


