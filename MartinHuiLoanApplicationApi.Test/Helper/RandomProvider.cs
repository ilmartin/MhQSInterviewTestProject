using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartinHuiLoanApplicationApi.Test.Helper
{
    internal class RandomProvider
    {
        private static Random random = new Random();

        internal static string RandomAlphanumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return RandomString(length, chars);
        }

        internal static string RandomAlphabet(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return RandomString(length, chars);
        }
        internal static int RandomNumber(int from, int to)
        {
            return random.Next(from, to);
        }

        private static string RandomString(int length, string chars)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
