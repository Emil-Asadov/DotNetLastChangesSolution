using System.Security.Cryptography;

namespace RSAKeyOpenSSL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var rsa = RSA.Create(2048);
            var publicKey = rsa.ExportSubjectPublicKeyInfo();
            var pem = PemEncoding.Write("PUBLIC KEY", publicKey);

            Console.WriteLine(pem);
        }
    }
}
