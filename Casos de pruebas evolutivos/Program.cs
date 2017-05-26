
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
            Grafo nodos = new Grafo();

            //Funciones
            Func<Nodo, object[], object> F1 = (nodo, data) =>
             {
                 nodo.SetVariable("P", data[0]);
                 nodo.SetVariable("Q", data[1]);

                 nodo.Nodos.First().Value.F(nodo.Nodos.First().Value, null);
                 return null;
             };
            Func<Nodo, object[], object> F2 = (nodo, data) =>
            {
                if (Convert.ToInt32(nodo.GetVariable("P"))
                + Convert.ToInt32(nodo.GetVariable("Q")) > 10)
                {
                    Console.WriteLine("mayor");
                    nodo.Nodos.ToList()[0].Value.F(
                        nodo.Nodos.ToList()[0].Value, null);
                    return true;
                }
                else
                {
                    Console.WriteLine("menor");
                    nodo.Nodos.ToList()[1].Value.F(
                        nodo.Nodos.ToList()[1].Value, null);
                    return false;
                }
            };
            Func<Nodo, object[], object> F3 = (nodo, data) =>
            {
                Console.WriteLine("Largo");
                nodo.Nodos.First().Value.F(nodo.Nodos.First().Value, null);
                return null;
            };
            Func<Nodo, object[], object> F4 = (nodo, data) =>
            {
                if (Convert.ToInt32(nodo.GetVariable("P")) > 50)
                {
                    Console.WriteLine("mayor");
                    nodo.Nodos.ToList()[0].Value.F(
                        nodo.Nodos.ToList()[0].Value, null);
                    return true;
                }
                else
                {
                    Console.WriteLine("menor");
                    nodo.Nodos.ToList()[1].Value.F(
                        nodo.Nodos.ToList()[1].Value, null);
                    return false;
                }
            };
            Func<Nodo, object[], object> F5 = (nodo, data) =>
            {
                Console.WriteLine("P largo");
                nodo.Nodos.First().Value.F(nodo.Nodos.First().Value, null);
                return null;
            };
            Func<Nodo, object[], object> F6 = (nodo, data) =>
            {
                Console.Write("Fin");
                return null;
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



            //Ejecucion
            nodos.GetRaiz().F(nodos.GetRaiz(),
                new object[] { 52, 6 });
            Console.WriteLine($"La complejidad Ciclomática del " +
                $"grafo es de {nodos.GetCC()}");
            Console.ReadKey();
        }
    }
}
