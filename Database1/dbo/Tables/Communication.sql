CREATE TABLE [dbo].[Communication] (
    [CommunicationID] INT  IDENTITY (1000, 1) NOT NULL,
    [PersonID]        INT  NULL,
    [email]           TEXT NULL,
    [Telephone]       INT  NULL,
    CONSTRAINT [PK_Communication] PRIMARY KEY CLUSTERED ([CommunicationID] ASC),
    CONSTRAINT [FK_Communication_Demographics] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Demographics] ([PERSONID])
);

