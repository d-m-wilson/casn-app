CREATE TABLE [dbo].[DriveLogStatus] (
    [Id]       INT           NOT NULL,
    [Name]     NVARCHAR (45) NOT NULL,
    [IsActive] BIT           CONSTRAINT [DF_DriveLogStatus_IsActive] DEFAULT (0x01) NOT NULL,
    [Created]  DATETIME      CONSTRAINT [DF_DriveLogStatus_Created] DEFAULT (GETUTCDATE()) NOT NULL,
    [Updated]  DATETIME      NULL,
    CONSTRAINT [PK_DriveLogStatus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_DriveLogStatus_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

