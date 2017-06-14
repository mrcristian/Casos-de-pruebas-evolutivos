using GrafosCasosPrueba.Grafos.Factory;
using System;
using System.Collections.Generic;

namespace Grafos.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<float[]> entradas = new List<float[]>()
            {
                new float[] { 51, -45},
                new float[] {5, 4, 6, 8},
                new float[] { 100 },
            };
            GrafoFactory gf = new GrafoFactory(
                    new XmlManager.Manager.XmlParser("Data/Grafos.xml"));
            int index = 0;
            foreach (var item in gf.ReadFromFile())
            {
                var s = item.GetRaiz().Function(entradas[index]);
                Console.WriteLine(s);
                index++;
            }
            Console.ReadKey();
        }
    }
}