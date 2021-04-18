CREATE TABLE [dbo].[Volunteer] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CiviContactId]  INT            NOT NULL,
    [FirstName]      NVARCHAR (50)  NOT NULL,
    [LastName]       NVARCHAR (50)  NOT NULL,
    [MobilePhone]    NVARCHAR (20)  NOT NULL,
    [GoogleAccount]  NVARCHAR (100) NOT NULL,
    [IsDriver]       BIT            NOT NULL,
    [IsDispatcher]   BIT            NOT NULL,
    [HasTextConsent] BIT            CONSTRAINT [DF_Volunteer_HasTextConsent] DEFAULT (0x01) NOT NULL,
    [Address]        NVARCHAR (100) NULL,
    [City]           NVARCHAR (50)  NULL,
    [State]          NVARCHAR (30)  NULL,
    [PostalCode]     NVARCHAR (10)  NULL,
    [Latitude]       DECIMAL (9, 6) NULL,
    [Longitude]      DECIMAL (9, 6) NULL,
    [Geocoded]       DATETIME       NULL,
    [IsActive]       BIT            CONSTRAINT [DF_Volunteer_IsActive] DEFAULT (0x01) NOT NULL,
    [Created]        DATETIME       CONSTRAINT [DF_Volunteer_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]        DATETIME       NULL,
    CONSTRAINT [PK_Volunteer] PRIMARY KEY CLUSTERED ([Id] ASC)
);

