﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLib
{
    public interface ISave
    {
        void Save<T>(T obj);

    }
}
