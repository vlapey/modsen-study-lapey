This ASP.NET project was written for Modsen as test task using following: 

Technologies and Features:
1. .NET 6
2. Entity Framework, 
3. Migrations, 
4. Docker, 
5. EF Fluent API, 
6. Fluent Validation, 
7. AutoMapper, 
8. JSON Web Token (JWT), 
9. Extensions,
10. Swagger,
11. Git.

Patterns:
1. Onion Acrhitecture, 
2. Generic Repository.

This application can be hosted by IIS on 44365 port and by WebApi on 7034 port.
Database is laying in docker as well as the whole application so the docker-compose file is nedded to be up.
When application starts it autoupdates database from migrations so other programmers can just run the app and database will be created.
Application has 2 versions of Architecture. One architecture is on main branch and another in develop_with_app_builder.

Main:![image](https://user-images.githubusercontent.com/58337766/232921870-f3e3bfa3-fbf7-4aa1-9a06-569f1e891ee3.png)
develop_with_app_builder: ![image](https://user-images.githubusercontent.com/58337766/232921447-7831a526-5527-4ce1-9779-95c5a794aa10.png)


The main goal was to try out to put persistence and UI (Web API) on the same level. 
It's hard to say if it's a good kind of practice so the first variant has been developed further.

Because of JWT implementation we have to get JWT token first by registration or login, which cridentials are stored in Database.
JWT was implemented by symmetric key that is stored in appsettings.json (NOT FOR THE PRODUCTION‼).
On production stage this key is normally stored in Azure Key Vault or another simillar cloud technologies.

Application can be tested in Postman with such http requests like: https://localhost:7034/Book/GetAllBooks

If we want to access program from docker, we use localhost:5000.

Database connection - Username: admin Password: 123
