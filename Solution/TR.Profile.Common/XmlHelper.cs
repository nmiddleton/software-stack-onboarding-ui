///
///                           $Id: XsltHelper.cs 13936 2010-11-24 14:57:22Z neil.middleton $
///              $LastChangedDate: 2010-11-24 14:57:22 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13936 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/XsltHelper.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Xsl;
using System.Xml;
using System.IO;
using System.Text;
using System.Xml.Schema;
using System.Diagnostics;

namespace TR.Profile.Common
{
    public static class XmlHelper
    {
        public static string Transform(string pXslt, string pXml)
        {
            if (String.IsNullOrWhiteSpace(pXslt) || String.IsNullOrWhiteSpace(pXml))
                return null;

            XslCompiledTransform xslt = new XslCompiledTransform();
            using (TextReader textReader = new StringReader(pXslt))
            {
                using (XmlReader reader = new XmlTextReader(textReader))
                {
                    xslt.Load(reader);
                }
            }

            StringBuilder result = new StringBuilder();
            using (TextReader textReader = new StringReader(pXml))
            {
                using (XmlReader reader = new XmlTextReader(textReader))
                {
                    using (TextWriter textWriter = new StringWriter(result))
                    {
                        using (XmlWriter writer = new XmlTextWriter(textWriter))
                        {
                            xslt.Transform(reader, writer);
                        }
                    }
                }
            }
            return result.ToString();
        }

		public static void ValidateXml(XmlReader pXmlReader, string pXsdFilename)
		{
			if (pXmlReader == null || String.IsNullOrWhiteSpace(pXsdFilename))
				throw new ArgumentException();

			XmlReader xmlReader;

			XmlDocument xmlDocument = new XmlDocument();
			using (pXmlReader)
			{
				xmlDocument.Load(pXmlReader);
			}

			XmlSchemaSet xmlSchemas = new XmlSchemaSet();
			using (xmlReader = XmlReader.Create(pXsdFilename))
			{
				xmlSchemas.Add("", xmlReader);
			}

			Debug.WriteLine(xmlDocument.ToString());
			Debug.WriteLine("=================");
			Debug.WriteLine(xmlSchemas.ToString());
			
			xmlDocument.Schemas = xmlSchemas;
			xmlDocument.Validate(
				(o, e) =>
				{
					throw e.Exception;
				});
		}

		public static void ValidateXml(string pXml, string pXmlSchemaFilename)
		{
			if (String.IsNullOrWhiteSpace(pXml) || String.IsNullOrWhiteSpace(pXmlSchemaFilename))
				throw new ArgumentException();

			ValidateXml(XmlReader.Create(new StringReader(pXml)), pXmlSchemaFilename);
		}

	}

}
