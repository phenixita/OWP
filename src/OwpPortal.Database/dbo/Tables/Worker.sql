CREATE TABLE [dbo].[Worker] (
    [WorkerId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FullName] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Worker] PRIMARY KEY CLUSTERED ([WorkerId] ASC)
);

