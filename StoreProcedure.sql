USE TestDb
GO
CREATE PROCEDURE ProductSearch 
	@productName NVARCHAR(225),
    @productVersionName NVARCHAR(225),
	@minSize decimal,
	@maxSize decimal
AS
BEGIN
    SELECT pv.Id as ProductVersionId, p.Name as ProductName, pv.Name as ProductVersion, pv.Width as Width, pv.Length as Length, pv.Height as Height 
	FROM Products p JOIN ProductVersions pv on p.Id = pv.ProductId
    WHERE p.Name LIKE Concat('%', @productName, '%') AND pv.Name LIKE Concat('%', @productVersionName, '%')
	AND (pv.Height * pv.Width * pv.Length) >= @minSize and (pv.Height * pv.Width * pv.Length) <= @maxSize
END