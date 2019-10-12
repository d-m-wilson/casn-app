BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()
    INSERT [dbo].[AppointmentType] ([Id], [Name], [Title], [Description], [EstimatedDurationMinutes], [IsActive], [Created], [Updated]) VALUES (3, N'surgical_appointment', N'Surgical Appointment', N'Appointment to undergo surgical procedure.', 210, 1, @utcNow, NULL)
    INSERT [dbo].[AppointmentType] ([Id], [Name], [Title], [Description], [EstimatedDurationMinutes], [IsActive], [Created], [Updated]) VALUES (4, N'ultrasound appointment', N'Ultrasound Appointment', N'Appointment to receive ultrasound in advance of surgical procedure.', 150, 1, @utcNow, NULL)
    INSERT [dbo].[AppointmentType] ([Id], [Name], [Title], [Description], [EstimatedDurationMinutes], [IsActive], [Created], [Updated]) VALUES (5, N'lam insert', N'Lam Insert', NULL, 90, 1, @utcNow, NULL)
    INSERT [dbo].[AppointmentType] ([Id], [Name], [Title], [Description], [EstimatedDurationMinutes], [IsActive], [Created], [Updated]) VALUES (6, N'lam to complete', N'Lam to Complete', NULL, 180, 1, @utcNow, NULL)
    INSERT [dbo].[AppointmentType] ([Id], [Name], [Title], [Description], [EstimatedDurationMinutes], [IsActive], [Created], [Updated]) VALUES (7, N'courthouse appointment', N'Courthouse Appointment', NULL, 60, 1, @utcNow, NULL)
    INSERT [dbo].[AppointmentType] ([Id], [Name], [Title], [Description], [EstimatedDurationMinutes], [IsActive], [Created], [Updated]) VALUES (8, N'follow up appointment', N'Follow-up Appointment', NULL, 30, 1, @utcNow, NULL)
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
