using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpKeywords.Keywords
{
    public class NotKeyword
    {
        public string CheckValueType(object obj)
        {
            var res = string.Empty;
            if (obj is not string)
                res = "The object is not a string";

            return res;
        }
    }
}
