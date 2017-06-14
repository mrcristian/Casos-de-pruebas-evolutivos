using Casos_de_pruebas_evolutivos.Modelo;
using Genetics.Algorithims;
using Genetics.Population;
using GrafosCasosPrueba.Grafos.Factory;
using GrafosCasosPrueba.Nodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Casos_de_pruebas_evolutivos.Controladores
{
    public class Controlador
    {
        GrafoFactory _grafoFactory;
        List<Grafo> _grafos;

        public Controlador(string filePath)
        {
            _grafoFactory = new GrafoFactory(filePath);
            _grafos = _grafoFactory.ReadFromFile().ToList();
        }

        /// <summary>
        /// Función que retorna una función de generación aleatorea 
        /// para un tipo de grafo, dada condiciones de los parámetros
        /// Se recomienda que se llame la función UNA VEZ 
        /// por instancia de grafo
        /// </summary>
        /// <param name="grafo">Grafo usado para evaluar el individuo</param>
        /// <param name="funEval">Función de evaluación para  el
        /// individuo</param>
        /// <param name="mutacion">Función de mutación del individuo</param>
        /// <param name="cruce"> Función de cruce del individuo
        /// </param>
        /// <returns>una variable función que, a su vez, 
        /// retorna un individuo aleatoreo</returns>
        private Func<IndividuoPrueba> get_FuncionAleatorea(Grafo grafo,
            Func<IndividuoPrueba, float> funEval,
            Action<IndividuoPrueba> mutacion,
            Func<IndividuoPrueba, IndividuoPrueba, IndividuoPrueba> cruce)
        {
            Random r = new Random();
            int size = grafo.nVariables * grafo.GetCC();
            return () =>
            {
                var _representacion = new float[size];
                for (int i = 0; i < size; i++)
                {
                    _representacion[i] =
                    (float)Math.Floor(r.NextDouble() * 400);
                }
                return new IndividuoPrueba(ref grafo, ref funEval,
                    ref mutacion, ref cruce, _representacion);
            };
        }


        /// <summary>
        /// Función que me da la población final de la ejecución del 
        /// algoritmo genético, a partir de los parámetros dados
        /// </summary>
        /// <param name="posGrafo">índice del grafo,
        /// en el archivo XML indicado en la construcción del controlador</param>
        /// <param name="funEval">Función de evaluación 
        /// para la ejecución del algoritmo</param>
        /// <param name="mutacion"> Función de mutación para 
        /// la ejecución del algoritmo</param>
        /// <param name="cruce">Función de cruce para la 
        /// ejecución del algoritmo</param>
        /// <param name="numIteraciones">Número de iteraciones del
        /// algoritmo</param>
        /// <param name="pobCount">Tamaño de la población que pasará a
        /// la siguiente generación</param>
        /// <param name="max">Indica si el algoritmo va a 
        /// ser maximizado(verdadero) o minimizado (falso). Por defecto
        /// su valor es verdadero</param>
        /// <param name="funConfiguracion">Función para especificar configuraciones
        /// adicionales a la población durante el inicio de cada generación.
        /// Por defecto es nula</param>
        /// <returns>Una lista con los n mejores individuos.
        /// Siendo n igual al parámetro pobCount</returns>
        public IEnumerable<IndividuoPrueba> Get_ResultadoGenetico(
            int posGrafo, Func<IndividuoPrueba, float> funEval,
            Action<IndividuoPrueba> mutacion,
            Func<IndividuoPrueba, IndividuoPrueba, IndividuoPrueba> cruce,
            int numIteraciones, int pobCount, bool max = true,
            Action<IEnumerable<IndividuoPrueba>> funConfiguracion = null)
        {
            var funAleatorea = get_FuncionAleatorea(_grafos[posGrafo],
                funEval, mutacion, cruce);
            var genetico 
                = new GeneticAlgorithm<IndividuoPrueba>
                (numIteraciones, pobCount, funAleatorea);
            return genetico.Run(pobCount, max, funConfiguracion);
        }
        
    }
}
