CREATE TABLE [dbo].[Volunteer_Drive] (
    [Id]          INT      IDENTITY (13, 1) NOT NULL,
    [VolunteerId] INT      NOT NULL,
    [DriveId]     INT      NOT NULL,
    [IsActive]    BIT      CONSTRAINT [DF_Volunteer_Drive_IsActive] DEFAULT ((1)) NOT NULL,
    [Created]     DATETIME CONSTRAINT [DF_Volunteer_Drive_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]     DATETIME NULL,
    CONSTRAINT [PK_Volunteer_Drive] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Volunteer_Drive_DriveId] FOREIGN KEY ([DriveId]) REFERENCES [dbo].[Drive] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Volunteer_Drive_VolunteerId] FOREIGN KEY ([VolunteerId]) REFERENCES [dbo].[Volunteer] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [FK_Volunteer_Drive_DriveId_idx]
    ON [dbo].[Volunteer_Drive]([DriveId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Volunteer_Drive_VolunteerId_idx]
    ON [dbo].[Volunteer_Drive]([VolunteerId] ASC);

