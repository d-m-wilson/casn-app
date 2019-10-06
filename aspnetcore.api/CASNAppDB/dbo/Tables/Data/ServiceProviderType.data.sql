BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()
    INSERT INTO [dbo].[ServiceProviderType] ([Id], [Name], [IsActive], [Created]) VALUES (1, 'Clinic', 1, @utcNow)
    INSERT INTO [dbo].[ServiceProviderType] ([Id], [Name], [IsActive], [Created]) VALUES (2, 'Courthouse', 1, @utcNow)
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
