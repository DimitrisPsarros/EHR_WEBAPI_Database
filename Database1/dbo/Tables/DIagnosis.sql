CREATE TABLE [dbo].[DIagnosis] (
    [DiagnosisID] INT           IDENTITY (1000, 1) NOT NULL,
    [VisitID]     INT           NULL,
    [Description] NVARCHAR (50) NULL,
    [ICD-CODE]    NCHAR (10)    NULL,
    CONSTRAINT [PK_DIagnosis1] PRIMARY KEY CLUSTERED ([DiagnosisID] ASC),
    CONSTRAINT [FK_DIagnosis_Visit] FOREIGN KEY ([VisitID]) REFERENCES [dbo].[Visit] ([VisitID])
);

