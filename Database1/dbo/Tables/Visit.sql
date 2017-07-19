CREATE TABLE [dbo].[Visit] (
    [VisitID]        INT   IDENTITY (100, 1) NOT NULL,
    [DoctorPersonID] INT   NOT NULL,
    [PersonID]       INT   NULL,
    [NumberOfVisit]  INT   NOT NULL,
    [Date]           NTEXT NULL,
    CONSTRAINT [PK_Visit] PRIMARY KEY CLUSTERED ([VisitID] ASC),
    CONSTRAINT [FK_Visit_Demographics] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Demographics] ([PERSONID])
);

