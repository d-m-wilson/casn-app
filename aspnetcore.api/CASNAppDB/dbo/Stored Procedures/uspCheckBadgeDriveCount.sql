CREATE PROCEDURE [dbo].[uspCheckBadgeDriveCount]
    @VolunteerId INT,
	@CountTarget INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @DriveCount INT
    DECLARE @approvedDriveLogStatus INT = 3

    SELECT @DriveCount = COUNT(*) FROM [dbo].[Volunteer_DriveLog] vdl
    WHERE vdl.VolunteerId = @VolunteerId
        AND vdl.IsActive = 1
        AND vdl.DriveLogStatusId = @approvedDriveLogStatus

    IF (@DriveCount = @CountTarget)
    BEGIN
        RETURN 1
    END

    RETURN 0
END