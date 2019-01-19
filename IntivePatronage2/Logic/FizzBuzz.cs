using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntivePatronage2.Logic
{
    public class FizzBuzz
    {
        public static string Create(int value)
        {
            return $"{(value % 2 == 0 ? "Fizz" : String.Empty)}{(value % 3 == 0 ? "Buzz" : String.Empty)}";
        }
    }
}
