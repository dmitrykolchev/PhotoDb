using System;
using System.Collections.Generic;

namespace PhotoDb.Data.Models;

public partial class AlbumImage
{
    public int AlbumId { get; set; }

    public long ImageId { get; set; }

    public short? Placeholder { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual Image Image { get; set; } = null!;
}
