using System;
using System.Collections.Generic;
using System.Text;

namespace GrafosCasosPrueba.Nodos
{
    public class Nodo
    {
        public Dictionary<string, Nodo> Nodos { get; set; }

        public Func<Nodo, object[], object> F { get; set; }

        public Nodo()
        {

        }
        public Nodo(Func<Nodo, object[], object> funcion)
        {
            Nodos = new Dictionary<string, Nodo>();
            F = funcion;
        }

        public void AddNodo(string id, Nodo newNodo)
        {
            Nodos.Add(id, newNodo);
        }




    }
}
