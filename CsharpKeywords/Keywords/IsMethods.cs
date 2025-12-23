using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpKeywords.Keywords
{
    public class IsMethods
    {
        public string CheckValueTypeFirst(object obj)
        {
            var res = string.Empty;
            if (obj is string)
                res = "The object is a string";

            return res;
        }

        public string CheckValueTypeSecond(object obj)
        {
            var res = string.Empty;
            if (obj is string s)
                res = $"string type:{s}";

            return res;
        }
    }
}
