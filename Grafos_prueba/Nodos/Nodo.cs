using System;
using System.Collections.Generic;
using System.Text;

namespace Grafos_prueba.Nodos
{
    public class Nodo
    {
       

        public List<Nodo> Nodos { get; set; }

        public Func<object[], object> F { get; set; }

        
    }
}
