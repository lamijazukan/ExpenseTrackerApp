# ExpenseTrackerApp

A full-stack budget/expense tracking application built with .NET(C#) and React Vite. For detail API documentation access (link for api readme).


### Prerequisites

Make sure the following are installed on your system:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

> Note: This project uses the official PostgreSQL Docker image (version 16) defined in `docker-compose.yml`.


## Setup Instructions

### 1. Clone the repository

```bash
git clone <repository-url>

cd docker/compose
```

### 2. Configure environment variables
Create a .env file in the docker/compose/ folder and add the following:

```bash
POSTGRES_DB=<yourdbname>
POSTGRES_USER=<user>
POSTGRES_PASSWORD=<password>

```
Replace ```<yourdbname>```, ```<user>```, and ```<password>``` with your Postgres database credentials.

### 3. Start/Stop application

```bash
docker-compose up --build
```

- To stop application:
```bash
docker-compose down
```


### 4. Ef core is used to manage the database schema

```bash
   
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        // assure DB exists and schema is up-to-date
        db.Database.Migrate();

        await DbSeeder.SeedAsync(db); 
    }
    
```
This will apply migrations and seed database on project startup.

### 5. Access the application

| Service      | URL                                                              |
| ------------ | ---------------------------------------------------------------- |
| Frontend     | [http://localhost:5173](http://localhost:5173)                   |
| Backend API  | [http://localhost:8080](http://localhost:8080)                   |
| Swagger Docs | [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html) |
