using System;
using System.Collections.Generic;
using System.Text;
using XmlManager.Manager;
using GrafosCasosPrueba.grafo;
using GrafosCasosPrueba.Nodos;
using System.Linq;

namespace GrafosCasosPrueba.Grafos.Factory
{
    public class GrafoFactory
    {
        private XmlParser _parser;

        public GrafoFactory(XmlParser parser)
        {
            _parser = parser;
        }

        public IEnumerable<Grafo> ReadFromFile()
        {
            var grafos = _parser.Parse<Grafo>("grafo", (element) =>
            {
                var grafo = new Grafo()
                {
                    nVariables = Convert.ToInt32(
                        element.Attribute("numVariables").Value),
                    Nodos = element.Descendants("nodo")
                           .Select(elemNodo => new Nodo()
                           {
                               Nombre = elemNodo.Attribute("nombre").Value
                               
                           })
                           .ToDictionary(item => item.Nombre)
                        
                 };
                return grafo;
             });
            return grafos;
        }
    }
}
