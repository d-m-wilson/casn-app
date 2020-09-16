CREATE TABLE [dbo].[VoucherStatus]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(50) NOT NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_VoucherStatus_IsActive] DEFAULT (0x01),
    [Created] DATETIME NOT NULL CONSTRAINT [DF_VoucherStatus_Created] DEFAULT (GETUTCDATE()),
    [Updated] DATETIME NULL,
	CONSTRAINT [PK_VoucherStatus] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)
