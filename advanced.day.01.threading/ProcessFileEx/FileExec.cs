using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ProcessFileEx
{
    public class FileExec
    {
        private const string Path = "..\\..\\..\\MyFiles";

        public void ReadFile()
        {
            if (!File.Exists(Path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(Path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                }
            }

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(Path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public void ReadFilesInDirectory()
        {
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            StringBuilder sb = new StringBuilder();
            foreach (string txtName in Directory.GetFiles(@Path, "*.txt"))
            {
                using (StreamReader sr = new StreamReader(txtName))
                {
                    sb.AppendLine(txtName.ToString());
                    sb.AppendLine("= = = = = =");
                    sb.Append(sr.ReadToEnd());
                    sb.AppendLine();
                    sb.AppendLine();
                }
            }
            using (StreamWriter outfile = new StreamWriter(mydocpath + @"\AllTxtFiles.txt"))
            {
                outfile.Write(sb.ToString());
            }
        }
    }
 }

