using System;
using System.Collections.Generic;
using System.Text;

namespace Genetics.Population
{
    public interface IIndividual
    {
        float Fitness { get; }
        Func<IIndividual, IIndividual> Get_Cross { get; set; }
        Action Mutate { get; set; }
    }
}
