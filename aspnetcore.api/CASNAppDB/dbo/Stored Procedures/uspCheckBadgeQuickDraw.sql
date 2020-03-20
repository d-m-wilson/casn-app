CREATE PROCEDURE [dbo].[uspCheckBadgeQuickDraw]
    @VolunteerId INT,
    @VolunteerDriveLogId INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @driveId INT = NULL
    DECLARE @drivePosted DATETIME = NULL
    DECLARE @applied DATETIME = NULL
    DECLARE @minutes INT = NULL

    SELECT @driveId = [DriveId], @applied = [Created] FROM [dbo].[Volunteer_DriveLog] WHERE [Id] = @VolunteerDriveLogId

    IF (@driveId IS NULL OR @applied IS NULL)
    BEGIN
        RETURN 0
    END

    SELECT @drivePosted = [Created] FROM [dbo].[Drive] WHERE [Id] = @driveId

    IF (@drivePosted IS NULL)
    BEGIN
        RETURN 0
    END

    SET @minutes = DATEDIFF(MINUTE, @drivePosted, @applied)

    IF (@minutes IS NOT NULL AND @minutes <= 30)
    BEGIN
        RETURN 1
    END

    RETURN 0
END
