
///
///                           $Id: IOHelper.cs 13936 2010-11-24 14:57:22Z neil.middleton $
///              $LastChangedDate: 2010-11-24 14:57:22 +0000 (Wed, 24 Nov 2010) $
///          $LastChangedRevision: 13936 $
///                $LastChangedBy: neil.middleton $
///                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.Common/IOHelper.cs $
///

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TR.Profile.Common
{
    public static class IOHelper
    {
        public static string ReadText(string pFileName)
        {
            if (String.IsNullOrEmpty(pFileName))
                return null;
            if (!File.Exists(pFileName))
                throw new FileNotFoundException("file not found", pFileName);

            using (StreamReader reader = File.OpenText(pFileName))
            {
                return reader.ReadToEnd();
            }
        }
        public static void WriteText(string pFileName, string pText)
        {
            if (String.IsNullOrEmpty(pFileName))
                return;

            if (File.Exists(pFileName))
            {
                File.Delete(pFileName);
            }
            using (StreamWriter writer = File.CreateText(pFileName))
            {
                writer.Write(pText);
            }
        }

        public static byte[] Read(string pFileName)
        {
            if (String.IsNullOrEmpty(pFileName))
                return null;
            if (!File.Exists(pFileName))
                throw new FileNotFoundException("file not found", pFileName);

            byte[] result = null;
            using (FileStream reader = File.OpenRead(pFileName))
            {
                result = new byte[reader.Length];
                reader.Read(result, 0, result.Length);
            }
            return result;
        }

        public static string[] GetFiles(string pPath, string pPattern)
        {
            return Directory.GetFiles(pPath, pPattern);
        }

    }

}
