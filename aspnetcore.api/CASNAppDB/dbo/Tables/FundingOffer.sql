CREATE TABLE [dbo].[FundingOffer]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[CallerId] INT NOT NULL,
	[FundingOfferStatusId] INT NOT NULL,
	[ClinicId] INT NOT NULL,
	[CreatedById] INT NOT NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_FundingOffer_IsActive] DEFAULT (0x01),
    [Created] DATETIME NOT NULL CONSTRAINT [DF_FundingOffer_Created] DEFAULT (GETUTCDATE()),
	[Issued] DATETIME NULL,
	[Redeemed] DATETIME NULL,
	[Paid] DATETIME NULL,
    [Updated] DATETIME NULL,
	CONSTRAINT [FK_FundingOffer_FundingOfferStatus] FOREIGN KEY ([FundingOfferStatusId]) REFERENCES [dbo].[FundingOfferStatus]([Id]),
	CONSTRAINT [FK_FundingOffer_Caller] FOREIGN KEY ([CallerId]) REFERENCES [dbo].[Caller]([Id]),
	CONSTRAINT [FK_FundingOffer_ServiceProvider] FOREIGN KEY ([ClinicId]) REFERENCES [dbo].[ServiceProvider]([Id]),
	CONSTRAINT [FK_FundingOffer_Volunteer] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Volunteer]([Id]),
	CONSTRAINT [PK_FundingOffer] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)
