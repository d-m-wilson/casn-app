CREATE PROCEDURE [dbo].[uspCheckBadgeAllClinics]
    @VolunteerId INT
AS
BEGIN
    SET NOCOUNT ON

    SET NOCOUNT ON;
    DECLARE @MinDriveCount INT
    
    SELECT @MinDriveCount = MIN(x.[DriveCount]) FROM (
        SELECT COUNT(d.Id) AS [DriveCount] FROM [dbo].[Clinic] c
        LEFT OUTER JOIN [dbo].[Appointment] a ON a.ClinicId = c.Id
        LEFT OUTER JOIN [dbo].[Drive] d ON d.AppointmentId = a.Id AND d.DriverId = @VolunteerId
        GROUP BY c.Id
    ) AS x

    IF (@MinDriveCount > 0)
    BEGIN
        RETURN 1
    END

    RETURN 0
END
