USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[spIsUserValid]    Script Date: 5/25/2022 12:22:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spIsUserValid]
	@email VARCHAR(50),
	@password VARCHAR(50)
AS
	BEGIN
		SELECT 
			pp.FirstName + ' ' + pp.LastName [Name],
			e.EmailAddress			
		FROM [Person].[EmailAddress] e
			INNER JOIN [Person].[Person] pp
			ON e.BusinessEntityID = pp.BusinessEntityID
			INNER JOIN [Person].[Password] p
			ON e.BusinessEntityID = p.BusinessEntityID
			AND e.EmailAddress = @email
			AND p.PasswordSalt = @password
	END
