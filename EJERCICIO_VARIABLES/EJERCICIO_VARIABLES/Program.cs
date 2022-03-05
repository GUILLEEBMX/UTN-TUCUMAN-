using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EJERCICIO_VARIABLES
{
    class Program
    {
        static void Main(string[] args)
        {



            Console.WriteLine("INGRESA LA CADENA A...");
            string A = Console.ReadLine();

            Console.WriteLine("INGRESA LA  CADENA B...");
            string B = Console.ReadLine() ;


             
            Console.WriteLine("EL VALOR NUEVO DE A ES: " +(A.Replace(A,B)) + "\n"  + "EL VALOR NUEVO DE B ES: " +(B.Replace(B, A)));

            


            Console.ReadKey();


        }








    }
}
