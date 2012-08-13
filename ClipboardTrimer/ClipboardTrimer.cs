using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ClipboardTrimer
{
    public class ClipboardTrimer
    {
        public static string Execute(string inputClipboardText, string prefix)
        {
            List<string> clipboardLineList = new List<string>();

            using (StringReader reader = new StringReader(inputClipboardText))
            {
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    clipboardLineList.Add(line);
                }
            }

            List<string> trimeEndLineList = new List<string>();
            foreach (string line in clipboardLineList)
            {
                trimeEndLineList.Add(line.TrimEnd());
            }

            List<string> unEmptyList = new List<string>();
            foreach (string line in trimeEndLineList)
            {
                if (string.IsNullOrEmpty(line) == false)
                {
                    unEmptyList.Add(line);
                }
            }

            List<string> withPrefixList = new List<string>();
            foreach (string line in unEmptyList)
            {
                withPrefixList.Add(string.Format("{0}{1}", prefix, line));
            }

            List<string> trimedList = new List<string>();
            foreach (string line in withPrefixList)
            {
                trimedList.Add(line.Trim());
            }

            List<string> ret = new List<string>();
            foreach (string line in trimedList)
            {
                if (string.IsNullOrEmpty(line) == false)
                {
                    ret.Add(line);
                }
            }

            string clipboardSetText = null;
            using (StringWriter writer = new StringWriter())
            {
                foreach (string line in ret.Take(ret.Count - 1))
                {
                    writer.WriteLine(line);
                }
                //最終行は改行なしとする
                writer.Write(ret[ret.Count - 1]);

                clipboardSetText = writer.ToString();

            }
            return clipboardSetText;
        }
    }
}
