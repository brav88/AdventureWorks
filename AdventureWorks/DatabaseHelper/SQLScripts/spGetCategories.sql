CREATE PROCEDURE [dbo].[spGetCategories]
AS
	BEGIN
		SELECT ProductCategoryID Id,
			   [Name] [Value]     
		FROM Production.ProductCategory
		ORDER BY ProductCategoryID ASC
	END
