BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()
    INSERT [dbo].[DriveCancelReason] ([Id], [Name], [Description], [IsActive], [Created], [Updated]) VALUES (1, N'Canceled by Caller', NULL, 1, @utcNow, NULL)
    INSERT [dbo].[DriveCancelReason] ([Id], [Name], [Description], [IsActive], [Created], [Updated]) VALUES (2, N'Unable to Staff', NULL, 1, @utcNow, NULL)
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
