CREATE TABLE [dbo].[FundingSource]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(50) NOT NULL,
	[IsExternal] BIT NOT NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_FundingSource_IsActive] DEFAULT (0x01),
    [Created] DATETIME NOT NULL CONSTRAINT [DF_FundingSource_Created] DEFAULT (GETUTCDATE()),
    [Updated] DATETIME NULL,
	CONSTRAINT [PK_FundingSource] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)
