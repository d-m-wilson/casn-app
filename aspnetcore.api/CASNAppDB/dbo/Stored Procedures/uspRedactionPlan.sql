CREATE PROCEDURE [dbo].[uspRedactionPlan] (@daysToKeep INT, @debugMode BIT = 0)
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        BEGIN TRANSACTION

        DECLARE @apptCount INT
        DECLARE @driveCount INT
        DECLARE @callerCount INT

        DECLARE @thresholdDate DATETIME = DATEADD(DAY, -@daysToKeep, GETUTCDATE())

        DECLARE @oldApptIds TABLE ([Id] INT NOT NULL PRIMARY KEY)

        INSERT INTO @oldApptIds ([Id])
        SELECT Id FROM [dbo].[Appointment]
        WHERE AppointmentDate < @thresholdDate AND AppointmentDate >= DATEADD(DAY, -7, @thresholdDate)

        UPDATE [dbo].[Appointment] SET
            PickupLocationVague = NULL,
            PickupVagueGeocoded = NULL,
            DropoffLocationVague = NULL,
            DropoffVagueGeocoded = NULL
        WHERE Id IN (SELECT Id FROM @oldApptIds)

        SELECT @apptCount = @@ROWCOUNT

        UPDATE [dbo].[Drive] SET
            StartAddress = NULL,
            StartCity = NULL,
            StartState = NULL,
            StartLatitude = NULL,
            StartLongitude = NULL,
            StartGeocoded = NULL,
            EndAddress = NULL,
            EndCity = NULL,
            EndState = NULL,
            EndLatitude = NULL,
            EndLongitude = NULL,
            EndGeocoded = NULL
        WHERE AppointmentId IN (SELECT Id FROM @oldApptIds)

        SELECT @driveCount = @@ROWCOUNT

        DECLARE @callerIdsToKeep TABLE ([Id] INT NOT NULL PRIMARY KEY)

        INSERT INTO @callerIdsToKeep ([Id])
        SELECT DISTINCT a.CallerId FROM [dbo].[Appointment] a
        WHERE a.AppointmentDate >= @thresholdDate AND a.IsActive = 1

        UPDATE [dbo].[Caller] SET
            CiviContactId = 0,
            CallerIdentifier = '',
            FirstName = '',
            LastName = '',
            Phone = '',
            IsMinor = 0,
            Note = NULL,
            IsActive = 0,
            Created = '2000-01-01',
            Updated = GETUTCDATE()
        WHERE Id NOT IN (SELECT Id FROM @callerIdsToKeep) AND CallerIdentifier != ''

        SELECT @callerCount = @@ROWCOUNT

        IF (@debugMode = 1)
        BEGIN
            SELECT @driveCount [DrivesRedacted], @apptCount [ApptsRedacted], @callerCount [CallersDeleted]
            SELECT Id, CallerIdentifier, FirstName, LastName FROM [dbo].[Caller] ORDER BY Id ASC
            ROLLBACK TRANSACTION
        END
        ELSE
        BEGIN
            COMMIT TRANSACTION
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
    END CATCH
END
