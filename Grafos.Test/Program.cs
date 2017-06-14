using GrafosCasosPrueba.Grafos.Factory;
using System;

namespace Grafos.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            GrafoFactory gf = new GrafoFactory(
                    new XmlManager.Manager.XmlParser("Data/Grafos.xml"));
            foreach (var item in gf.ReadFromFile())
            {
                var s = item.GetRaiz().Function(new float[] { 94 , 6 });
                Console.WriteLine(s);
            }
            Console.ReadKey();
        }
    }
}