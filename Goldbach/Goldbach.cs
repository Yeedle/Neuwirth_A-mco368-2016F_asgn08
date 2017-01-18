using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Goldbach
{
    public static class Goldbach
    {
        public static Tuple<int, int> Composition(int x)
        { 
            if (x == 4) return new Tuple<int, int>(2, 2);
            for (int i = 3; i <= x/2; i += 2)
            {
                if (i.IsNotPrime() || (x-i).IsNotPrime()) continue;
                return new Tuple<int, int>(i, x - i);
            }
            return null;
        }

        public static bool IsNotPrime(this int x)
        {
            return !x.IsPrime();
        }

        public static bool IsPrime(this int x)
        {
            for (var i = 2; i <= Math.Sqrt(x); i++)
            {
                if (x % i == 0)
                    return false;
            }
            return true;
        }
    }
}