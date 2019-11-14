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
        WHERE CallerId IS NOT NULL
        AND AppointmentDate < @thresholdDate

        UPDATE [dbo].[Appointment] SET
            CallerId = NULL,
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

        DECLARE @oldCallerIds TABLE ([Id] INT NOT NULL PRIMARY KEY)

        INSERT INTO @oldCallerIds ([Id])
        SELECT DISTINCT c.Id FROM [dbo].[Caller] c
        LEFT OUTER JOIN [dbo].[Appointment] a ON a.CallerId = c.Id
        WHERE a.CallerId IS NULL AND
        c.Created < @thresholdDate AND
        (c.Updated IS NULL OR c.Updated < @thresholdDate)

        DELETE FROM [dbo].[Caller]
        WHERE Id IN (SELECT Id FROM @oldCallerIds)

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
