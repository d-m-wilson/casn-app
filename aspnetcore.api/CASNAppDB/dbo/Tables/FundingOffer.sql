CREATE TABLE [dbo].[FundingOffer]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[CallerId] INT NOT NULL,
	[FundingOfferStatusId] INT NOT NULL,
	[ClinicId] INT NOT NULL,
	[AppointmentTypeId] INT NOT NULL,
	[AppointmentDate] DATETIME NOT NULL,
	[CreatedById] INT NOT NULL,
	[Note] NVARCHAR(500) NULL,
    [FollowUpConsent]        BIT NOT NULL   CONSTRAINT [DF_FundingOffer_FollowUpConsent] DEFAULT (0),
    [DemographicSurveySent]  BIT NOT NULL   CONSTRAINT [DF_FundingOffer_DemographicSurveySent] DEFAULT (0),
	[ClinicReferenceNumber]  NVARCHAR(20) NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_FundingOffer_IsActive] DEFAULT (0x01),
    [Created] DATETIME NOT NULL CONSTRAINT [DF_FundingOffer_Created] DEFAULT (GETUTCDATE()),
	[Issued] DATETIME NULL,
	[Redeemed] DATETIME NULL,
	[Paid] DATETIME NULL,
    [Updated] DATETIME NULL,
    [Voided] DATETIME NULL,
	[IssuedById] INT NULL,
	[VoidedById] INT NULL,
	[UpdatedById] INT NULL,
	CONSTRAINT [FK_FundingOffer_FundingOfferStatus] FOREIGN KEY ([FundingOfferStatusId]) REFERENCES [dbo].[FundingOfferStatus]([Id]),
	CONSTRAINT [FK_FundingOffer_Caller] FOREIGN KEY ([CallerId]) REFERENCES [dbo].[Caller]([Id]),
	CONSTRAINT [FK_FundingOffer_ServiceProvider] FOREIGN KEY ([ClinicId]) REFERENCES [dbo].[ServiceProvider]([Id]),
	CONSTRAINT [FK_FundingOffer_AppointmentType] FOREIGN KEY ([AppointmentTypeId]) REFERENCES [dbo].[AppointmentType]([Id]),
	CONSTRAINT [FK_FundingOffer_Created_Volunteer] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Volunteer]([Id]),
	CONSTRAINT [FK_FundingOffer_Issued_Volunteer] FOREIGN KEY ([IssuedById]) REFERENCES [dbo].[Volunteer]([Id]),
	CONSTRAINT [FK_FundingOffer_Voided_Volunteer] FOREIGN KEY ([VoidedById]) REFERENCES [dbo].[Volunteer]([Id]),
	CONSTRAINT [FK_FundingOffer_Updated_Volunteer] FOREIGN KEY ([UpdatedById]) REFERENCES [dbo].[Volunteer]([Id]),
	CONSTRAINT [PK_FundingOffer] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)
