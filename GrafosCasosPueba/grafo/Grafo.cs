using System;
using System.Collections.Generic;
using System.Text;

namespace GrafosCasosPrueba.Nodos
{
    public class Grafo
    {
        Dictionary<string, Nodo> grafo;

        public Grafo()
        {
            grafo = new Dictionary<string, Nodo>();
        }

        public void AddNodo(string key,Nodo newNodo) {
            if (!grafo.ContainsKey(key)) {
                grafo.Add(key,newNodo);
            }
        }
        public void RelacionarNodos(string key1, string key2) {
            if (grafo.ContainsKey(key1) && grafo.ContainsKey(key2)) {
                grafo[key1].AddNodo(key2, grafo[key2]);
            }
        }
        public Nodo GetRaiz(string key) {
            return grafo[key];
        }

    }
}
