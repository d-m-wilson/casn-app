CREATE TABLE [dbo].[FundingOfferItem]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[FundingOfferId] INT NOT NULL,
	[FundingSourceId] INT NOT NULL,
	[FundingTypeId] INT NULL,
	[NeedAmount] DECIMAL(10,2) NULL,
	[NeedAmountNullReasonId] INT NULL,
	[FundingAmount] DECIMAL(10,2) NULL,
	[FundingAmountNullReasonId] INT NULL,
	[PaymentMethodId] INT NULL,
	[GrantId] INT NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_FundingOfferItem_IsActive] DEFAULT (0x01),
    [Created] DATETIME NOT NULL CONSTRAINT [DF_FundingOfferItem_Created] DEFAULT (GETUTCDATE()),
    [Updated] DATETIME NULL,
	CONSTRAINT [FK_FundingOfferItem_FundingOffer] FOREIGN KEY ([FundingOfferId]) REFERENCES [dbo].[FundingOffer]([Id]),
	CONSTRAINT [FK_FundingOfferItem_FundingSource] FOREIGN KEY ([FundingSourceId]) REFERENCES [dbo].[FundingSource]([Id]),
	CONSTRAINT [FK_FundingOfferItem_FundingType] FOREIGN KEY ([FundingTypeId]) REFERENCES [dbo].[FundingType]([Id]),
	CONSTRAINT [FK_FundingOfferItem_PaymentMethod] FOREIGN KEY ([PaymentMethodId]) REFERENCES [dbo].[PaymentMethod]([Id]),
	CONSTRAINT [FK_FundingOfferItem_NeedAmountNullReason] FOREIGN KEY ([NeedAmountNullReasonId]) REFERENCES [dbo].[NullReason]([Id]),
	CONSTRAINT [CK_FundingOfferItem_NeedAmountOrNullReason] CHECK ([NeedAmount] IS NOT NULL OR [NeedAmountNullReasonId] IS NOT NULL),
	CONSTRAINT [FK_FundingOfferItem_FundingAmountNullReason] FOREIGN KEY ([FundingAmountNullReasonId]) REFERENCES [dbo].[NullReason]([Id]),
	CONSTRAINT [CK_FundingOfferItem_FundingAmountOrNullReason] CHECK ([FundingAmount] IS NOT NULL OR [FundingAmountNullReasonId] IS NOT NULL),
	CONSTRAINT [FK_FundingOfferItem_Grant] FOREIGN KEY ([GrantId]) REFERENCES [dbo].[Grant]([Id]),
	CONSTRAINT [PK_FundingOfferItem] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)
