BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (0, N'Unknown', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (1, N'ApptAddedOneWayToServiceProvider', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (2, N'ApptAddedOneWayFromServiceProvider', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (3, N'ApptAddedRoundTripSameAddress', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (4, N'ApptAddedRoundTripDiffAddress', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (5, N'ApptAddedToday', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (6, N'FriendlyReminder', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (7, N'SeriousRequest', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (8, N'DesperatePlea', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (9, N'DriverAppliedForDrive', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (10, N'DriveCanceled', 1, @utcNow, NULL)
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES (11, N'DriverApprovedForDrive', 1, @utcNow, NULL)
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
