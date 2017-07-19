CREATE TABLE [dbo].[Users] (
    [UserID]   INT          IDENTITY (1000, 1) NOT NULL,
    [PersonID] INT          NULL,
    [UserName] VARCHAR (50) NOT NULL,
    [Password] VARCHAR (50) NOT NULL,
    [IsDoctor] BIT          NOT NULL,
    [Key]      VARCHAR (50) NULL,
    [Salt]     VARCHAR (50) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Users_Demographics] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Demographics] ([PERSONID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users]
    ON [dbo].[Users]([PersonID] ASC);

