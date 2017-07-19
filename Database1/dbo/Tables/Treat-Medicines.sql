CREATE TABLE [dbo].[Treat-Medicines] (
    [TreatmentID] INT        IDENTITY (1000, 1) NOT NULL,
    [VisitID]     INT        NULL,
    [ATC-CODES]   NCHAR (10) NULL,
    [Dosage]      TEXT       NULL,
    CONSTRAINT [PK_Treat-Medicines] PRIMARY KEY CLUSTERED ([TreatmentID] ASC),
    CONSTRAINT [FK_Treat-Medicines_Visit] FOREIGN KEY ([VisitID]) REFERENCES [dbo].[Visit] ([VisitID])
);

