create table [dbo].[album_image]
(
    [album_id]              int         not null,
    [image_id]              bigint      not null,
    [placeholder]           smallint    null,
    constraint [pk_album_image] primary key nonclustered ([album_id], [image_id]), 
    constraint [fk_album_image_album] foreign key ([album_id]) references [dbo].[album]([id]),
    constraint [fk_album_image_image] foreign key ([image_id]) references [dbo].[image]([id]),
)
