﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class RowValue2
    {
        public ColumnSchema Column { get; set; }
        public string Value { get; set; }

        public static IComparer<RowValue2> SortByBinaryLayout()
        {
            return (IComparer<RowValue2>) new RowValueSorter();
        }
    }
}
