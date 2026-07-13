// <copyright file="DataServiceBase.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoDb.Data.Services;

public abstract class DataServiceBase
{
    private readonly PhotoDbContextBase _context;

    protected DataServiceBase(PhotoDbContextBase context)
    {
        _context = context;
    }

    public PhotoDbContextBase Context => _context;
}
