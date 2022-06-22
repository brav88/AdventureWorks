
CREATE PROCEDURE [dbo].[spGetProducts]
	@id INT
AS
	BEGIN
		SELECT [ProductID]
			  ,[Name]
			  ,[ProductNumber]
			  ,[Color]
			  ,[ListPrice]
		  FROM [Production].[Product] p
		WHERE p.ProductSubcategoryID = @id
	END
