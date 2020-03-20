BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()
    INSERT [dbo].[Badge] ([Id], [Title], [Description], [MessageText], [Path], [TriggerType], [ProcedureName], [ServiceProviderId], [CountTarget], [IncludeVolunteerDriveLogId], [AppointmentTypeId], [IsActive], [IsHidden], [Created], [Updated], [DisplayOrdinal]) VALUES 
    (1, N'First Drive', N'This badge is for completing your first drive.', N'Thank you for doing your part to support abortion access!', N'assets/badges/1.png', 2, N'uspCheckBadgeDriveCount', NULL, 1, 0, NULL, 1, 0, @utcNow, NULL, 1),
    (2, N'5 Drives', N'This badge is for helping five callers.', N'Way to go! You’ve completed 5 drives. Keep up the great work!', N'assets/badges/7.png', 2, N'uspCheckBadgeDriveCount', NULL, 5, 0, NULL, 1, 0, @utcNow, NULL, 2),
    (3, N'10 Drives', N'This badge is for helping ten callers.', N'Way to go! You’ve completed 10 drives. Keep up the great work!', N'assets/badges/19.png', 2, N'uspCheckBadgeDriveCount', NULL, 10, 0, NULL, 1, 0, @utcNow, NULL, 3),
    (4, N'HWC', N'This badge is for completing a drive to the Houston Women''s Clinic.', N'Thanks for completing a drive to Houston Women''s Clinic. :)', N'assets/badges/10.png', 2, N'uspCheckBadgeSingleClinic', 1, 1, 0, NULL, 1, 0, @utcNow, NULL, 9),
    (5, N'PP', N'This badge is for completing a drive to Planned Parenthood.', N'Thanks for completing a drive to Planned Parenthood. :)', N'assets/badges/11.png', 2, N'uspCheckBadgeSingleClinic', 2, 1, 0, NULL, 1, 0, @utcNow, NULL, 10),
    (6, N'AWC', N'This badge is for completing a drive to the Aaron Women''s Clinic.', N'Thanks for completing a drive to Aaron Women''s Clinic. :)', N'assets/badges/12.png', 2, N'uspCheckBadgeSingleClinic', 3, 1, 0, NULL, 1, 0, @utcNow, NULL, 11),
    (7, N'WCH', N'This badge is for completing a drive to the Women''s Center of Houston.', N'Thanks for completing a drive to Women''s Center of Houston. :)', N'assets/badges/13.png', 2, N'uspCheckBadgeSingleClinic', 4, 1, 0, NULL, 1, 0, @utcNow, NULL, 12),
    (8, N'Clean Sweep', N'This badge is for completing a drive to all clinics.', N'You’ve completed drives to all of our clinics. Thanks so much!', N'assets/badges/14.png', 2, N'uspCheckBadgeAllClinics', NULL, NULL, 0, NULL, 1, 0, @utcNow, NULL, 14),
    (9, N'20 Drives', N'This badge is for helping 20 callers.', N'Way to go! You’ve completed 20 drives. Keep up the great work!', N'assets/badges/20.png', 2, N'uspCheckBadgeDriveCount', NULL, 20, 0, NULL, 1, 0, @utcNow, NULL, 4),
    (10, N'50 Drives', N'This badge is for helping 50 callers.', N'Wow, you’ve done 50 drives. We just want to say thank you!', N'assets/badges/21.png', 2, N'uspCheckBadgeDriveCount', NULL, 50, 0, NULL, 1, 0, @utcNow, NULL, 5),
    (11, N'100 Drives', N'This badge is for helping 100 callers.', N'Amazing! You’ve done 100 drives. We can’t thank you enough.', N'assets/badges/22.png', 2, N'uspCheckBadgeDriveCount', NULL, 100, 0, NULL, 1, 0, @utcNow, NULL, 6),
    (12, N'150 Drives', N'This badge is for helping 150 callers.', N'You’ve done 150 drives. Way to show up for abortion access!', N'assets/badges/23.png', 2, N'uspCheckBadgeDriveCount', NULL, 150, 0, NULL, 1, 0, @utcNow, NULL, 7),
    (13, N'200 Drives', N'This badge is for helping 200 callers.', N'200 Drives!! You’re a true abortion access hero.', N'assets/badges/24.png', 2, N'uspCheckBadgeDriveCount', NULL, 200, 0, NULL, 1, 1, @utcNow, NULL, 8),
    (14, N'HWRS', N'This badge is for completing a drive to Houston Women''s Reproductive Services.', N'Thanks for completing a drive to Houston Women''s Reproductive Services. :)', N'assets/badges/25.png', 2, N'uspCheckBadgeSingleClinic', 10, 1, 0, NULL, 1, 0, @utcNow, NULL, 13),
    (15, N'Quick Draw', N'This badge is for applying for a drive within 30 minutes of it being posted.', N'Way to go, Speedy! You applied quickly after the drive was posted.', N'assets/badges/2.png', 1, N'uspCheckBadgeQuickDraw', NULL, NULL, 1, NULL, 1, 0, @utcNow, NULL, 15),
    (16, N'Early Bird', N'This badge is for applying a drive at least one week in advance.', N'Thanks for applying a week in advance! Way to support abortion access.', N'assets/badges/4.png', 1, N'uspCheckBadgeEarlyBird', NULL, NULL, 1, NULL, 1, 0, @utcNow, NULL, 16),
    (17, N'LTC', N'This badge is for completing a drive to a Lam to Complete appointment.', N'Lam to Complete appointments are so important to staff. Thank you for your help!', N'assets/badges/5.png', 2, N'uspCheckBadgeApprovedDriveCountForApptType', NULL, 1, 0, 6, 1, 0, @utcNow, NULL, 17)
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
