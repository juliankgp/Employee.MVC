
# Employees MVC 
Employees MVC is an application developed using the ASP. Net Core Web App (MVC), the graphic interface was made with bootstrap 5, css and javascript.

## Version
 Versión del framework .NET Core 6 



## ¿How it works?
The application has a main screen which allows entering the ID of an employee, once the ID is entered it consumes a public API that returns a screen with the information of the determined employee and a calculated field that is the annual salary of the employee. In the event that no ID is entered, return a list of all the employees that are registered in a table calculating the annual salary of each of the employees. If an ID is entered that does not exist, the application would return a general error.



## Libraries

- *Newtonsoft.Json*: library used for serialization and deserialization of data.



## Instalation
The application does not require any installation to work, all you have to do is download the repository, and run the project solution (Employee.MVC.sln)



## Unit Tests 

Unit tests were carried out on this project with Xunit, they were carried out in the business layer and in the shared kernel services.

## Methods

| Name| Business Layer | Methods |
| ------ | ------ | ------ |
| Search|GetEmployeesById | POST|
| SearchEmployees |GetEmployees|GET|



## Usage tests
## Main screen

![main_screen](https://user-images.githubusercontent.com/48841736/204196840-391c105d-ae01-4614-bedf-c127596fa7a3.png)

### Return Employee by id


