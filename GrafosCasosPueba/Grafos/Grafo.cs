using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrafosCasosPrueba.Nodos
{
    public class Grafo
    {
        private int _numAristas;
        private Dictionary<string, Nodo> _nodos;
        public int nVariables { get; set; }
        public float[] variables { get; set; }
        public Dictionary<string, Nodo> Nodos { get; set; }

        public Grafo() { }
        public Grafo(int nVariables)
        {
            _numAristas = 0;
            _nodos = new Dictionary<string, Nodo>();
            this.nVariables = nVariables;
            this.variables = variables;
        }

        public void AddNodo(string key,Nodo newNodo) {
            if (!_nodos.ContainsKey(key)) {
                _nodos.Add(key,newNodo);
            }
        }
        public void RelacionarNodos(string key1, string key2) {
            if (_nodos.ContainsKey(key1) && _nodos.ContainsKey(key2)) {
                _nodos[key1].AddNodo(key2, _nodos[key2]);
                _numAristas++;
            }
        }
        public Nodo GetRaiz() {
            return _nodos.First().Value;
        }

        /// <summary>
        /// Función que muestra el camino recorrido por un caso de prueba
        /// </summary>
        /// <param name="variables">array de objetos con las variables iniciales
        /// del progeama</param>
        /// <returns></returns>
        public string getCamino(float[] variables)
        {
            return this.GetRaiz().F(this.GetRaiz(), variables);
        }

        /// <summary>
        /// Función que permite calcular la complejidad ciclomática
        /// de un grafo
        /// </summary>
        /// <returns>El valor de la complejidad ciclomática, 
        /// ó -1 si la complejidad ciclomática 
        /// no se puede calcular</returns>
        public int GetCC()
        {
            var n1 = _nodos.Values
                .Where(g => g.Nodos.Count > 1).Count() + 1;
            var n2 = (_numAristas - _nodos.Count) + 2;

            return (n1 == n2) ? n1 : -1;
        }

    }
}
