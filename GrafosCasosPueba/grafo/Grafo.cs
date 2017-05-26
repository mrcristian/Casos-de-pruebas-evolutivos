﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrafosCasosPrueba.Nodos
{
    public class Grafo
    {
        private int _numAristas;
        Dictionary<string, Nodo> _grafo;

        

        public Grafo()
        {
            _numAristas = 0;
            _grafo = new Dictionary<string, Nodo>();
        }

        public void AddNodo(string key,Nodo newNodo) {
            if (!_grafo.ContainsKey(key)) {
                _grafo.Add(key,newNodo);
            }
        }
        public void RelacionarNodos(string key1, string key2) {
            if (_grafo.ContainsKey(key1) && _grafo.ContainsKey(key2)) {
                _grafo[key1].AddNodo(key2, _grafo[key2]);
                _numAristas++;
            }
        }
        public Nodo GetRaiz() {
            return _grafo.First().Value;
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
            var n1 = _grafo.Values
                .Where(g => g.Nodos.Count > 1).Count() + 1;
            var n2 = (_numAristas - _grafo.Count) + 2;

            return (n1 == n2) ? n1 : -1;
        }

    }
}
