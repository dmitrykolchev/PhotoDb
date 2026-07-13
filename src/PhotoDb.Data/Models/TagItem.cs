using System;
using System.Collections.Generic;

namespace PhotoDb.Data.Models;

public partial class TagItem
{
    public int Id { get; set; }

    public short State { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual ICollection<ImageTag> ImageTag { get; set; } = new List<ImageTag>();
}
