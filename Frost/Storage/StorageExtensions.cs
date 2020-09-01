using C5;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FrostDB
{
    public static class StorageExtensions
    {
        public static TreeDictionary<int, Page> DeepCopy(this TreeDictionary<int, Page> item)
        {
            var result = new TreeDictionary<int, Page>();
            item.ForEach(i => result.Add(i.Key, i.Value));
            return result;
        }
    }
}
