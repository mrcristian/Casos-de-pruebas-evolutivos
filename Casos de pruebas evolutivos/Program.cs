
using Casos_de_pruebas_evolutivos.Controladores;
using Casos_de_pruebas_evolutivos.Modelo;
using Genetics.Algorithims;
using GrafosCasosPrueba.Nodos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Casos_de_pruebas_evolutivos
{
    class Program
    {
        static void Main(string[] args)
        {
            var controlador = new Controlador("Data/Grafos.xml");

            var pob = controlador.Get_ResultadoGenetico(0, null,
                null, null, 1000, 10);

            Console.WriteLine($"ind: {pob.First().Fitness}");
            foreach (float item in pob.First()._representacion)
            {
                Console.Write(item + "|");
            }

            //Ejecucion
            Console.ReadKey();
        }
    }
}
