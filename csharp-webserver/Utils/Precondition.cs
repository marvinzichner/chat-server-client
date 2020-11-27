using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csharp_webserver
{
    public class Precondition
    {

        public static string setDefaultIfStringIsNullOrEmpty(string s, string def)
        {
            if (string.IsNullOrEmpty(s)) return def;
            return s;
        }

        public static void notEqualsOrAbort(int i, int j)
        {
            if(i == j)
                throw new PreconditionException(
                    $"Precondition asserted false: i={i.ToString()} j={j.ToString()}");
        }
    }
}