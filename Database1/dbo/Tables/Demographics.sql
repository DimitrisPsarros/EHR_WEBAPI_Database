CREATE TABLE [dbo].[Demographics] (
    [PERSONID]     INT           IDENTITY (1000, 1) NOT NULL,
    [FirstName]    NVARCHAR (50) NOT NULL,
    [LastName]     NVARCHAR (50) NOT NULL,
    [Sex]          NVARCHAR (50) NOT NULL,
    [Country]      NVARCHAR (50) NOT NULL,
    [City]         NVARCHAR (50) NOT NULL,
    [StreetName]   NVARCHAR (50) NOT NULL,
    [StreetNumber] INT           NOT NULL,
    [Birthday]     TEXT          NOT NULL,
    CONSTRAINT [PK_Demographics_1] PRIMARY KEY CLUSTERED ([PERSONID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Demographics]
    ON [dbo].[Demographics]([PERSONID] ASC);

