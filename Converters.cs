﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SaveXML
{
    /// <summary>
    /// <see cref="Converters"/> provides methods for converting XML to objects and vice-versa.
    /// </summary>
    public class Converters
    {
        /// <summary>
        /// <see cref="CreateXML"/> method creates an XML string from any class object.
        /// </summary>
        /// <example>
        /// <code>
        /// ABCClass abcObject = new ABCClass();
        /// string outputXML = SaveXML.Converters.CreateXML(abcObject);
        /// </code>
        /// </example>
        /// <param name="YourClassObject">Any class object that you want to convert to XML.</param>
        /// <returns>Returns XML as string.</returns>
        public static string CreateXML(Object YourClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, YourClassObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }
        /// <summary>
        /// <see cref="CreateObject"/> method converts specified XML string into an <see cref="Object"/> that can be explicitly converted to corresponding class object.
        /// </summary>
        /// <example>
        /// <code>
        /// string abcXML = "<root><child></child></root>"
        /// ABCClass abcObject = (ABCClass) SaveXML.Converters.CreateObject(abcXML, abcObject);
        /// </code>
        /// </example>
        /// <param name="XMLString">XML string that is to be converted.</param>
        /// <param name="YourClassObject">Any class object that will store the converted data.</param>
        /// <returns>Returns converted <see cref="Object"/>.</returns>
        public static Object CreateObject(string XMLString, Object YourClassObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(YourClassObject.GetType());
            //The StringReader will be the stream holder for the existing XML file 
            YourClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            //initially deserialized, the data is represented by an object without a defined type 
            return YourClassObject;
        }
    }
}
