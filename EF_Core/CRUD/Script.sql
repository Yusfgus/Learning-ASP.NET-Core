CREATE TABLE [Wallets] (
    [Id] int NOT NULL IDENTITY,
    [Holder] nvarchar(max) NULL,
    [Balance] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Wallets] PRIMARY KEY ([Id])
);
GO


