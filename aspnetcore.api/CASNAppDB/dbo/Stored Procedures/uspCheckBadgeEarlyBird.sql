CREATE PROCEDURE [dbo].[uspCheckBadgeEarlyBird]
    @VolunteerId INT,
    @VolunteerDriveLogId INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @driveId INT = NULL
    DECLARE @apptId INT = NULL
    DECLARE @apptDate DATETIME = NULL
    DECLARE @applied DATETIME = NULL
    DECLARE @days INT = NULL

    SELECT @driveId = [DriveId], @applied = [Created] 
    FROM [dbo].[Volunteer_DriveLog] WHERE [Id] = @VolunteerDriveLogId

    IF (@driveId IS NULL)
    BEGIN
        RETURN 0
    END

    SELECT @apptId = [AppointmentId] FROM [dbo].[Drive] WHERE [Id] = @driveId

    IF (@apptId IS NULL)
    BEGIN
        RETURN 0
    END

    SELECT @apptDate = [AppointmentDate] FROM [dbo].[Appointment] WHERE [Id] = @apptId

    IF (@apptDate IS NULL)
    BEGIN
        RETURN 0
    END

    SET @days = DATEDIFF(DAY, @applied, @apptDate)

    IF (@days IS NOT NULL AND @days >= 7)
    BEGIN
        RETURN 1
    END

    RETURN 0
END
