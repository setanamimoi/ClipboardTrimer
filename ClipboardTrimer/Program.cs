using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace ClipboardTrimer
{
    public static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string inputClipboardText = Clipboard.GetText();
            string clipboardSetText = Execute(inputClipboardText);

            Clipboard.SetText(clipboardSetText);
        }

        public static string Execute(this string inputClipboardText)
        {
            List<string> lineList = new List<string>();
            using (var reader = new StringReader(inputClipboardText))
            {
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    lineList.Add(line);
                }
            }

            List<string> trimEndList = new List<string>();
            foreach (string line in lineList)
            {
                trimEndList.Add(line.TrimEnd());
            }

            List<string> unEmptyList = new List<string>();
            foreach (string line in trimEndList)
            {
                if (string.IsNullOrEmpty(line) == false)
                {
                    unEmptyList.Add(line);
                }
            }

            List<string> ret = new List<string>();
            foreach (string line in unEmptyList)
            {
                ret.Add(string.Format("'{0}", line));
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
