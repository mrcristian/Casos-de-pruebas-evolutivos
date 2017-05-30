﻿using Genetics.Population;
using GrafosCasosPrueba.Nodos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Casos_de_pruebas_evolutivos.Modelo
{
    class IndividuoPrueba : IIndividual
    {

        #region funcion de evaluacion
        public static Func<IndividuoPrueba, float> Evaluacion;
        public static void SetEvaluacion(Func<IndividuoPrueba, float> fun)
        {
            Evaluacion = fun;
        }
        #endregion

        public Grafo _grafo { get; set; }
        public float[] _representacion { get; set; }
        public int NumVariables { get; set; }

        private float _fintess;        

        public float Fitness => _fintess;

        public IndividuoPrueba(ref Grafo newGrafo, float[] representacion)
        {            
            _grafo = newGrafo;
            NumVariables = _grafo.nVariables;
            _representacion = representacion;
            
            _fintess = Evaluacion(this);
        }

        public IIndividual Get_Cross(IIndividual otherParent)
        {
            return this;
        }

        public void Mutate()
        {
            
        }
    }
}
