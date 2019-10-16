CREATE TABLE [dbo].[WorkItem] (
    [WorkItemId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Description]        NVARCHAR (MAX) NULL,
    [WorkItemType]       INT            NOT NULL,
    [CreatedOn]          DATETIME2 (7)  CONSTRAINT [DF_WorkItem_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [LastChangedOn]      DATETIME2 (7)  CONSTRAINT [DF_WorkItem_LastChangedOn] DEFAULT (getdate()) NOT NULL,
    [Status]             INT            NOT NULL,
    [AssignedToWorkerId] BIGINT         NULL,
    [Address]            NVARCHAR (MAX) NULL,
    [AssignedToEmail]    NVARCHAR (255) NULL,
    CONSTRAINT [PK_WorkItem] PRIMARY KEY CLUSTERED ([WorkItemId] ASC),
    CONSTRAINT [FK_WorkItem_Worker_AssignedToWorkerId] FOREIGN KEY ([AssignedToWorkerId]) REFERENCES [dbo].[Worker] ([WorkerId])
);


GO
CREATE NONCLUSTERED INDEX [IX_WorkItem_AssignedToWorkerId]
    ON [dbo].[WorkItem]([AssignedToWorkerId] ASC);


GO


CREATE TRIGGER tgr_modlastchanged
ON WorkItem
AFTER UPDATE AS
  UPDATE WorkItem
  SET LastChangedOn = GETDATE()
  WHERE WorkItemId IN (SELECT WorkItemId FROM Inserted)