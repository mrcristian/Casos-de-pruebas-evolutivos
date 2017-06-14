using System;
using System.Collections.Generic;
using System.Text;

namespace GrafosCasosPrueba.Nodos
{
    public class Nodo
    {
        public string  Nombre { get; set; }

        public Dictionary<string, Nodo> Nodos { get; set; }

        public Func<Nodo, float[], string> F { get; set; }

        public Nodo()
        {

            Nodos = new Dictionary<string, Nodo>();
        }
        public Nodo(Func<Nodo, float[], string> funcion,
            string nombre)
            :this()
        {
            Nombre = nombre;
            F = funcion;
        }

        public void AddNodo(string id, Nodo newNodo)
        {
            Nodos.Add(id, newNodo);
        }

        public string Function(float[] args)
        {
            return (F != null) ? F(this, args) : "";
        }


    }
}
