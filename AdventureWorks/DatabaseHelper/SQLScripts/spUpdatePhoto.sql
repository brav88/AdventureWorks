USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[spUpdatePhoto]    Script Date: 5/26/2022 5:21:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdatePhoto]
	@BusinessEntityID INT,
	@PhotoPath VARCHAR(100)
AS
	BEGIN
		UPDATE [Person].[Person] 
		SET PhotoPath = @PhotoPath
		WHERE BusinessEntityID = @BusinessEntityID		
	END
