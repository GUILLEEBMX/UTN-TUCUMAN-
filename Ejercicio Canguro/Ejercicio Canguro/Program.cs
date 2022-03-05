using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_Canguro
{
    class Program
    {
        static void Main(string[] args)
        {

            Canguro c1 = new Canguro();
            Console.WriteLine("Ubicacion canguro 1");
            c1.Ubicacion = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Velocidad canguro 2");
            c1.Velocidad = Convert.ToInt32(Console.ReadLine());

            Canguro c2 = new Canguro();
            Console.WriteLine("Ubicacion canguro 2");
            c2.Ubicacion = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Velocidad canguro 2");
            c2.Velocidad = Convert.ToInt32(Console.ReadLine());

            int salto = 0;
            int x = 0;
            int x2 = 0;


            if (c1.Ubicacion >= 0 && c1.Ubicacion < c2.Ubicacion && c2.Ubicacion <= 10000)
            {
                if (c1.Velocidad >= 1 && c2.Velocidad >= 1 && c1.Velocidad <= 10000 && c2.Velocidad <= 10000)
                {
                    while (c1.Ubicacion != c2.Ubicacion && salto != 10000 )
                    {
                        x = c1.Saltar(c1.Velocidad);
                        x2 = c2.Saltar(c2.Velocidad);
                        salto++;


                    }
                    if (salto.Equals(10000))
                    { Console.WriteLine("Dieron 10000 saltos sin encontrarse, por lo que la respuesta es No ");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Luego de " + salto + " saltos ambos canguros se encuentran en ubicacion " + x + ", por lo que la " +
                        "respuesta es SI.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Verifique restricciones de velocidad");
                }
            }
            else

            {
                Console.WriteLine("Verifique restricciones de ubicación");
            }


        }
    }
}
