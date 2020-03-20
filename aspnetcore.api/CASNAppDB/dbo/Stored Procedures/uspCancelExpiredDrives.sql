CREATE PROCEDURE [dbo].[uspCancelExpiredDrives] (@debugMode BIT = 0)
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @canceledStatus INT = 3
    DECLARE @cancelReason INT = 2 -- Unable to Staff
    DECLARE @utcNow DATETIME = GETUTCDATE()

    DECLARE @affectedRows INT

    BEGIN TRY
        BEGIN TRANSACTION

        UPDATE d
        SET [StatusId] = @canceledStatus,
            [CancelReasonId] = @cancelReason,
            [Updated] = GETUTCDATE()
        FROM [dbo].[Drive] d
        INNER JOIN [dbo].[Appointment] a ON a.[Id] = d.[AppointmentId]
        WHERE d.[StatusId] IN (0, 1) -- Open or Pending status
        AND a.[AppointmentDate] < @utcNow -- Appointment date & time has passed
        AND a.[CallerId] IS NOT NULL -- Exclude redacted records

        SELECT @affectedRows = @@ROWCOUNT

        IF (@debugMode = 1)
        BEGIN
            SELECT @affectedRows [DrivesCanceled]

            SELECT a.[Id] ApptId, d.[Id] DriveId, a.[AppointmentDate], a.ServiceProviderId, a.CallerId, d.Direction, d.DriverId, d.Updated
            FROM [dbo].[Drive] d
            INNER JOIN [dbo].[Appointment] a ON a.[Id] = d.[AppointmentId]
            WHERE d.[StatusId] = @canceledStatus AND d.[CancelReasonId] = @cancelReason AND d.[Updated] = @utcNow
            AND a.[AppointmentDate] < @utcNow AND a.[CallerId] IS NOT NULL

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
