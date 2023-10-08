﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyUrl.Common.Interfaces
{
    public interface IResult<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
    }
}
