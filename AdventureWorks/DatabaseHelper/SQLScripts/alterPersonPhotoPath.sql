--Add column to storage the photo path
IF NOT EXISTS
(
SELECT *
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'PhotoPath' AND TABLE_NAME = 'Person'
)
BEGIN
  ALTER TABLE [Person].[Person] ADD PhotoPath VARCHAR(100);
  UPDATE [Person].[Person] SET PhotoPath = '/Files/0.jpg';
END