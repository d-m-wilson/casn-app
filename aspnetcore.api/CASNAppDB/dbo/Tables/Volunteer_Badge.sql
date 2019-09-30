CREATE TABLE [dbo].[Volunteer_Badge] (
    [Id]          INT      IDENTITY (2, 1) NOT NULL,
    [VolunteerId] INT      NOT NULL,
    [BadgeId]     INT      NOT NULL,
    [DriveLogId]  INT      NOT NULL,
    [Created]     DATETIME CONSTRAINT [DF_Volunteer_Badge_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]     DATETIME NULL,
    CONSTRAINT [PK_Volunteer_Badge] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Volunteer_Badge_BadgeId] FOREIGN KEY ([BadgeId]) REFERENCES [dbo].[Badge] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Volunteer_Badge_VolunteerId] FOREIGN KEY ([VolunteerId]) REFERENCES [dbo].[Volunteer] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Volunteer_Badge_DriveLogId] FOREIGN KEY ([DriveLogId]) REFERENCES [dbo].[Volunteer_DriveLog] ([Id]) ON DELETE NO ACTION,
);


GO
CREATE NONCLUSTERED INDEX [FK_Volunteer_Badge_BadgeId_idx]
    ON [dbo].[Volunteer_Badge]([BadgeId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Volunteer_Badge_VolunteerId_idx]
    ON [dbo].[Volunteer_Badge]([VolunteerId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Volunteer_Badge_DriveLogId_idx]
    ON [dbo].[Volunteer_Badge]([DriveLogId] ASC);
