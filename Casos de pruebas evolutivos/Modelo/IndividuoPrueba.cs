using Genetics.Population;
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
        private Func<IIndividual, IIndividual> _getCross;
        private Action _doMutate;
        private Func<IndividuoPrueba, float> _doEvaluate;

        public float Fitness => _fintess;

        public Func<IndividuoPrueba, float> Evaluate {
            get => _doEvaluate;
            set => _doEvaluate = value;
        }

        Func<IIndividual, IIndividual> IIndividual.Get_Cross {
            get => _getCross;
            set => _getCross = value;
        }
        Action IIndividual.Mutate
        {
            get => _doMutate;
            set => _doMutate = value;
        }

        public IndividuoPrueba(ref Grafo newGrafo,
            Func<IndividuoPrueba, float> eval,
            Action mutation,
            Func<IIndividual, IIndividual> cross,
            float[] representacion)
        {            
            _grafo = newGrafo;
            NumVariables = _grafo.nVariables;
            _representacion = representacion;
            
            _fintess = Evaluacion(this);
        }

        
    }
}
