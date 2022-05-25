USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[spIsUserValid]    Script Date: 5/25/2022 1:02:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spIsUserValid]
	@email VARCHAR(50),
	@password VARCHAR(50)
AS
	BEGIN
		SELECT 
			pp.FirstName + ' ' + pp.LastName [Name],
			e.EmailAddress,
			em.JobTitle,
			em.HireDate,
			d.Name Department
		FROM [Person].[EmailAddress] e
			INNER JOIN [Person].[Person] pp
				INNER JOIN [HumanResources].[Employee] em
					INNER JOIN [HumanResources].[EmployeeDepartmentHistory] dh
						INNER JOIN [HumanResources].[Department] d
						ON dh.DepartmentID = d.DepartmentID
					ON em.BusinessEntityID = dh.BusinessEntityID
					AND dh.EndDate IS NULL
				ON pp.BusinessEntityID = em.BusinessEntityID
			ON e.BusinessEntityID = pp.BusinessEntityID
			INNER JOIN [Person].[Password] p
			ON e.BusinessEntityID = p.BusinessEntityID
			AND e.EmailAddress = @email
			AND p.PasswordSalt = @password
	END
