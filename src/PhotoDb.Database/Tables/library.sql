create table [dbo].[library]
(
    [id]                int             not null default(next value for [dbo].[library_sequence]),
    [state]             smallint        not null,
    [name]              nvarchar(256)   not null,
    [path]              nvarchar(1024)  not null,
    [last_scan_date]    datetime2       not null,
    [description]          nvarchar(max)   null,
    [created]           datetime2       not null,
    [modified]          datetime2       not null, 
    constraint [pk_library] primary key ([id])
)
