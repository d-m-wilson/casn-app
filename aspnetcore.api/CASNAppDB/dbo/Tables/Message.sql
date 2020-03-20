CREATE TABLE [dbo].[Message] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [MessageTypeId] INT          NOT NULL,
    [MessageText] NVARCHAR (250) NOT NULL,
    [IsActive]    BIT            CONSTRAINT [DF_Message_IsActive] DEFAULT ((1)) NOT NULL,
    [Created]     DATETIME       CONSTRAINT [DF_Message_Created] DEFAULT (getutcdate()) NOT NULL,
    [Updated]     DATETIME       NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Message_MessageTypeId] FOREIGN KEY ([MessageTypeId]) REFERENCES [dbo].[MessageType] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [FK_Message_MessageTypeId_idx]
    ON [dbo].[Message]([MessageTypeId] ASC);
