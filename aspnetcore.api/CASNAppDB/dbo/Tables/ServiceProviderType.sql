CREATE TABLE [dbo].[ServiceProviderType]
(
    [Id]        INT          NOT NULL,
    [Name]      NVARCHAR(45) NOT NULL,
    [IsActive]  BIT          NOT NULL CONSTRAINT [DF_ServiceProviderType_IsActive] DEFAULT (0x01),
    [Created]   DATETIME     NOT NULL CONSTRAINT [DF_ServiceProviderType_Created] DEFAULT (GETUTCDATE()),
    [Updated]   DATETIME     NULL,
    CONSTRAINT [PK_ServiceProviderType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_ServiceProviderType_Name] UNIQUE NONCLUSTERED ([Name] ASC)
)
