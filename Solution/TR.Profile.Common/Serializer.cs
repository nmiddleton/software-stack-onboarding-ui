///
///                           $Id: Serializer.cs 14672 2011-01-18 10:26:34Z neil.middleton $
///              $LastChangedDate: 2011-01-18 10:26:34 +0000 (Tue, 18 Jan 2011) $
///          $LastChangedRevision: 14672 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/Serializer.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace TR.Profile.Common
{
    public static class Serializer
    {
        private const string XML_EXTENSION = ".xml";

        public static string Serialize(object pObject)
        {
            StringBuilder result = new StringBuilder();
            using (StringWriter writer = new StringWriter(result, System.Globalization.CultureInfo.InvariantCulture))
            {
                XmlSerializer serializer = new XmlSerializer(pObject.GetType());
                serializer.Serialize(writer, pObject);
            }
            return result.ToString();
        }
		public static string SerializeUtf8(object pObject)
		{
			Encoding UTF_8_ENCODING = new UTF8Encoding();
			string result;

			XmlSerializer xmlSerializer = new XmlSerializer(pObject.GetType());
			using (Stream stream = new MemoryStream())
			{
				using (XmlWriter writer = new XmlTextWriter(stream, UTF_8_ENCODING))
				{
					xmlSerializer.Serialize(writer, pObject);
					writer.Flush();

					using (TextReader reader = new StreamReader(stream, UTF_8_ENCODING))
					{
						stream.Position = 0;
						result = reader.ReadToEnd();
					}
				}
			}
			return result;
		}
		public static string Serialize(object pObject, string pNamespace)
        {
            XmlAttributes xmlAttribute = new XmlAttributes();
            xmlAttribute.XmlRoot = new XmlRootAttribute();
            xmlAttribute.XmlRoot.Namespace = pNamespace;
            XmlAttributeOverrides xao = new XmlAttributeOverrides();
            xao.Add(pObject.GetType(), xmlAttribute);

            StringBuilder result = new StringBuilder();
            using (StringWriter writer = new StringWriter(result, System.Globalization.CultureInfo.InvariantCulture))
            {
                XmlSerializer serializer = new XmlSerializer(pObject.GetType(), xao);
                serializer.Serialize(
                    writer,
                    pObject,
                    new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty, pNamespace) }));
            }
            return result.ToString();
        }
        public static string Serialize(object pObject, string pNamespace, string pName)
        {
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result, System.Globalization.CultureInfo.InvariantCulture);
            XmlSerializer serializer = new XmlSerializer(pObject.GetType());

            serializer.Serialize(
                writer,
                pObject,
                new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(pName, pNamespace) }));

            return result.ToString();
        }

        public static T Deserialize<T>(string pXml)
        {
            T result = default(T);
            using (StringReader reader = new StringReader(pXml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(reader);
            }
            return result;
        }

        //public static string CreateFileName(string pPath, string pName)
        //{
        //    return Path.Combine(pPath, pName) + XML_EXTENSION;
        //}

    }

}
