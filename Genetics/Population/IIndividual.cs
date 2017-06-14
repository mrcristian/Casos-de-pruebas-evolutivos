using System;
using System.Collections.Generic;
using System.Text;

namespace Genetics.Population
{
    public interface IIndividual
    {
        float Fitness { get; }
        IIndividual Get_Cross(IIndividual otherParent);
        void Mutate()
    }
}
