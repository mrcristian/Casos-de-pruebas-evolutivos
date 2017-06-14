using System;
using System.Collections.Generic;
using System.Text;
using XmlManager.Manager;
using GrafosCasosPrueba.grafo;
using GrafosCasosPrueba.Nodos;
using System.Linq;
using System.Xml.Linq;

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
                var adyacencias
                = new Dictionary<string, string[]>();
                var grafo = new Grafo()
                {
                    nVariables = Convert.ToInt32(
                        element.Attribute("numVariables").Value),
                    Nodos = element.Descendants("nodo")
                           .Select(elemNodo =>
                           {
                               var nombre = elemNodo
                               .Attribute("nombre").Value;

                               var variables = elemNodo
                               .Descendants("variable")
                               .Select(item => item.Value)
                               .ToArray();

                               var siguientes = elemNodo
                               .Descendants("siguiente")
                               .Select(item => item.Value)
                               .ToArray();
                               adyacencias.Add(nombre, siguientes);
                               return new Nodo()
                               {
                                   Nombre = nombre,
                                   F = getFuncion(elemNodo)
                               };
                           })
                           .ToDictionary(item => item.Nombre)
                };
                grafo.Set_Adyacencias(adyacencias);
                return grafo;
            });
            return grafos;
        }

        #region Funcion Factory

        private Func<Nodo, float[], string> getFuncion(XElement elemNodo)
        {
            var tipo = elemNodo.Attribute("tipo").Value;
            var variables = elemNodo
                               .Descendants("variable")
                               .Select(item => item.Value)
                               .ToArray();

            var siguientes = elemNodo.Descendants("siguiente")
            .Select(item => item.Value).ToArray();

            switch (tipo)
            {
                case "asignacion":
                    var asignaciones = elemNodo.Descendants("asignacion");
                    return get_AssingFunction(asignaciones,
                        variables, siguientes[0]);
                case "bloque":
                    return get_BlockFunction(siguientes[0]);
                case "comparacion":
                    var comparacion = get_Comparisson(
                        elemNodo.Descendants("operacion").First());
                    return get_ComparissonFunction(siguientes, comparacion);
                default:
                    return get_FinalFunction();
            }
        }

        private Func<Nodo, bool> get_Comparisson(XElement operacion)
        {
            var tipo = operacion.Attribute("tipo").Value;
            var operandos = operacion.Descendants("operando")
                .ToList();
            var op1 = get_Operator(operandos[0]);
            var pos2 = (operandos[0].Attribute("tipo").Value
                == "suboperacion") ? 3 : 1;
            var op2 = get_Operator(operandos[pos2]);
            switch (tipo)
            {
                case "mayor":
                    return (nodo) => op1(nodo) > op2(nodo);
                case "menor":
                    return (nodo) => op1(nodo) < op2(nodo);
                case "mayor-igual":
                    return (nodo) => op1(nodo) >= op2(nodo);
                case "menor-igual":
                    return (nodo) => op1(nodo) <= op2(nodo);
                case "diferente":
                    return (nodo) => op1(nodo) != op2(nodo);
                default:
                    return (nodo) => op1(nodo) == op2(nodo);
            }
        }

        private Func<Nodo, int> get_Operation(XElement operacion)
        {
            var tipo = operacion.Attribute("tipo").Value;
            var operandos = operacion.Descendants("operando")
                .ToList();
            var op1 = get_Operator(operandos[0]);
            var op2 = get_Operator(operandos[1]);
            switch (tipo)
            {
                case "suma":
                    return (nodo) => op1(nodo) + op2(nodo);
                case "resta":
                    return (nodo) => op1(nodo) - op2(nodo);
                case "mod":
                    return (nodo) => op1(nodo) % op2(nodo);
                case "multiplicacion":
                    return (nodo) => op1(nodo) * op2(nodo);
                case "division":
                    return (nodo) => op1(nodo) / op2(nodo);
                default:
                    return (nodo) => 0;
            }
        }

        private Func<Nodo, int> get_Operator(XElement operando,
            float[] data = null)
        {
            var tipo = operando.Attribute("tipo").Value;
            switch (tipo)
            {
                case "valor":
                    return (nodo) => Convert.ToInt32(operando.Value);
                case "variable":
                    return (nodo) => Convert.ToInt32(
                        nodo.GetVariable(operando.Value));
                case "entrada":
                    return (nodo) =>
                    (int)data[Convert.ToInt32(operando.Value)];
                case "suboperacion":
                    return get_Operation(
                        operando.Descendants("operacion").First());
                default:
                    return (nodo) => 0;
            }
        }




        /// <summary>
        /// Función que crea una función 
        /// capaz de asignar múltiples variables
        /// </summary>
        /// <param name="varibles">nombre de las variables a ser asignadas</param>
        /// <returns>La función con las variables asignadas</returns>
        private Func<Nodo, float[], string>
            get_AssingFunction(IEnumerable<XElement> asignaciones,
            string[] varibles, string siguiente)
        {
            List<Action<Nodo, float[]>> funcs
                = new List<Action<Nodo, float[]>>();
            foreach (var assign in asignaciones)
            {
                var variable = assign.Attribute("variable").Value;
                funcs.Add((nodo, data) =>
                {
                    var value = get_Operator(
                        assign.Descendants("operando").First(), data);
                    nodo.SetVariable(variable, value(nodo));
                });
            }

            Func<Nodo, float[], string> f = (nodo, data) =>
            {
                foreach (var func in funcs)
                {
                    func(nodo, data);
                }
                var n = nodo.Nodos[siguiente].Function(null) +
                "-" + nodo.Nombre;
                return n;
            };
            return f;
        }

        /// <summary>
        /// Función que retorna una función de bloque
        /// </summary>
        /// <param name="siguiente">nombre del siguiente nodo</param>
        /// <returns></returns>
        private Func<Nodo, float[], string>
            get_BlockFunction(string siguiente)
        {
            return (nodo, data) => nodo.Nodos[siguiente]
                            .Function(null) + "-" + nodo.Nombre;
        }

        /// <summary>
        /// Función que retorna una función de comparación 
        /// dada las variables de entrada
        /// </summary>
        /// <param name="siguientes">Nombre de los siguientes 
        /// nodos en la ejecución del algoritmo, 
        /// el primero es el caso verdadero;
        /// el segundo se ejecuta cuando la comparación es falsa</param>
        /// <param name="fComparacion">función de comparación
        /// que permite evaluar la ejecución del código
        /// </param>
        /// <returns></returns>
        private Func<Nodo, float[], string> get_ComparissonFunction(
            string[] siguientes, Func<Nodo, bool> fComparacion)
        {
            Func<Nodo, float[], string> f = (nodo, data) =>
            {
                var valor = (fComparacion(nodo))
                    ? siguientes[0] : siguientes[1];

                return nodo.Nodos[valor].Function(null) 
                + "-" + nodo.Nombre;

            };
            return f;
        }

        /// <summary>
        /// Función que crea la función del nodo final del grafo
        /// </summary>
        /// <returns>La función que termina la ejecución del programa</returns>
        private Func<Nodo, float[], string>
            get_FinalFunction()
        {
            Func<Nodo, float[], string> f = (nodo, data) => nodo.Nombre;
            return f;
        }

        #endregion
    }
}
