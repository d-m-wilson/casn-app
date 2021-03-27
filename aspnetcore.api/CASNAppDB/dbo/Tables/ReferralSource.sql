CREATE TABLE [dbo].[ReferralSource]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(50) NOT NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_ReferralSource_IsActive] DEFAULT (1),
    [Created] DATETIME NOT NULL CONSTRAINT [DF_ReferralSource_Created] DEFAULT (GETUTCDATE()),
    [Updated] DATETIME NULL,
	CONSTRAINT [PK_ReferralSource] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)
