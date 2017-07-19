CREATE TABLE [dbo].[DataSender] (
    [DataSenderID] INT             IDENTITY (2000, 1) NOT NULL,
    [PersonID]     INT             NULL,
    [ReseiverID]   INT             NULL,
    [Text]         TEXT            NULL,
    [PictureInfo]  TEXT            NULL,
    [Picture]      VARBINARY (MAX) NULL,
    [Date]         NTEXT           NULL,
    CONSTRAINT [PK_DataSender] PRIMARY KEY CLUSTERED ([DataSenderID] ASC),
    CONSTRAINT [FK_DataSender_Demographics] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Demographics] ([PERSONID])
);

