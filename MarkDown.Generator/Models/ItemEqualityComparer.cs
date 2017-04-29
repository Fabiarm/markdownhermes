using System;
using System.Collections.Generic;

namespace MarkDown.Generator.Models
{
    internal class ItemEqualityComparer<T1, T2> : EqualityComparer<Tuple<T1, T2>>
    {
        public override bool Equals(Tuple<T1, T2> x, Tuple<T1, T2> y)
        {
            return x.Item1.Equals(y.Item1);
        }

        public override int GetHashCode(Tuple<T1, T2> obj)
        {
            return obj.Item1.GetHashCode();
        }
    }
}