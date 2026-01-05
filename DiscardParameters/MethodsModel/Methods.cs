using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscardParameters.MethodsModel
{
    public class Methods
    {
        public string DiscardValue(object inp)
        {
            // int _ → means:
            // “If inp is of type int, match it and ignore the value”
            //_ is a discard variable
            var resNewSwitch = inp switch
            {
                int _ => "Data is integer",
                string _ => "Data is string",
                _ => "Data has an unknown type"
            };

            var resOldSwitch = string.Empty;
            switch (inp)
            {
                case int _:
                    resOldSwitch = "Data is integer";
                    break;
                case string _:
                    resOldSwitch = "Data is string";
                    break;
                default:
                    resOldSwitch = "Data has an unknown type";
                    break;
            }

            return resNewSwitch;
        }

        public (string firstName, string lastName) DiscardTupleMembers()
        {
            return ("Kamran", "Asadov");
        }

        public string DiscardOutParameter(string inp)
        {
            var res = string.Empty;
            var convOutDiscard = int.TryParse(inp, out _);
            res = convOutDiscard ? "Parsing successful" : "Failed to parse input as an integer";

            return res;
        }

        public string DiscardReturnValue(string inp)
        {
            var res = string.Empty;
            if (string.IsNullOrWhiteSpace(inp))
            {
                res = "Data is null or empty";
                return res;
            }
            res = $"Processing data:{inp}";

            return res;
        }
    }
}
