BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()
    INSERT INTO [dbo].[DriveLogStatus] ([Id], [Name], [IsActive], [Created]) VALUES (1, 'Applied', 1, @utcNow)
    INSERT INTO [dbo].[DriveLogStatus] ([Id], [Name], [IsActive], [Created]) VALUES (2, 'Canceled', 1, @utcNow)
    INSERT INTO [dbo].[DriveLogStatus] ([Id], [Name], [IsActive], [Created]) VALUES (3, 'Approved', 1, @utcNow)
    INSERT INTO [dbo].[DriveLogStatus] ([Id], [Name], [IsActive], [Created]) VALUES (4, 'Reassigned', 1, @utcNow)
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
