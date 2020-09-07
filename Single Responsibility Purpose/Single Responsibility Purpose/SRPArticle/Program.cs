using System;
using System.Collections.Generic;
using System.Linq;

namespace SRPArticle
{

    class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine(OldSchoolAddification.AddNumbers(6669, 4444).ToString());
            Console.WriteLine(OldSchoolAddificationSRP.AddNumbers(6669, 4444).ToString());

            Console.ReadKey();
        }
    }
}
