CREATE PROCEDURE [dbo].[uspCheckBadgeSingleClinic]
    @VolunteerId INT,
    @ServiceProviderId INT,
	@CountTarget INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @DriveCount INT
    
    SELECT @DriveCount = COUNT(*) FROM [dbo].[Volunteer_DriveLog] vdl
    INNER JOIN [dbo].[Drive] d ON d.Id = vdl.DriveId
    INNER JOIN [dbo].[Appointment] a ON a.Id = d.AppointmentId
    WHERE vdl.VolunteerId = @VolunteerId
        AND vdl.IsActive = 1
        AND a.ServiceProviderId = @ServiceProviderId

    IF (@DriveCount = @CountTarget)
    BEGIN
        RETURN 1
    END

    RETURN 0
END