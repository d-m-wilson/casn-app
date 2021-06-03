CREATE TABLE [dbo].[ServiceProvider] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [CiviContactId] INT            NULL,
    [ServiceProviderTypeId] INT    NOT NULL,
    [Name]          NVARCHAR (100) NOT NULL,
    [Phone]         NVARCHAR (20)  NOT NULL,
    [Address]       NVARCHAR (100) NOT NULL,
    [City]          NVARCHAR (50)  NOT NULL,
    [State]         NVARCHAR (30)  NOT NULL,
    [PostalCode]    NVARCHAR (10)  NOT NULL,
    [IsActive]      BIT            CONSTRAINT [DF_ServiceProvider_IsActive] DEFAULT (0x01) NOT NULL,
    [Created]       DATETIME       CONSTRAINT [DF_ServiceProvider_Created] DEFAULT (GETUTCDATE()) NOT NULL,
    [Updated]       DATETIME       NULL,
    [Latitude]      DECIMAL (9, 6) NULL,
    [Longitude]     DECIMAL (9, 6) NULL,
    [Geocoded]      DATETIME       NULL,
    CONSTRAINT [PK_ServiceProvider] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceProvider_ServiceProviderTypeId] FOREIGN KEY ([ServiceProviderTypeId]) REFERENCES [dbo].[ServiceProviderType] ([Id]),
);


GO
CREATE NONCLUSTERED INDEX [FK_ServiceProvider_ServiceProviderTypeId_idx]
    ON [dbo].[ServiceProvider]([ServiceProviderTypeId] ASC);

