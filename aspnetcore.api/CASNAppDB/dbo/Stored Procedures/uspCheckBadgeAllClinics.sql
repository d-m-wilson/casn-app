CREATE PROCEDURE [dbo].[uspCheckBadgeAllClinics]
    @VolunteerId INT
AS
BEGIN
    SET NOCOUNT ON
    DECLARE @SPType_Clinic INT = 1
    DECLARE @MinDriveCount INT
    
    SELECT @MinDriveCount = MIN(x.[DriveCount]) FROM (
        SELECT COUNT(d.Id) AS [DriveCount] FROM [dbo].[ServiceProvider] c
        LEFT OUTER JOIN [dbo].[Appointment] a ON a.ServiceProviderId = c.Id
        LEFT OUTER JOIN [dbo].[Drive] d ON d.AppointmentId = a.Id AND d.DriverId = @VolunteerId
        WHERE c.ServiceProviderTypeId = @SPType_Clinic
        GROUP BY c.Id
    ) AS x

    IF (@MinDriveCount > 0)
    BEGIN
        RETURN 1
    END

    RETURN 0
END
