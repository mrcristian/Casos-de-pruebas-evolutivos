using Genetics.Population;
using GrafosCasosPrueba.Nodos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Casos_de_pruebas_evolutivos.Modelo
{
    public class IndividuoPrueba : IIndividual
    {

        

        public Grafo _grafo { get; set; }
        public float[] _representacion { get; set; }
        public int NumVariables { get; set; }
        

        private float _fintess;
        private Func<IndividuoPrueba, IndividuoPrueba,
            IndividuoPrueba> _getCross;
        private Action<IndividuoPrueba> _doMutate;
        private Func<IndividuoPrueba, float> _doEvaluate;

        public Func<IndividuoPrueba, float> Evaluacion => _doEvaluate;
        public Action<IndividuoPrueba> GetMutation => _doMutate;
        public Func<IndividuoPrueba, IndividuoPrueba,
            IndividuoPrueba> GetCross => _getCross;
        public float Fitness => _fintess;

        public Func<IndividuoPrueba, float> Evaluate {
            get => _doEvaluate;
            set => _doEvaluate = value;
        }
        

        public IndividuoPrueba(ref Grafo newGrafo,
            ref Func<IndividuoPrueba, float> eval,
            ref Action<IndividuoPrueba> mutation,
            ref Func<IndividuoPrueba,
                IndividuoPrueba, IndividuoPrueba> cross,
            float[] representacion)
        {            
            _grafo = newGrafo;
            NumVariables = _grafo.nVariables;
            _representacion = representacion;

            _doEvaluate = eval;
            _doMutate = mutation;
            _getCross = cross;
            
            _fintess = Evaluacion(this);
        }

        public IIndividual Get_Cross(IIndividual otherParent)
        {
            return _getCross(this, (IndividuoPrueba)otherParent);
        }

        public void Mutate()
        {
            _doMutate(this);
        }
    }
}
