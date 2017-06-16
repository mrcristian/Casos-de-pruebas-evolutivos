
using Casos_de_pruebas_evolutivos.Controladores;
using Casos_de_pruebas_evolutivos.Modelo;
using Genetics.Algorithims;
using GrafosCasosPrueba.Nodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Casos_de_pruebas_evolutivos
{
    class Program
    {
        static void Main(string[] args)
        {
            var controlador = new Controlador("Data/Grafos.xml");
            int condicion = 1;
            while (condicion != 3)
            {
                Console.WriteLine("Digite la funcion de evaluacion que desea utilizar");
                Console.WriteLine("1) Función de evaluación empirica");
                Console.WriteLine("2) Función de evaluación de Kumar (modificada)");
                Console.WriteLine("3) Salir");
                condicion = Convert.ToInt32(Console.ReadLine());
                IEnumerable<IndividuoPrueba> pob = null;
                switch (condicion)
                {
                    case 1:
                         pob = controlador.Get_ResultadoGenetico(2, controlador.getFuncionEvaluacionB(),
                            (ind) => { }, controlador.getFuncionCruceA(), 1000, 10);
                        imprimirRes(pob);
                        break;
                    case 2:
                         pob = controlador.Get_ResultadoGenetico(2, controlador.getFuncionEvaluacionA(),
                           (ind) => { }, controlador.getFuncionCruceA(), 1000, 10);
                        imprimirRes(pob);
                        break;
                    case 3:
                        Console.WriteLine("Hasta luego...");
                        break;
                    default:
                        Console.WriteLine("Digite una opcion valida");
                        break;
                }
            }
            Console.ReadKey();
        }

        public static void imprimirRes(IEnumerable<IndividuoPrueba> pob)
        {
            Console.WriteLine($"Fitness: {pob.First().Fitness}");
            foreach (float item in pob.First()._representacion)
            {
                Console.Write(item + "|");
            }
            Console.WriteLine();
        }
    }
}
