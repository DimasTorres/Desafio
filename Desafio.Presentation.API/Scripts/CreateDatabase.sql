IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DesafioDB')
BEGIN
    CREATE DATABASE DesafioDB
END
--GO

USE DesafioDB

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    -- Criação da tabela Users
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(60) NOT NULL,
        Login VARCHAR(20) NOT NULL,
        PasswordHash VARCHAR(255) NOT NULL,
        Email VARCHAR(60) NOT NULL,
        IsDeleted BIT DEFAULT 0,
        CreatedAt DATETIME DEFAULT GETDATE()
    );

    -- Criação da tabela Products
    CREATE TABLE Products (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ProductName VARCHAR(20) NOT NULL,
        Value DECIMAL(10,2),
        IsDeleted BIT DEFAULT 0,
        CreatedAt DATETIME DEFAULT GETDATE()
    );

    -- Criação da tabela Orders
    CREATE TABLE Orders (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ClientName VARCHAR(60) NOT NULL,
        ClientEmail VARCHAR(60) NOT NULL,
        IsPaid BIT DEFAULT 0,
	    UserId INT NOT NULL,
        IsDeleted BIT DEFAULT 0,
        CreatedAt DATETIME DEFAULT GETDATE(),
	    FOREIGN KEY (UserId) REFERENCES Users(Id),
    );

    -- Criação da tabela OrderItems
    CREATE TABLE OrderItems (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL,
        ProductId INT NOT NULL,
        Amount INT,
        IsDeleted BIT DEFAULT 0,
        CreatedAt DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (OrderId) REFERENCES Orders(Id),
        FOREIGN KEY (ProductId) REFERENCES Products(Id)
    );

    SET IDENTITY_INSERT [dbo].[Users] ON 
    INSERT [dbo].[Users] ([Id], [Name], [Login], [PasswordHash], [Email], [IsDeleted], [CreatedAt]) VALUES (1, N'Dimas Duarte', N'dgduarte', N'HyGP/mFghoh8U3DqtzPvLj83/A44hEwBPI55+Cg+89KMVW803V6j+CsKOcv4UkHC', N'dgduarte@stefanini.com', 0, CAST(N'2024-09-07T00:41:48.873' AS DateTime))
    SET IDENTITY_INSERT [dbo].[Users] OFF

    SET IDENTITY_INSERT [dbo].[Products] ON 
    INSERT [dbo].[Products] ([Id], [ProductName], [Value], [IsDeleted], [CreatedAt]) VALUES (1, N'Produto 1', CAST(10.55 AS Decimal(10, 2)), 0, CAST(N'2024-09-07T21:34:11.797' AS DateTime))
    INSERT [dbo].[Products] ([Id], [ProductName], [Value], [IsDeleted], [CreatedAt]) VALUES (2, N'Produto 2', CAST(12.51 AS Decimal(10, 2)), 0, CAST(N'2024-09-07T21:34:30.960' AS DateTime))
    INSERT [dbo].[Products] ([Id], [ProductName], [Value], [IsDeleted], [CreatedAt]) VALUES (3, N'Produto 3', CAST(2.99 AS Decimal(10, 2)), 0, CAST(N'2024-09-07T21:34:46.857' AS DateTime))
    INSERT [dbo].[Products] ([Id], [ProductName], [Value], [IsDeleted], [CreatedAt]) VALUES (4, N'Produto 4', CAST(0.69 AS Decimal(10, 2)), 0, CAST(N'2024-09-07T21:35:02.773' AS DateTime))
    INSERT [dbo].[Products] ([Id], [ProductName], [Value], [IsDeleted], [CreatedAt]) VALUES (5, N'Produto 5', CAST(16.00 AS Decimal(10, 2)), 0, CAST(N'2024-09-07T21:35:20.660' AS DateTime))
    SET IDENTITY_INSERT [dbo].[Products] OFF
    
    SET IDENTITY_INSERT [dbo].[Orders] ON 
    INSERT [dbo].[Orders] ([Id], [ClientName], [ClientEmail], [IsPaid], [IsDeleted], [CreatedAt], [UserId]) VALUES (1, N'Cliente 1', N'cliente1@client.com', 1, 0, CAST(N'2024-09-07T21:51:31.063' AS DateTime), 1)
    INSERT [dbo].[Orders] ([Id], [ClientName], [ClientEmail], [IsPaid], [IsDeleted], [CreatedAt], [UserId]) VALUES (2, N'Cliente 1', N'cliente1@client.com', 1, 0, CAST(N'2024-09-07T22:07:52.347' AS DateTime), 1)
    SET IDENTITY_INSERT [dbo].[Orders] OFF
    
    SET IDENTITY_INSERT [dbo].[OrderItems] ON 
    INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [Amount], [IsDeleted], [CreatedAt]) VALUES (1, 1, 1, 10, 0, CAST(N'2024-09-07T21:51:31.333' AS DateTime))
    INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [Amount], [IsDeleted], [CreatedAt]) VALUES (2, 1, 4, 1, 0, CAST(N'2024-09-07T21:51:31.343' AS DateTime))
    INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [Amount], [IsDeleted], [CreatedAt]) VALUES (3, 2, 2, 100, 0, CAST(N'2024-09-07T22:07:52.367' AS DateTime))
    INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [Amount], [IsDeleted], [CreatedAt]) VALUES (4, 2, 5, 10, 0, CAST(N'2024-09-07T22:07:52.373' AS DateTime))
    SET IDENTITY_INSERT [dbo].[OrderItems] OFF
END
--GO
