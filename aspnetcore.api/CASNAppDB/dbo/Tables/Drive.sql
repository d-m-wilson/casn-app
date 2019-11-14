CREATE TABLE [dbo].[Drive] (
    [Id]              INT            IDENTITY (54, 1) NOT NULL,
    [AppointmentId]   INT            NOT NULL,
    [Direction]       TINYINT        NOT NULL,
    [StatusId]        INT            CONSTRAINT [DF_Drive_StatusId] DEFAULT ((0)) NOT NULL,
    [DriverId]        INT            NULL,
    [StartAddress]    NVARCHAR (100) NULL,
    [StartCity]       NVARCHAR (50)  NULL,
    [StartState]      NVARCHAR (30)  NULL,
    [StartPostalCode] NVARCHAR (10)  NULL,
    [StartLatitude]   DECIMAL (9, 6) NULL,
    [StartLongitude]  DECIMAL (9, 6) NULL,
    [StartGeocoded]   DATETIME       NULL,
    [EndAddress]      NVARCHAR (100) NULL,
    [EndCity]         NVARCHAR (50)  NULL,
    [EndState]        NVARCHAR (30)  NULL,
    [EndPostalCode]   NVARCHAR (10)  NULL,
    [EndLatitude]     DECIMAL (9, 6) NULL,
    [EndLongitude]    DECIMAL (9, 6) NULL,
    [EndGeocoded]     DATETIME       NULL,
    [IsActive]        BIT            CONSTRAINT [DF_Drive_IsActive] DEFAULT (0x01) NOT NULL,
    [Created]         DATETIME       CONSTRAINT [DF_Drive_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]         DATETIME       NULL,
    [Approved]        DATETIME       NULL,
    [ApprovedById]    INT            NULL,
    [CancelReasonId]  INT            NULL,
    CONSTRAINT [PK_Drive] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Drive_AppointmentId] FOREIGN KEY ([AppointmentId]) REFERENCES [dbo].[Appointment] ([Id]),
    CONSTRAINT [FK_Drive_ApprovedById] FOREIGN KEY ([ApprovedById]) REFERENCES [dbo].[Volunteer] ([Id]),
    CONSTRAINT [FK_Drive_CancelReasonId] FOREIGN KEY ([CancelReasonId]) REFERENCES [dbo].[DriveCancelReason] ([Id]),
    CONSTRAINT [FK_Drive_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [dbo].[Volunteer] ([Id]),
    CONSTRAINT [FK_Drive_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[DriveStatus] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [FK_Drive_AppointmentId_idx]
    ON [dbo].[Drive]([AppointmentId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Drive_ApprovedById_idx]
    ON [dbo].[Drive]([ApprovedById] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Drive_CancelReasonId_idx]
    ON [dbo].[Drive]([CancelReasonId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Drive_DriverId_idx]
    ON [dbo].[Drive]([DriverId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Drive_StatusId_idx]
    ON [dbo].[Drive]([StatusId] ASC);

