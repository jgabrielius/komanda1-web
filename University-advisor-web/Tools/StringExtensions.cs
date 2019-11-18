using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Tools
{
    public static class StringExtensions
    {
        public static int TryParse(this string input, int valueIfWrong)
        {
            if (Int32.TryParse(input, out int value))
            {
                return value;
            }
            return valueIfWrong;
        }

    }
}
