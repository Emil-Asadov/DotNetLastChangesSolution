using System.Text;

namespace ModernUsingDeclaration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var txt = string.Empty;
            var filePath = @"C:\Users\e.q.asadov\Downloads\Test.txt";

            #region Traditional using
            using (var fsTraditional = new FileStream(filePath, FileMode.Open))
            {
                byte[] arrTraditional = new byte[fsTraditional.Length];
                fsTraditional.Read(arrTraditional, 0, arrTraditional.Length);
                txt = Encoding.UTF8.GetString(arrTraditional);
            }
            Console.Write($"Traditional using: {txt}");
            #endregion

            Console.WriteLine();
            Console.WriteLine();

            #region Modern using
            using var fsModern = new FileStream(filePath, FileMode.Open);
            byte[] arrModern = new byte[fsModern.Length];
            fsModern.Read(arrModern, 0, arrModern.Length);
            txt = Encoding.UTF8.GetString(arrModern);
            Console.Write($"Modern using: {txt}");
            #endregion

            //Asagidaki kod islemir. Cunku, fsModern hele dispose olunmayib. using-i block-da yazmadiqimiza gore dispose kod blokunun sonunda olacaq.
            //byte[] bytes = File.ReadAllBytes(filePath);
            //txt = Encoding.UTF8.GetString(bytes);
            //Console.Write($"File.ReadAllBytes: {txt}");


            Console.ReadKey();
        }
    }
}
