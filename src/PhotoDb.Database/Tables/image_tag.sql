create table [dbo].[image_tag]
(
	[image_id]          bigint      not null,
    [tag_id]            int         not null,
    [placeholder]       smallint    null,
    constraint [pk_image_tag] primary key nonclustered ([image_id], [tag_id]), 
    constraint [fk_image_tag_image] foreign key ([image_id]) references [dbo].[image]([id]),
    constraint [fk_image_tag_tag] foreign key ([tag_id]) references [dbo].[tag_item]([id]),
)
