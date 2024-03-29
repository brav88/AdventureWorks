USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[spIsUserValid]    Script Date: 5/26/2022 5:18:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetUser]
	@email VARCHAR(50),
	@password VARCHAR(50)
AS
	BEGIN
		SELECT 
			pp.BusinessEntityID,
			pp.FirstName + ' ' + pp.LastName [Name],
			e.EmailAddress,
			em.JobTitle,
			em.HireDate,
			d.Name Department,
			pp.PhotoPath,
			ad.AddressLine1 + ', ' + ad.City Address,
			em.VacationHours,
			em.SickLeaveHours
		FROM [Person].[Person] pp
			INNER JOIN [HumanResources].[Employee] em
				INNER JOIN [HumanResources].[EmployeeDepartmentHistory] dh
					INNER JOIN [HumanResources].[Department] d
					ON dh.DepartmentID = d.DepartmentID
				ON em.BusinessEntityID = dh.BusinessEntityID
				AND dh.EndDate IS NULL
			ON pp.BusinessEntityID = em.BusinessEntityID
			
			INNER JOIN [Person].[Password] pw
			ON pp.BusinessEntityID = pw.BusinessEntityID

			INNER JOIN [Person].[EmailAddress] e
			ON e.BusinessEntityID = pp.BusinessEntityID

			LEFT JOIN [Person].[BusinessEntityAddress] bea
				INNER JOIN [Person].[Address] ad
					INNER JOIN [Person].[StateProvince] sp
					ON ad.StateProvinceID = sp.StateProvinceID
				ON bea.AddressID = ad.AddressID
			ON pp.BusinessEntityID = bea.BusinessEntityID
			
		WHERE e.EmailAddress = @email
		AND pw.PasswordSalt = @password
	END
