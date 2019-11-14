CREATE TABLE [dbo].[Caller] (
    [Id]                     INT            IDENTITY (1005, 1) NOT NULL,
    [CiviContactId]          INT            NOT NULL,
    [CallerIdentifier]       NVARCHAR (45)  NOT NULL,
    [FirstName]              NVARCHAR (50)  NOT NULL,
    [LastName]               NVARCHAR (50)  NULL,
    [Phone]                  NVARCHAR (20)  NOT NULL,
    [IsMinor]                BIT            NOT NULL,
    [PreferredLanguage]      NVARCHAR (25)  NOT NULL,
    [PreferredContactMethod] SMALLINT       NOT NULL,
    [Note]                   NVARCHAR (500) NULL,
    [IsActive]               BIT            CONSTRAINT [DF_Caller_IsActive] DEFAULT (0x01) NOT NULL,
    [Created]                DATETIME       CONSTRAINT [DF_Caller_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]                DATETIME       NULL,
    CONSTRAINT [PK_Caller] PRIMARY KEY CLUSTERED ([Id] ASC)
);

