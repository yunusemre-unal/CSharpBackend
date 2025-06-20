﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data) : base(data, false)
        {
        }

        public ErrorDataResult(T data, string message) : base(data, message, false)
        {
        }

        public ErrorDataResult() : base(default, false)
        {
        }

        public ErrorDataResult(string message) : base(default, message, false)
        {
        }
    }
}
