using CsharpKeywords.Keywords;

namespace CsharpKeywords
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region is keyword
            object objStr = "test word";
            var isCls = new IsMethods();
            var isResFirst = isCls.CheckValueTypeFirst(objStr);
            var isResSecond = isCls.CheckValueTypeSecond(objStr);
            Console.WriteLine($"Is res first: {isResFirst}");
            Console.WriteLine($"Is res second: {isResSecond}");
            #endregion

            #region not keyword
            object objInt = 42;
            var notCls = new NotKeyword();
            var notRes = notCls.CheckValueType(objInt);
            Console.WriteLine($"Not res: {notRes}");
            #endregion

            #region or keyword
            object objOr = 3.14;
            var orCls = new OrKeyword();
            var orRes = orCls.CheckIntDouble(objInt);
            Console.WriteLine($"Or res: {orRes}");
            #endregion

            #region combining keyword
            object inp = null;//2, -2, "", "test", 3m, null
            var combiningCls = new CombiningKeyword();
            var combiRes = combiningCls.CheckMultipleType(inp);
            Console.WriteLine($"Combining res: {combiRes}");
            #endregion
            Console.ReadKey();
        }
    }
}
