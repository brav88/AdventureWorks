
CREATE PROCEDURE [dbo].[spGetSubCategories]
	@id INT
AS
	BEGIN
		SELECT ProductSubcategoryID Id,
			   [Name] [Value]
		FROM Production.ProductSubcategory
		WHERE ProductCategoryID = @id
		ORDER BY ProductSubcategoryID ASC
	END
