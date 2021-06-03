CREATE TABLE [dbo].[Appointment] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [DispatcherId]          INT            NOT NULL,
    [CallerId]              INT            NULL,
    [ServiceProviderId]     INT            NOT NULL,
    [PickupLocationVague]   NVARCHAR (255) NULL,
    [PickupVagueLatitude]   DECIMAL (9, 6) NULL,
    [PickupVagueLongitude]  DECIMAL (9, 6) NULL,
    [PickupVagueGeocoded]   DATETIME       NULL,
    [DropoffLocationVague]  NVARCHAR (255) NULL,
    [DropoffVagueLatitude]  DECIMAL (9, 6) NULL,
    [DropoffVagueLongitude] DECIMAL (9, 6) NULL,
    [DropoffVagueGeocoded]  DATETIME       NULL,
    [AppointmentDate]       DATETIME       NOT NULL,
    [AppointmentTypeId]     INT            NOT NULL,
    [IsActive]              BIT            CONSTRAINT [DF_Appointment_IsActive] DEFAULT (0x01) NOT NULL,
	[Tier1MessageCount]      INT            NULL,
    [Tier1MessageDate]      DATETIME       NULL,
	[Tier2MessageCount]		INT			   NULL,
    [Tier2MessageDate]      DATETIME       NULL,
	[Tier3MessageCount]      INT	           NULL,
    [Tier3MessageDate]      DATETIME       NULL,
	[BroadcastMessageCount]	INT			   NULL,
    [BroadcastMessageDate]  DATETIME       NULL,
    [Created]               DATETIME       CONSTRAINT [DF_Appointment_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]               DATETIME       NULL,
    [IsRedacted]            BIT            CONSTRAINT [DF_Appointment_IsRedacted] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Appointment_AppointmentTypeId] FOREIGN KEY ([AppointmentTypeId]) REFERENCES [dbo].[AppointmentType] ([Id]),
    CONSTRAINT [FK_Appointment_CallerId] FOREIGN KEY ([CallerId]) REFERENCES [dbo].[Caller] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_Appointment_ServiceProviderId] FOREIGN KEY ([ServiceProviderId]) REFERENCES [dbo].[ServiceProvider] ([Id]),
    CONSTRAINT [FK_Appointment_DispatcherId] FOREIGN KEY ([DispatcherId]) REFERENCES [dbo].[Volunteer] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [FK_Appointment_AppointmentTypeId_idx]
    ON [dbo].[Appointment]([AppointmentTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Appointment_CallerId_idx]
    ON [dbo].[Appointment]([CallerId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Appointment_ServiceProviderId_idx]
    ON [dbo].[Appointment]([ServiceProviderId] ASC);


GO
CREATE NONCLUSTERED INDEX [FK_Appointment_DispatcherId_idx]
    ON [dbo].[Appointment]([DispatcherId] ASC);

