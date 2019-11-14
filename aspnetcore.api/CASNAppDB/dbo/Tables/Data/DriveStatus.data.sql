BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()
    INSERT [dbo].[DriveStatus] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (0, N'Open', 1, @utcNow, NULL)
    INSERT [dbo].[DriveStatus] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (1, N'Pending', 1, @utcNow, NULL)
    INSERT [dbo].[DriveStatus] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (2, N'Approved', 1, @utcNow, NULL)
    INSERT [dbo].[DriveStatus] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (3, N'Canceled', 1, @utcNow, NULL)
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
