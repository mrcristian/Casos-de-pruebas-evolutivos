using GrafosCasosPrueba.Nodos;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="_this">Grafo usado para el recorrido</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<string>>
            GetCaminos(this Grafo _this)
        {
            var caminoBase = new List<string>();
            var caminos = new List<List<string>>()
            {
                caminoBase
            };
            getCamino(_this.GetRaiz(),ref caminos, ref caminoBase);

            return caminos;

        }
        private static void
            getCamino(Nodo nodo,
            ref List<List<string>> caminos,
            ref List<string> caminoBase)
        {
            caminoBase.Append(nodo.Nombre);
            foreach (var child in nodo.Nodos.Values)
            {
                var newCaminoBase = caminoBase
                    .Select(i => i);
                caminos.Append(newCaminoBase);
            }

        }
    }
}
