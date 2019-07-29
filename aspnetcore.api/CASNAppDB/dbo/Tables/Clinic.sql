CREATE TABLE [dbo].[Clinic] (
    [Id]            INT            IDENTITY (5, 1) NOT NULL,
    [CiviContactId] INT            NOT NULL,
    [Name]          NVARCHAR (100) NOT NULL,
    [Phone]         NVARCHAR (20)  NOT NULL,
    [Address]       NVARCHAR (100) NOT NULL,
    [City]          NVARCHAR (50)  NOT NULL,
    [State]         NVARCHAR (30)  NOT NULL,
    [PostalCode]    NVARCHAR (10)  NOT NULL,
    [IsActive]      BIT            CONSTRAINT [DF_Clinic_IsActive] DEFAULT (0x01) NOT NULL,
    [Created]       DATETIME       CONSTRAINT [DF_Clinic_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]       DATETIME       NULL,
    [Latitude]      DECIMAL (9, 6) NULL,
    [Longitude]     DECIMAL (9, 6) NULL,
    [Geocoded]      DATETIME       NULL,
    CONSTRAINT [PK_Clinic] PRIMARY KEY CLUSTERED ([Id] ASC)
);

