CREATE VIEW [dbo].[WorkItemReportView]
	AS SELECT  
	[WorkItemId] as [ID],
    [Description],
    Case [WorkItemType]
		when 0 then 'unspecified'
		when 1 then 'asphalt maintenance'
		when 2 then 'paving'
		when 3 then 'bridge repair'
		when 4 then 'street sweeping'
		when 5 then 'traffic lights'
		when 6 then 'paint striping'
		when 7 then 'road inspections'
		when 8 then 'stormwater management'
		else
			'unknown'
	end as [Type],
    [CreatedOn],
    [LastChangedOn],
     Case [Status]
		when 0 then 'New'
		when 1 then 'Assigned'
		when 2 then 'Re-assigned'
		when 3 then 'Done'
		else
			'unknown'
		end as [Status],
    [AssignmentId],
    [Address],
    [Latitude], 
    [Longitude], 
    [Image], 
	Case [WorkItemPriority]
		when 0 then 'Low'
		when 1 then 'Normal'
		when 2 then 'High'
		when 3 then 'Critical'
		else
			'unknown'
	end as [WorkItemPriority]
	FROM [WorkItem]