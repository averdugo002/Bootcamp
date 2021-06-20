using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaRectangulo_AV
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaramos las variables: 
            string anchuraTexto;
            string alturaTexto;
            int anchura;
            int altura;
            int resultado;


            Console.WriteLine("Por favor, introduzca la anchura del rectángulo:"); //Escribimos el texto que se mostrará en pantalla
            anchuraTexto = Console.ReadLine(); // Guardamos el input introducido por el usuario
            anchura = Convert.ToInt32(anchuraTexto); //Convertimos nuestra variable de tipo string a un valor entero.
            Console.WriteLine("Por favor, introduzca la altura del rectángulo:");
            alturaTexto = Console.ReadLine();
            altura = Convert.ToInt32(alturaTexto);
            resultado = anchura * altura; // Definimos el cálculo de la variable resultado.
            Console.WriteLine("El area del rectángulo es: " + resultado);
        }
    }
}
