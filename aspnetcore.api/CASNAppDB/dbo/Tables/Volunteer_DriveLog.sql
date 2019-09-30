CREATE TABLE [dbo].[Volunteer_DriveLog] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [VolunteerId] INT      NOT NULL,
    [DriveId]     INT      NOT NULL,
    [DriveLogStatusId] INT NOT NULL,
    [Canceled]    DATETIME NULL,
    [Approved]    DATETIME NULL,
    [Reassigned]  DATETIME NULL,
    [IsActive]    BIT      CONSTRAINT [DF_Volunteer_DriveLog_IsActive] DEFAULT (1) NOT NULL,
    [Created]     DATETIME CONSTRAINT [DF_Volunteer_DriveLog_Created] DEFAULT (GETUTCDATE()) NOT NULL,
    [Updated]     DATETIME NULL,
    CONSTRAINT [PK_Volunteer_DriveLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Volunteer_DriveLog_DriveId] FOREIGN KEY ([DriveId]) REFERENCES [dbo].[Drive] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Volunteer_DriveLog_VolunteerId] FOREIGN KEY ([VolunteerId]) REFERENCES [dbo].[Volunteer] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Volunteer_DriveLog_DriveLogStatusId] FOREIGN KEY ([DriveLogStatusId]) REFERENCES [dbo].[DriveLogStatus] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [FK_Volunteer_Drive_DriveId_idx]
    ON [dbo].[Volunteer_DriveLog]([DriveId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Volunteer_Drive_DriveLogStatusId_idx]
    ON [dbo].[Volunteer_DriveLog]([DriveLogStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Volunteer_Drive_VolunteerId_idx]
    ON [dbo].[Volunteer_DriveLog]([VolunteerId] ASC);
