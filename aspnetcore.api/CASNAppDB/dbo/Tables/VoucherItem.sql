CREATE TABLE [dbo].[VoucherItem]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[VoucherId] INT NOT NULL,
	[FundingSourceId] INT NOT NULL,
	[FundingTypeId] INT NULL,
	[NeedAmount] DECIMAL(10,2) NULL,
	[NeedAmountNullReasonId] INT NULL,
	[FundingAmount] DECIMAL(10,2) NULL,
	[FundingAmountNullReasonId] INT NULL,
	[PaymentMethodId] INT NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_VoucherItem_IsActive] DEFAULT (0x01),
    [Created] DATETIME NOT NULL CONSTRAINT [DF_VoucherItem_Created] DEFAULT (GETUTCDATE()),
    [Updated] DATETIME NULL,
	CONSTRAINT [FK_VoucherItem_Voucher] FOREIGN KEY ([VoucherId]) REFERENCES [dbo].[Voucher]([Id]),
	CONSTRAINT [FK_VoucherItem_FundingSource] FOREIGN KEY ([FundingSourceId]) REFERENCES [dbo].[FundingSource]([Id]),
	CONSTRAINT [FK_VoucherItem_FundingType] FOREIGN KEY ([FundingTypeId]) REFERENCES [dbo].[FundingType]([Id]),
	CONSTRAINT [FK_VoucherItem_PaymentMethod] FOREIGN KEY ([PaymentMethodId]) REFERENCES [dbo].[PaymentMethod]([Id]),
	CONSTRAINT [FK_VoucherItem_NeedAmountNullReason] FOREIGN KEY ([NeedAmountNullReasonId]) REFERENCES [dbo].[NullReason]([Id]),
	CONSTRAINT [CK_VoucherItem_NeedAmountOrNullReason] CHECK ([NeedAmount] IS NOT NULL OR [NeedAmountNullReasonId] IS NOT NULL),
	CONSTRAINT [FK_VoucherItem_FundingAmountNullReason] FOREIGN KEY ([FundingAmountNullReasonId]) REFERENCES [dbo].[NullReason]([Id]),
	CONSTRAINT [CK_VoucherItem_FundingAmountOrNullReason] CHECK ([FundingAmount] IS NOT NULL OR [FundingAmountNullReasonId] IS NOT NULL),
	CONSTRAINT [PK_VoucherItem] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)
