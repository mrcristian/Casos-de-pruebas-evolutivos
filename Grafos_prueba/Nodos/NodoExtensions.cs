using System;
using System.Collections.Generic;
using System.Text;

namespace Grafos_prueba.Nodos
{
    public static class NodoExtensions
    {
        public static Dictionary<string, object> Variables = new Dictionary<string, object>();

        public static void SetVariable(this Nodo _this, string nombre, object valor)
        {
            if(!Variables.ContainsKey(nombre))
            {
                Variables.Add(nombre, valor);
            }
            else
            {
                Variables[nombre] = valor;
            }
        }

        public static void GetVariable(this Nodo _this, string nombre)
        {
            return (Variables.ContainsKey(nombre)) ? Variables[nombre] : null;
        }
    }
}
