/*!	\file		XMLExtension.cs
	\author		Group 13 (Haohan Liu, Dmytro Liaska, David Rivard)
	\date		2019-03-19

    FUNCTIONAL REQUIREMENTS: Implementation for XML output Conversion for Writing new XML file
*/

using System.IO;

namespace InfixConverter
{
    public static class XMLExtension
    {
        //This method should include xml version and encoding
        public static void WriteStartDocument(this StreamWriter streamWriter)
        {
            streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        }

        //This method should add the root tag to the file
        public static void WriteStartRootElement(this StreamWriter streamWriter)
        {
            streamWriter.WriteLine("<root>");
        }

        //This method should end the root tag.
        public static void WriteEndRootElement(this StreamWriter streamWriter)
        {
            streamWriter.WriteLine("</root>");
        }

        //This method should add the element tag to the file
        public static void WriteStartElement(this StreamWriter streamWriter)
        {
            streamWriter.WriteLine("\t<elements>");
        }

        //This method should end element tag
        public static void WriteEndElement(this StreamWriter streamWriter)
        {
            streamWriter.WriteLine("\t</elements>");
        }

        //This method should add each attribute with its values.
        public static void WriteAttribute(this StreamWriter streamWriter, string attr, string val)
        {
            streamWriter.WriteLine("\t\t<{0}>{1}</{2}>", attr, val, attr);
        }

    }// class XMLExtension
}// namespace InfixConverter
