CREATE TABLE [dbo].[MessageType]
(
    [Id]        INT          NOT NULL,
    [Name]      NVARCHAR(45) NOT NULL,
    [IsActive]  BIT          NOT NULL CONSTRAINT [DF_MessageType_IsActive] DEFAULT (0x01),
    [Created]   DATETIME     NOT NULL CONSTRAINT [DF_MessageType_Created] DEFAULT (GETUTCDATE()),
    [Updated]   DATETIME     NULL,
    CONSTRAINT [PK_MessageType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_MessageType_Name] UNIQUE NONCLUSTERED ([Name] ASC)
)
