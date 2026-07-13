using System;
using System.Collections.Generic;

namespace PhotoDb.Data.Models;

public partial class ImageTag
{
    public long ImageId { get; set; }

    public int TagId { get; set; }

    public short? Placeholder { get; set; }

    public virtual Image Image { get; set; } = null!;

    public virtual TagItem Tag { get; set; } = null!;
}
