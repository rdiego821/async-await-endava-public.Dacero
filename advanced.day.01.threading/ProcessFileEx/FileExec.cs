using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Permissions;

namespace ProcessFileEx
{
    public class FileExec
    {
        private const string Path = @"C:\AsyncTraining\Exercise1\async-await-endava-public.Dacero\advanced.day.01.threading\MyFiles\";
        public Queue<string> files = new Queue<string>();
        public List<StreamReader> OutputFiles = new List<StreamReader>();
        public int NumberOfFiles = 0;

        public void ReadFile(string FileName)
        {
            using (StreamReader sr = File.OpenText(Path + FileName))
            {
                string s;
                Console.WriteLine($"File name: {FileName} - Content: ");
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        // Consumer
        public void ProcessFiles()
        {
            while(files.Count > 0)
                ReadFile(files.Dequeue());
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void WatchDirectory()
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = Path;

                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                watcher.Filter = "*.txt";
                watcher.Created += EnqueueFile;
                watcher.EnableRaisingEvents = true;
                while (NumberOfFiles <= 10) ;
            }
        }

        // Define the event handlers. Producer
        private void EnqueueFile(object source, FileSystemEventArgs e)
        {
            files.Enqueue(e.Name);
            NumberOfFiles++;
        }
    }
 }

