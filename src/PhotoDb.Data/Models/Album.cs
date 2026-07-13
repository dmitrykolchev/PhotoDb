using System;
using System.Collections.Generic;

namespace PhotoDb.Data.Models;

public partial class Album
{
    public int Id { get; set; }

    public short State { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public virtual ICollection<AlbumImage> AlbumImage { get; set; } = new List<AlbumImage>();
}
