using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isTrue = isPrime(2367);
            Console.WriteLine(isTrue);
            Console.ReadKey();
        }


        public static bool isPrime(long n)
        {
            if (n <= 3) return n > 1;
            if (n%2 == 0 || n%3 == 0) return false;
            for (int i = 5; i*i<=n; i+=6)
            {
                if (n%i == 0 || n%(i + 2) == 0) return false;

            }
            return true;
        }
    }
}
