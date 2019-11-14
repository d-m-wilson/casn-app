CREATE TABLE [dbo].[DriveCancelReason] (
    [Id]          INT            NOT NULL,
    [Name]        NVARCHAR (45)  NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [IsActive]    BIT            CONSTRAINT [DF_DriveCancelReason_IsActive] DEFAULT (0x01) NOT NULL,
    [Created]     DATETIME       CONSTRAINT [DF_DriveCancelReason_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]     DATETIME       NULL,
    CONSTRAINT [PK_DriveCancelReason] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_DriveCancelReason_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

