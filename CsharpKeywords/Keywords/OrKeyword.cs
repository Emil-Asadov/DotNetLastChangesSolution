using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpKeywords.Keywords
{
    public class OrKeyword
    {
        public string CheckIntDouble(object obj)
        {
            var res=string.Empty;
            if (obj is int or double)
                res = "The object is an int or double";

            return res;
        }

        public string CheckStringOrNull(object obj)
        {
            //This check introduced in C# 9.0
            var res = string.Empty;
            //if (obj is string s or null)
            //    res = $"String or null:{s}";

            return res;
        }
    }
}
