create table [dbo].[tag_item]
(
    [id]                int             not null default(next value for [dbo].[tag_sequence]),
    [state]             smallint        not null,
    [name]              nvarchar(256)   not null,
    [created]           datetime2       not null,
    constraint [pk_tag_item] primary key ([id]), 
)
