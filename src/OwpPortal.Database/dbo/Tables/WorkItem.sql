CREATE TABLE [dbo].[WorkItem] (
    [WorkItemId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Description]        NVARCHAR (MAX) NULL,
    [WorkItemType]       INT            NOT NULL,
    [CreatedOn]          DATETIME2 (7)  CONSTRAINT [DF_WorkItem_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [LastChangedOn]      DATETIME2 (7)  CONSTRAINT [DF_WorkItem_LastChangedOn] DEFAULT (getdate()) NOT NULL,
    [Status]             INT            NOT NULL,
    [AssignmentId]		 NVARCHAR (MAX) NULL,
    [Address]            NVARCHAR (MAX) NULL,
    [Latitude] DECIMAL(30, 14) NULL, 
    [Longitude] DECIMAL(30, 14) NULL, 
    [Image] IMAGE NULL, 
    CONSTRAINT [PK_WorkItem] PRIMARY KEY CLUSTERED ([WorkItemId] ASC)
);

GO

CREATE TRIGGER tgr_modlastchanged
ON WorkItem
AFTER UPDATE AS
  UPDATE WorkItem
  SET LastChangedOn = GETDATE()
  WHERE WorkItemId IN (SELECT WorkItemId FROM Inserted)