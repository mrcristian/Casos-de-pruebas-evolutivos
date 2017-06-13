using System;
using System.Collections.Generic;

namespace Manager.Reading
{
    class XmlManager
    {
        
        private string _filePath;


        public XmlManager(string filePath)
        {
            _filePath = filePath;
        }

        public List<T> Read<T>()
        {
            
            using (XmlReader reader = XmlReader.Create(_filePath))
            {

            }
        }

        

    }
}
