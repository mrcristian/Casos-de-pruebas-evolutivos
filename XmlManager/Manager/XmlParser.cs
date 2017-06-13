using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XmlManager.Manager
{
    public class XmlParser
    {
        private string _filePath;

        public XmlParser(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Función que parsea un documento XML a 
        /// un objeto de Tipo T
        /// </summary>
        /// <typeparam name="T">Clase que  heredar de class</typeparam>
        /// <param name="nodeName">Nombre del nodo en xml</param>
        /// <param name="selection">Función que permite la selección de 
        /// Xelement al objeto T</param>
        /// <returns></returns>
        public IEnumerable<T> Parse<T>(string nodeName, Func<XElement, T> selection)
            where T : class
        {
            var doc = XDocument.Load(_filePath);
            var ns = XNamespace.None;
            var resultList = doc.Descendants(ns + nodeName)
                .Select(selection);
            return resultList;
        }
    }
}
