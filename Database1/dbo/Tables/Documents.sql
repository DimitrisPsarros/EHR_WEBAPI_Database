CREATE TABLE [dbo].[Documents] (
    [DocumentsID]     INT             IDENTITY (1000, 1) NOT NULL,
    [PersonID]        INT             NULL,
    [file]            VARBINARY (MAX) NULL,
    [Document_TypeID] INT             NULL,
    [Date]            NTEXT           NULL,
    CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED ([DocumentsID] ASC),
    CONSTRAINT [FK_Documents_Demographics] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Demographics] ([PERSONID]),
    CONSTRAINT [FK_Documents_DocType] FOREIGN KEY ([Document_TypeID]) REFERENCES [dbo].[DocType] ([Document_typeID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Documents]
    ON [dbo].[Documents]([Document_TypeID] ASC);

