using System;
using System.Collections.Generic;

namespace PhotoDb.Data.Models;

public partial class Library
{
    public int Id { get; set; }

    public short State { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public DateTime LastScanDate { get; set; }

    public string? Description { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public virtual ICollection<Image> Image { get; set; } = new List<Image>();
}
