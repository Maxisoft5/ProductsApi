
CREATE DATABASE TestDb
GO
USE TestDb
GO

CREATE TABLE Products (
	Id UNIQUEIDENTIFIER PRIMARY KEY,
	[Name] NVARCHAR(225) NOT NULL UNIQUE,
	[Description] NVARCHAR(MAX)
);

CREATE NONCLUSTERED INDEX ProductName ON Products ([Name]) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

CREATE TABLE ProductVersions (
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	ProductId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Products(Id) ON DELETE CASCADE,
	[Name] NVARCHAR(225) NOT NULL,
	[Description] NVARCHAR(225),
	CreatingDate DATETIME2 NOT NULL DEFAULT GETDATE(),
	Width DECIMAL NOT NULL,
	Height DECIMAL NOT NULL,
	[Length] DECIMAL NOT NULL,
);
CREATE NONCLUSTERED INDEX ProductVersionsName ON ProductVersions ([Name]) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
CREATE NONCLUSTERED INDEX ProductVersionCreatingDate ON ProductVersions (CreatingDate) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
CREATE NONCLUSTERED INDEX ProductVersionWidth ON ProductVersions (Width) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
CREATE NONCLUSTERED INDEX ProductVersionHeight ON ProductVersions (Height) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
CREATE NONCLUSTERED INDEX ProductVersionLength ON ProductVersions ([Length]) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

CREATE TABLE EventLog(
	Id UNIQUEIDENTIFIER PRIMARY KEY NEWID(),
	EventDate DATETIME2 NOT NULL DEFAULT GETDATE(),
	[Description] NVARCHAR(MAX)
);

CREATE NONCLUSTERED INDEX EventLogDescription ON EventLog (EventDate) WITH (ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE TRIGGER AddProductTrigger ON Products 
AFTER INSERT
AS 
INSERT INTO EventLog (Id, [Description])
SELECT NEWID(), 'Добавлен товар - ' + [Name] FROM INSERTED
GO

CREATE TRIGGER DeleteProductTrigger ON Products 
AFTER DELETE
AS 
INSERT INTO EventLog (Id, [Description])
SELECT NEWID(), 'Удалён товар - ' + [Name] FROM DELETED
GO

CREATE TRIGGER AlterProductTrigger ON Products 
AFTER UPDATE
AS 
INSERT INTO EventLog (Id, [Description])
SELECT NEWID(), 'Обновлён товар - ' + [Name] FROM INSERTED

GO
CREATE TRIGGER AddProductVersionsTrigger ON ProductVersions 
AFTER INSERT
AS 
INSERT INTO EventLog (Id, [Description])
SELECT NEWID(), CONCAT('Добавлена версия товара - ' + [Name] + '. Длина: ', [Length], ', ширина: ', [Width], ', высота: ', Height) FROM INSERTED

GO
CREATE TRIGGER DeleteProductVersionsTrigger ON ProductVersions 
AFTER INSERT
AS 
INSERT INTO EventLog (Id, [Description])
SELECT NEWID(), CONCAT('Удалена версия товара - ' + [Name] + '. Длина: ', [Length], ', ширина: ', [Width], ', высота: ', Height) FROM DELETED

GO
CREATE TRIGGER AlterProductVersionsTrigger ON ProductVersions 
AFTER INSERT
AS 
INSERT INTO EventLog (Id, [Description])
SELECT NEWID(), CONCAT('Обновлена версия товара - ' + [Name] + '. Длина: ', [Length], ', ширина: ', [Width], ', высота: ', Height) FROM INSERTED
GO


