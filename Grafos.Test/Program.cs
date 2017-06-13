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
                Console.WriteLine(item);
            }
        }
    }
}