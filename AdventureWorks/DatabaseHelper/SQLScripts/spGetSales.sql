USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[spGetSales]    Script Date: 6/8/2022 12:10:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetSales]	
AS
	BEGIN
	  SELECT	
		YEAR(sh.QuotaDate) years,
		CONVERT(numeric(10,0), SUM(sh.SalesQuota)) sales
	  FROM Sales.SalesPerson sp
		INNER JOIN Sales.SalesPersonQuotaHistory sh
		ON sp.BusinessEntityID = sh.BusinessEntityID
	  GROUP BY YEAR(sh.QuotaDate)	
	END
