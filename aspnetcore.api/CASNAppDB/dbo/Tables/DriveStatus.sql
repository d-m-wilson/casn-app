CREATE TABLE [dbo].[DriveStatus] (
    [Id]       INT           NOT NULL,
    [Name]     NVARCHAR (45) NOT NULL,
    [IsActive] BIT           CONSTRAINT [DF_DriveStatus_IsActive] DEFAULT (0x01) NOT NULL,
    [Created]  DATETIME      CONSTRAINT [DF_DriveStatus_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]  DATETIME      NULL,
    CONSTRAINT [PK_DriveStatus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_DriveStatus_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

