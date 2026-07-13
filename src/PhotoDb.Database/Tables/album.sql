CREATE TABLE [dbo].[album]
(
	[id]            int             not null default(next value for [dbo].[album_sequence]),
    [state]         smallint        not null,
    [name]          nvarchar(256)   not null,
    [description]      nvarchar(max)   null,
    [created]       datetime2       not null,
    [modified]      datetime2       not null, 
    constraint [pk_album] primary key nonclustered ([id])
)
