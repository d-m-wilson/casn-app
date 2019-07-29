CREATE TABLE [dbo].[AppointmentType] (
    [Id]                       INT            NOT NULL,
    [Name]                     NVARCHAR (64)  NOT NULL,
    [Title]                    NVARCHAR (64)  NOT NULL,
    [Description]              NVARCHAR (255) NULL,
    [EstimatedDurationMinutes] INT            CONSTRAINT [DF_AppointmentType_EstimatedDurationMinutes] DEFAULT ((60)) NOT NULL,
    [IsActive]                 BIT            CONSTRAINT [DF_AppointmentType_IsActive] DEFAULT (0x01) NOT NULL,
    [Created]                  DATETIME       CONSTRAINT [DF_AppointmentType_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]                  DATETIME       NULL,
    CONSTRAINT [PK_AppointmentType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_AppointmentType_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

