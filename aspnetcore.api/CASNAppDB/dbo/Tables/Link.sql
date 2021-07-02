CREATE TABLE [dbo].[Link]
(
    [Id] INT NOT NULL IDENTITY(1,1),
    [Title] NVARCHAR(200) NOT NULL,
    [Url] NVARCHAR(1000) NOT NULL,
    [Target] NVARCHAR(20) NOT NULL,
    [DisplayOrdinal] INT NOT NULL,
    [IsActive]    BIT            CONSTRAINT [DF_Link_IsActive] DEFAULT (1) NOT NULL,
    [Created]     DATETIME       CONSTRAINT [DF_Link_Created] DEFAULT (GETUTCDATE()) NOT NULL,
    [Updated]     DATETIME       NULL,
    CONSTRAINT [PK_Link] PRIMARY KEY CLUSTERED ([Id] ASC)
)
