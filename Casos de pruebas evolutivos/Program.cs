
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
            #region funcionDeEvaluacion
            IndividuoPrueba.SetEvaluacion((ind) =>
            {
                Dictionary<string, string> recorridos
                = new Dictionary<string, string>();
                for (int i = 0; i < ind._representacion.Length;
                    i += ind.NumVariables)
                {
                    var variables = new float[ind.NumVariables];
                    for (int j = 0; j < ind.NumVariables; j++)
                    {
                        variables[j] = ind._representacion[i+j];
                    }
                    var camino = ind._grafo.getCamino(variables);
                    if (!recorridos.ContainsKey(camino))
                        recorridos.Add(camino, camino);
                }
                return recorridos.Count;
            });
            #endregion

            Grafo nodos = new Grafo(2, new float[] { 3, 6 });

            //Funciones
            Func<Nodo, float[], string> F1 = (nodo, data) =>
             {
                 nodo.SetVariable("P", data[0]);
                 nodo.SetVariable("Q", data[1]);


                 var n = nodo.Nodos.First().Value.F(nodo.Nodos.First().Value, null) +
                 "-" + nodo.Nombre;
                 return n;
             };
            Func<Nodo, float[], string> F2 = (nodo, data) =>
            {
                if (Convert.ToInt32(nodo.GetVariable("P"))
                + Convert.ToInt32(nodo.GetVariable("Q")) > 10)
                {
                    

                    return nodo.Nodos.ToList()[0].Value.F(
                        nodo.Nodos.ToList()[0].Value, null) + "-" + nodo.Nombre;
                }
                else
                {
                    
                    return nodo.Nodos.ToList()[1].Value.F(
                        nodo.Nodos.ToList()[1].Value, null) + "-" + nodo.Nombre;
                }
            };
            Func<Nodo, float[], string> F3 = (nodo, data) =>
            {
                
                return nodo.Nodos.First().Value.F(nodo.Nodos.First().Value, null) 
                + "-" + nodo.Nombre;

            };
            Func<Nodo, float[], string> F4 = (nodo, data) =>
            {
                if (Convert.ToInt32(nodo.GetVariable("P")) > 50)
                {
                    
                    return nodo.Nodos.ToList()[0].Value.F(
                        nodo.Nodos.ToList()[0].Value, null) + "-" + nodo.Nombre;

                }
                else
                {
                    
                    return nodo.Nodos.ToList()[1].Value.F(
                        nodo.Nodos.ToList()[1].Value, null) + "-" + nodo.Nombre;

                }
            };
            Func<Nodo, float[], string> F5 = (nodo, data) =>
            {
                
                return nodo.Nodos.First().Value.F(nodo.Nodos.First().Value, null) 
                + "-" + nodo.Nombre;

            };
            Func<Nodo, float[], string> F6 = (nodo, data) =>
            {
                
                return nodo.Nombre;
            };


            //Nodos
            nodos.AddNodo("nodo1", new Nodo(F1, "nodo1"));
            nodos.AddNodo("nodo2", new Nodo(F2, "nodo2"));
            nodos.AddNodo("nodo3", new Nodo(F3, "nodo3"));
            nodos.AddNodo("nodo4", new Nodo(F4, "nodo4"));
            nodos.AddNodo("nodo5", new Nodo(F5, "nodo5"));
            nodos.AddNodo("nodo6", new Nodo(F6, "nodo6"));

            //Relaciones
            nodos.RelacionarNodos("nodo1", "nodo2");
            nodos.RelacionarNodos("nodo2", "nodo3");
            nodos.RelacionarNodos("nodo2", "nodo4");
            nodos.RelacionarNodos("nodo3", "nodo4");
            nodos.RelacionarNodos("nodo4", "nodo5");
            nodos.RelacionarNodos("nodo4", "nodo6");
            nodos.RelacionarNodos("nodo5", "nodo6");


            Random r = new Random();
            int size = nodos.nVariables * nodos.GetCC();
            //Creacion genetico
            GeneticAlgorithm<IndividuoPrueba> genetico 
                = new GeneticAlgorithm<IndividuoPrueba>(10, 5, () =>
            {
                var _representacion = new float[size];
                for (int i = 0; i < size; i++)
                {
                    _representacion[i] = (float)Math.Floor(r.NextDouble()*400);
                }
                return new IndividuoPrueba(ref nodos, _representacion);
            });

            var pob = genetico.Run(10, true);

            Console.WriteLine($"ind: {pob.First().Fitness}");




            //Ejecucion
            Console.ReadKey();
        }
    }
}
