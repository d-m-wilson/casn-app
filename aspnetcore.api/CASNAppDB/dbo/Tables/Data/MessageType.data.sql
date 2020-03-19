BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()
    INSERT [dbo].[MessageType] ([Id], [Name], [IsActive], [Created], [Updated]) VALUES 
    (0, N'Unknown', 1, @utcNow, NULL),
    (1, N'ApptAddedOneWayToServiceProvider', 1, @utcNow, NULL),
    (2, N'ApptAddedOneWayFromServiceProvider', 1, @utcNow, NULL),
    (3, N'ApptAddedRoundTripSameAddress', 1, @utcNow, NULL),
    (4, N'ApptAddedRoundTripDiffAddress', 1, @utcNow, NULL),
    (5, N'ApptAddedToday', 1, @utcNow, NULL),
    (6, N'FriendlyReminder', 1, @utcNow, NULL),
    (7, N'SeriousRequest', 1, @utcNow, NULL),
    (8, N'DesperatePlea', 1, @utcNow, NULL),
    (9, N'DriverAppliedForDrive', 1, @utcNow, NULL),
    (10, N'DriveCanceled', 1, @utcNow, NULL),
    (11, N'DriverApprovedForDrive', 1, @utcNow, NULL),
    (12, N'BadgeMessageTemplate', 1, @utcNow, NULL),
    (13, N'DriverRetractedApplication', 1, @utcNow, NULL),
    (14, N'DispatcherDeclinedApplication', 1, @utcNow, NULL)
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
