CREATE PROCEDURE [dbo].[uspCheckBadgeDriveCount]
    @VolunteerId INT,
	@CountTarget INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @DriveCount INT

    SELECT @DriveCount = COUNT(*) FROM [dbo].[Volunteer_DriveLog] vdl
    WHERE vdl.VolunteerId = @VolunteerId
        AND vdl.IsActive = 1

    IF (@DriveCount = @CountTarget)
    BEGIN
        RETURN 1
    END

    RETURN 0
END