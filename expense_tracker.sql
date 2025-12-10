/* EXPENSE TRACKER DATABASE SCHEMA WITH SAMPLE DATA - PostgreSQL */

-- Table: Users
CREATE TABLE Users (
    UserID UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Username VARCHAR(15) NOT NULL UNIQUE,
    Email VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT NOW(),
    Preferences JSONB,
);

-- Table: Store
CREATE TABLE Store (
    StoreID SERIAL PRIMARY KEY,
    StoreName VARCHAR(20) NOT NULL,
    Location VARCHAR(100)
);

-- Table: Category
CREATE TABLE Category (
    CategoryID SERIAL PRIMARY KEY,
    UserID UUID NOT NULL,
    Name VARCHAR(50) NOT NULL,
    ParentCategoryID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (ParentCategoryID) REFERENCES Category(CategoryID) ON DELETE SET NULL
);

-- Table: Budget
CREATE TABLE Budget (
    BudgetID SERIAL PRIMARY KEY,
    UserID UUID NOT NULL,
    CategoryID INT NOT NULL,
    PeriodType VARCHAR(20) NOT NULL CHECK (PeriodType IN ('Daily', 'Weekly', 'Monthly', 'Yearly')),
    Amount DECIMAL(12, 2) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID) ON DELETE CASCADE
);

-- Table: Receipt
CREATE TABLE Receipt (
    ReceiptID SERIAL PRIMARY KEY,
    UserID UUID NOT NULL,
    ImageUrl VARCHAR(500),
    UploadedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ProcessingStatus VARCHAR(20) DEFAULT 'Pending' CHECK (ProcessingStatus IN ('Pending', 'Processing', 'Completed', 'Failed')),
    ReceiptExtractedData JSONB NOT NULL,
    TotalAmount DECIMAL(12, 2) NOT NULL,
    StoreID INT,
    PurchaseDate DATE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (StoreID) REFERENCES Store(StoreID) ON DELETE SET NULL
);

-- Table: Product
CREATE TABLE Product (
    ProductID SERIAL PRIMARY KEY,
    ProductName VARCHAR(20) NOT NULL,
    Brand VARCHAR(25),
    CategoryID INT,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID) ON DELETE SET NULL
);

-- Table: ProductPrice
CREATE TABLE ProductPrice (
    PriceID SERIAL PRIMARY KEY,
    ProductID INT NOT NULL,
    StoreID INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Source VARCHAR(50),
    PriceDate DATE NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID) ON DELETE CASCADE,
    FOREIGN KEY (StoreID) REFERENCES Store(StoreID) ON DELETE CASCADE
);

-- Table: ReceiptItem
CREATE TABLE ReceiptItem (
    ReceiptItemID SERIAL PRIMARY KEY,
    ReceiptID INT NOT NULL,
    ProductID INT,
    ItemName VARCHAR(50) NOT NULL,
    Quantity INT DEFAULT 1,
    Price DECIMAL(10, 2) NOT NULL,
    TotalPrice DECIMAL(10, 2) NOT NULL,
    CategoryID INT,
    FOREIGN KEY (ReceiptID) REFERENCES Receipt(ReceiptID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID) ON DELETE SET NULL,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID) ON DELETE SET NULL
);

-- Table: Expense
CREATE TABLE Expense (
    ExpenseID SERIAL PRIMARY KEY,
    UserID UUID NOT NULL,
    CategoryID INT NOT NULL,
    ReceiptID INT,
    Amount DECIMAL(12, 2) NOT NULL,
    ExpenseDate DATE NOT NULL,
    Description TEXT,
    PaymentMethod VARCHAR(50),
    StoreID INT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    IsReviewed BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
    FOREIGN KEY (ReceiptID) REFERENCES Receipt(ReceiptID) ON DELETE SET NULL,
    FOREIGN KEY (StoreID) REFERENCES Store(StoreID) ON DELETE SET NULL
);

-- Table: Notification
CREATE TABLE Notification (
    NotificationID SERIAL PRIMARY KEY,
    UserID UUID NOT NULL,
    NotificationType VARCHAR(50) NOT NULL CHECK (NotificationType IN ('Budget Alert', 'Receipt Processed', 'Price Alert', 'System', 'Reminder')),
    Title VARCHAR(50) NOT NULL,
    Message TEXT NOT NULL,
    Severity VARCHAR(20) DEFAULT 'Info' CHECK (Severity IN ('Info', 'Warning', 'Error', 'Success')),
    IsRead BOOLEAN DEFAULT FALSE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    EntityReferenceID INT,
    EntityReferenceType VARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);

-- Sample Data Insertion

INSERT INTO Users (UserID, Username, Email, Password, Preferences) VALUES
('550e8400-e29b-41d4-a716-446655440001','john_doe', 'john.doe@email.com', 'StrongPassword123##@', '{"language": "bs", "currency": "BAM"}'),
('550e8400-e29b-41d4-a716-446655440002','jane_smith', 'jane.smith@email.com', 'Password123!=#', '{"language": "en", "currency": "USD"}'),
('550e8400-e29b-41d4-a716-446655440003', 'mike_wilson', 'mike.wilson@email.com', '123myPassword$%#', '{"language": "bs", "currency": "BAM"}');

-- Insert Stores
INSERT INTO Store (StoreName, Location) VALUES
('Bingo', 'BCC Halovići'),
('Amko', 'Titova ulica'),
('LC Waikiki', 'SCC Sarajevo');

-- Insert Categories
INSERT INTO Category (UserID, Name, ParentCategoryID) VALUES
('550e8400-e29b-41d4-a716-446655440001', 'Food & Dining', NULL),
('550e8400-e29b-41d4-a716-446655440001', 'Groceries', 1),
('550e8400-e29b-41d4-a716-446655440001', 'Clothing', NULL);

-- Insert Budgets
INSERT INTO Budget (UserID, CategoryID, PeriodType, Amount, StartDate, EndDate, IsActive) VALUES
('550e8400-e29b-41d4-a716-446655440001', 1, 'Monthly', 600.00, '2025-11-01', '2025-11-30', TRUE),
('550e8400-e29b-41d4-a716-446655440001', 2, 'Monthly', 400.00, '2025-11-01', '2025-11-30', TRUE),
('550e8400-e29b-41d4-a716-446655440001', 3, 'Monthly', 200.00, '2025-11-01', '2025-11-30', TRUE);

-- Insert Products
INSERT INTO Product (ProductName, Brand, CategoryID) VALUES
('Milk', 'Milkos', 2),
('Whole Wheat Bread', 'Zlatno Zrno', 2),
('Dress', 'Wikiki', NULL);

-- Insert Receipts
INSERT INTO Receipt (UserID, ImageUrl, ProcessingStatus, TotalAmount, StoreID, PurchaseDate, ReceiptExtractedData) VALUES
('550e8400-e29b-41d4-a716-446655440001', '/uploads/receipt_001.jpg', 'Completed', 45.67, 1, '2025-11-15', '{"items": 5, "tax": 0.17}'),
('550e8400-e29b-41d4-a716-446655440001', '/uploads/receipt_002.jpg', 'Completed', 128.99, 2, '2025-11-14', '{"items": 3, "tax": 0.17}'),
('550e8400-e29b-41d4-a716-446655440002', '/uploads/receipt_003.jpg', 'Processing', 20.21, 3, '2025-11-16', '{"items": 3, "tax": 0.17}');

-- Insert ProductPrice
INSERT INTO ProductPrice (ProductID, StoreID, Price, Source, PriceDate) VALUES
(1, 1, 5.99, 'Receipt', '2025-11-15'),
(2, 1, 4.99, 'Receipt', '2025-11-15'),
(3, 2, 60.00, 'Manual', '2025-11-16');

-- Insert ReceiptItems
INSERT INTO ReceiptItem (ReceiptID, ProductID, ItemName, Quantity, Price, TotalPrice, CategoryID) VALUES
(1, 1, 'Milk', 2, 5.99, 11.98, 2),
(1, 2, 'Whole Wheat Bread', 1, 4.99, 4.99, 2),
(2, 3, 'Coffee', 1, 12.99, 12.99, 1);

-- Insert Expenses
INSERT INTO Expense (UserID, CategoryID, ReceiptID, Amount, ExpenseDate, Description, PaymentMethod, StoreID, IsReviewed) VALUES
('550e8400-e29b-41d4-a716-446655440001', 2, 1, 45.67, '2025-11-15', 'Weekly grocery shopping', 'Credit Card', 1, TRUE),
('550e8400-e29b-41d4-a716-446655440001', 1, 2, 25.98, '2025-11-14', 'Coffee beans', 'Debit Card', 2, TRUE),
('550e8400-e29b-41d4-a716-446655440001', 3, NULL, 45.00, '2025-11-12', 'Gas fill-up', 'Cash', 3, FALSE);

-- Insert Notifications
INSERT INTO Notification (UserID, NotificationType, Title, Message, Severity, IsRead, EntityReferenceID, EntityReferenceType) VALUES
('550e8400-e29b-41d4-a716-446655440001', 'Budget Alert', 'Approaching Budget Limit', 'You have spent 85% of your Food & Dining budget.', 'Warning', FALSE, 1, 'Budget'),
('550e8400-e29b-41d4-a716-446655440001', 'Receipt Processed', 'Receipt Processed', 'Your receipt has been processed successfully.', 'Success', TRUE, 1, 'Receipt'),
('550e8400-e29b-41d4-a716-446655440002', 'System', 'Welcome', 'Welcome to the expense tracker!', 'Info', FALSE, NULL, NULL);