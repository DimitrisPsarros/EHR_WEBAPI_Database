CREATE TABLE [dbo].[Contacts] (
    [ContactsID] INT   IDENTITY (1000, 1) NOT NULL,
    [PersonId]   INT   NULL,
    [Contactid]  INT   NULL,
    [FirstName]  NTEXT NULL,
    [LastName]   NTEXT NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([ContactsID] ASC),
    CONSTRAINT [FK_Contacts_Demographics] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Demographics] ([PERSONID])
);

