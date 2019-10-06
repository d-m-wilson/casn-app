CREATE TABLE [dbo].[Badge] (
    [Id]          INT            NOT NULL,
    [Title]       NVARCHAR (150) NOT NULL,
    [Description] NVARCHAR (500) NULL,
    [Path]        NVARCHAR (100) NOT NULL,
    [TriggerType] INT            NOT NULL,
    [ProcedureName] NVARCHAR(100) NOT NULL,
    [ServiceProviderId]    INT NULL,
    [CountTarget] INT NULL,
    [IsActive]    BIT            CONSTRAINT [DF_Badge_IsActive] DEFAULT (0x01) NOT NULL,
    [IsHidden]    BIT            CONSTRAINT [DF_Badge_IsHidden] DEFAULT (0x00) NOT NULL,
    [Created]     DATETIME       CONSTRAINT [DF_Badge_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]     DATETIME       NULL,
    CONSTRAINT [PK_Badge] PRIMARY KEY CLUSTERED ([Id] ASC)
);

