using GrafosCasosPrueba.Nodos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrafosCasosPrueba.grafo
{
    public static class GrafoExtensions
    {

        private static Queue<Nodo> _queue;

        /// <summary>
        /// Función que me genera todos los 
        /// caminos posibles de un grafo
        /// </summary>
        /// <param name="_grafo">Grafo usado para el recorrido</param>
        /// <returns></returns>
        public static IEnumerable<string>
            GetCaminos(this Grafo _grafo)
        {
            var camino = _grafo.GetRaiz();
            _queue = new Queue<Nodo>();
            _queue.Enqueue(_grafo.GetRaiz());
            while(_queue.Count > 0)
            {
                var node = _queue.Dequeue();
                foreach (var child in node.Nodos.Values)
                {
                    _queue.Enqueue(child);
                }
            }
            return new List<string>();
        }

    }
}
