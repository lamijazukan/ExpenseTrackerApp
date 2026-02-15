# ExpenseTracker API Documentation


```bash
http://localhost:8080/swagger/index.html
```

## 🔐 Authentication

All endpoints (except register & login) require a JWT Bearer token.

After login, copy the token and authorize in Swagger using:

```bash
Authorization: Bearer <your_token>
```

### Demo credentials

Use this account to test the API with pre-seeded data:

```bash
Email: demo@demo.com
Password: Demo123!
```

## Auth Endpoints

### POST ```api/v1//auth/register```
Registers a new user account.

#### Description
Creates a new user and returns a JWT token.

#### Request body
```bash
{
  "email": "user@email.com",
  "password": "Password123!",
  "firstName": "John",
  "lastName": "Doe"
}
```

#### Response
- 200 OK → JWT token returned

### POST ```api/v1/auth/login```
Logs in an existing user.

#### Description
Validates credentials and returns JWT token.

#### Request body
```bash
{
  "email": "demo@demo.com",
  "password": "Demo123!"
}
```

#### Response
- 200 OK → JWT token

## 🗂️ Categories

Categories can be hierarchical (parent/child). If parentCategoryId is null, than category is parent. Allows user to create, update and delete categories.

### GET ```api/v1/categories```

### POST ```api/v1/categories```
```bash
{
  "name": "Food",
  "parentCategoryId": null
}
```

### GET ```api/v1/categories/{categoryId}```

### PATCH ```api/v1/categories/{categoryId}```

### DELETE ```api/v1/categories/{categoryId}```

### GET ```api/v1/categories/tree```

- Returns hierarchical category tree.

- Used by frontend for nested category UI.
  

## 💰 Budgets

Represents monthly budgets per category. User can add budget per every category parent or child.

### GET ```api/v1/budgets```

### POST ```api/v1/budgets```

#### Example
```bash
{
  "categoryId": "guid",
  "amount": 500,
  "startDate": "2025-01-01",
  "endDate": "2025-01-31"
}
```

### GET ```api/v1/budgets/{budgetId}```

### PATCH ```api/v1/budgets/{budgetId}```

### DELETE ```api/v1/budgets/{budgetId}```


## 💳 Transactions

Transactions represents total amount spent somewhere. One or more expenses make one transaction.

### GET ```api/v1/transactions```

### POST ```api/v1/transactions```

### GET ```api/v1/transactions/{transactionId}```

### PATCH ```api/v1/transactions/{transactionId}```

### DELETE ```api/v1/transactions/{transactionId}```


## 💸 Expenses

Represents user expenses. One expense is individual record from transaction.

### GET ```api/v1/expenses```

### POST ```api/v1/expenses```
```bash
{
  "amount": 50,
  "categoryId": "guid",
  "date": "2025-01-10",
  "note": "Groceries"
}
```

### GET ```api/v1/expenses/{expenseId}```

### PATCH ```api/v1/expenses/{expenseId}```

### DELETE ```api/v1/expenses/{expenseId}```


## 📊 Statistics

Used to represent on dashboard some specific statistics.

### GET ```api/v1/statistics/budget/{budgetId}```

Tracks budget status and returns budget usage statistics:
- total spent
- remaining amount
- percentage used
- warning/limit flags

### GET ```api/v1/statistics/monthly-savings```

Returns a monthly savings overview based on the user’s defined budgets and spending.
This endpoint calculates how much money the user has saved by comparing total expenses against the budgets set for the month. It highlights whether the user stayed within budget and aggregates the remaining (unused) amounts from all budgets to present the total monthly savings.


### GET ```api/v1/statistics/category-expenses```

Returns expenses grouped by category.



## 👤 User Profile

### GET ```api/v1/userprofile```

Returns logged user profile.

### PATCH ```api/v1/userprofile```

Updates user profile info.

#### Example:
```bash
{
  "firstName": "John",
  "lastName": "Doe"
}
```


## 👥 Users (Later for Admin / internal usage)

### GET ```api/v1/users```

### GET ```api/v1/users/{userId}```

### PATCH ```api/v1/users/{userId}```


## ❤️ Health Check

### GET ```/health```

Simple endpoint to check if API is running.

#### Returns:
- Healthy
- Statistics
- Provides analytical insights based on user data.

> ### Notes
- All endpoints require authentication unless stated otherwise.
- Database is automatically seeded on first run.
- Demo user contains sample budgets, categories, transactions and expenses for testing.
