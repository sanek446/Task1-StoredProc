CREATE PROCEDURE [dbo].[insertSP]

@valuesString nvarchar(250)
	
AS
INSERT INTO [dbo].Machine VALUES (@valuesString)

	
RETURN 0