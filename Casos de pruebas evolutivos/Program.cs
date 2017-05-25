
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
            Dictionary<String, Nodo> nodos = new Dictionary<string, Nodo>();

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
                if (Convert.ToInt32(nodo.GetVariable("P")) + Convert.ToInt32(nodo.GetVariable("Q")) > 10)
                {
                    Console.WriteLine("mayor");
                    nodo.Nodos.ToList()[0].Value.F(nodo.Nodos.ToList()[0].Value, null);
                    return true;
                }
                else
                {
                    Console.WriteLine("menor");
                    nodo.Nodos.ToList()[1].Value.F(nodo.Nodos.ToList()[1].Value, null);
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
                    nodo.Nodos.ToList()[0].Value.F(nodo.Nodos.ToList()[0].Value, null);
                    return true;
                }
                else
                {
                    Console.WriteLine("menor");
                    nodo.Nodos.ToList()[1].Value.F(nodo.Nodos.ToList()[1].Value, null);
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
            nodos.Add("nodo1", new Nodo(F1));
            nodos.Add("nodo2", new Nodo(F2));
            nodos.Add("nodo3", new Nodo(F3));
            nodos.Add("nodo4", new Nodo(F4));
            nodos.Add("nodo5", new Nodo(F5));
            nodos.Add("nodo6", new Nodo(F6));

            //Relaciones
            nodos["nodo1"].AddNodo("nodo2", nodos["nodo2"]);

            nodos["nodo2"].AddNodo("nodo3", nodos["nodo3"]);
            nodos["nodo2"].AddNodo("nodo4", nodos["nodo4"]);

            nodos["nodo3"].AddNodo("nodo4", nodos["nodo4"]);

            nodos["nodo4"].AddNodo("nodo5", nodos["nodo5"]);
            nodos["nodo4"].AddNodo("nodo6", nodos["nodo6"]);

            nodos["nodo5"].AddNodo("nodo6", nodos["nodo6"]);


            //Ejecucion
            nodos["nodo1"].F(nodos["nodo1"], new object[] { 52, 6 });            
            Console.ReadKey();
        }
    }
}
