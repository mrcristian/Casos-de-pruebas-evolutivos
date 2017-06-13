using Genetics.Population;
using Genetics.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Algorithims
{
    public class GeneticAlgorithm<T>
        where T : IIndividual
    {
        private Func<T> _getRandomIndividual;
        private int _indCount;

        public GeneticAlgorithm(int indCount,int breedCount,
            Func<T> randomIndividual)
        {
            _indCount = indCount;
            _getRandomIndividual = randomIndividual;
        }

        public IEnumerable<T> Run(int genCount,
            bool max = false,
            Action<IEnumerable<T>> genConfiguration = null)            
        {
            var currentGen = Get_initialGeneration()                
                .ToList();
            for (int i = 0; i < genCount; i++)
            {
                genConfiguration?.Invoke(currentGen);
                Do_crossGeneration(currentGen);

                currentGen = (max) ?
                    currentGen
                    .OrderByDescending(ind => ind.Fitness)
                    .Take(_indCount)
                    .ToList() :
                    currentGen
                    .OrderBy(ind => ind.Fitness)
                    .Take(_indCount)
                    .ToList();
            }
            return currentGen;
        }

        private IEnumerable<T> Get_initialGeneration()            
        {
            var generation = new List<T>();
            for (int i = 0; i < _indCount; i++)
            {
                generation.Add(_getRandomIndividual());
            }
            return generation;
        }

        
        private void Do_crossGeneration(
            List<T> currentGen)
        {
            currentGen.Shuffle();
            var parents = currentGen.Take(_indCount / 2);            
            foreach (var item in parents)
            {
                currentGen.Append(
                    (T)item.Get_Cross(currentGen.Random()));
            }
        }
    }
}
