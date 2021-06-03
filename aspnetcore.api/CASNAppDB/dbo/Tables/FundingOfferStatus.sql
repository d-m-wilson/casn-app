CREATE TABLE [dbo].[FundingOfferStatus]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(50) NOT NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_FundingOfferStatus_IsActive] DEFAULT (0x01),
    [Created] DATETIME NOT NULL CONSTRAINT [DF_FundingOfferStatus_Created] DEFAULT (GETUTCDATE()),
    [Updated] DATETIME NULL,
	CONSTRAINT [PK_FundingOfferStatus] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)
