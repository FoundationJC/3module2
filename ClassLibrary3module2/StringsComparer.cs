using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3module2
{
    public class StringsComparer : IComparer<Strings>
    {
        // Порівнюємо за довжиною рядка
        public int Compare(Strings x, Strings y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Length.CompareTo(y.Length);
        }
    }
}
