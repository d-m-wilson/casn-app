CREATE TABLE [dbo].[Voucher]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[CallerId] INT NOT NULL,
	[VoucherStatusId] INT NOT NULL,
	[CallerStateOfResidenceId] INT NOT NULL,
	[ClinicId] INT NOT NULL,
	[CreatedById] INT NOT NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_Voucher_IsActive] DEFAULT (0x01),
    [Created] DATETIME NOT NULL CONSTRAINT [DF_Voucher_Created] DEFAULT (GETUTCDATE()),
	[Issued] DATETIME NULL,
	[Redeemed] DATETIME NULL,
	[Paid] DATETIME NULL,
    [Updated] DATETIME NULL,
	CONSTRAINT [FK_Voucher_VoucherStatus] FOREIGN KEY ([VoucherStatusId]) REFERENCES [dbo].[VoucherStatus]([Id]),
	CONSTRAINT [FK_Voucher_Caller] FOREIGN KEY ([CallerId]) REFERENCES [dbo].[Caller]([Id]),
	CONSTRAINT [FK_Voucher_ServiceProvider] FOREIGN KEY ([ClinicId]) REFERENCES [dbo].[ServiceProvider]([Id]),
	CONSTRAINT [FK_Voucher_Volunteer] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Volunteer]([Id]),
	CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)
