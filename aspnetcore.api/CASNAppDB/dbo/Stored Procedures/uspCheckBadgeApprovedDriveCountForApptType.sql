CREATE PROCEDURE [dbo].[uspCheckBadgeApprovedDriveCountForApptType]
    @VolunteerId INT,
    @CountTarget INT,
    @AppointmentTypeId INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @DriveCount INT
    DECLARE @approvedDriveLogStatus INT = 3

    SELECT @DriveCount = COUNT(*) FROM [dbo].[Volunteer_DriveLog] vdl
    INNER JOIN [dbo].[Drive] d ON d.Id = vdl.DriveId
    INNER JOIN [dbo].[Appointment] a ON a.Id = d.AppointmentId
    WHERE vdl.VolunteerId = @VolunteerId
        AND vdl.IsActive = 1
        AND vdl.DriveLogStatusId = @approvedDriveLogStatus
        AND a.AppointmentTypeId = @AppointmentTypeId

    IF (@DriveCount = @CountTarget)
    BEGIN
        RETURN 1
    END

    RETURN 0
END
