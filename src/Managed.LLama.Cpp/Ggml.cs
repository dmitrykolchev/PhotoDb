// <copyright file="Ggml.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public static class Ggml
{
    public static void LoadAllBackends()
    {
        ggml_backend_load_all();
    }


}
