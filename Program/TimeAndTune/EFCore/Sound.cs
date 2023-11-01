using System;
using System.Collections.Generic;

namespace EFCore;

public partial class Sound
{
    public int Soundid { get; set; }

    public string Soundname { get; set; } = null!;

    public string Audiofilepath { get; set; } = null!;

    public string Photofilepath { get; set; } = null!;
}
