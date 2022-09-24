INSERT INTO Products VALUES (NEWID(), 'Samsung galaxy S22', '4 รแ, 2 SIM, 2400x1080')
INSERT INTO Products VALUES (NEWID(), 'PC', 'Intel Core i9')
INSERT INTO Products VALUES (NEWID(), 'Mouse', 'Hyper X')

declare @p1Id uniqueidentifier;
declare @p2Id uniqueidentifier;
declare @p3Id uniqueidentifier;

set @p1Id = (select top 1 Id from Products where Name = 'Samsung galaxy S22')
set @p2Id = (select top 1 Id from Products where Name = 'PC')
set @p3Id = (select top 1 Id from Products where Name = 'Mouse')


INSERT INTO ProductVersions VALUES (NEWID(), @p1Id, 'Samsung galaxy S22 v2', '6 รแ, 2 SIM, 2400x1080', getdate(), 200, 100, 50)
INSERT INTO ProductVersions VALUES (NEWID(), @p2Id, 'PC v2', 'Intel Core i11', getdate(), 500, 300, 150)
INSERT INTO ProductVersions VALUES (NEWID(), @p3Id, 'Mouse v2', 'Hyper X v2', getdate(), 100, 50, 25)
