CREATE TABLE [dbo].[DocType] (
    [Document_typeID] INT  IDENTITY (1000, 1) NOT NULL,
    [Type]            TEXT NULL,
    CONSTRAINT [PK_DocType] PRIMARY KEY CLUSTERED ([Document_typeID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_DocType]
    ON [dbo].[DocType]([Document_typeID] ASC);

