using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpKeywords.Keywords
{
    public class CombiningKeyword
    {
        public string CheckMultipleType(object obj)
        {
            var res = obj switch
            {
                int value when value is > 0 => "Positive integer",
                int value when value is < 0 => "Negative integer",
                string str when str is { Length: 0 } or { Length: 1 } => "Empty or single character string",
                string str when str is { Length: > 1 } => "String with more than one character",
                not null => "Some other non-null type",
                _ => "Null value"
            };

            return res;
        }
    }
}
