﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessFileExample
{
    public class FileExec
    {
        private const string Path = @"C:\AsyncTraining\Exercise1\async-await-endava-public.Dacero\Day03\MyFiles\";
        public Queue<string> files = new Queue<string>();
        public int NumberOfFiles = 0;
        static SemaphoreSlim _sem = new SemaphoreSlim(4);

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

        public async Task<int> RunProcess()
        {
            return await Task.Run(() => ProcessFiles());
        }


        // Consumer
        public int ProcessFiles()
        {
            _sem.Wait();
            while (files.Count > 0)
               ReadFile(files.Dequeue());
            _sem.Release();

            return 1;
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
