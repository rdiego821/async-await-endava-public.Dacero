using System;
using System.IO;
using System.Threading;

namespace ProcessFileEx
{
    class Program
    {
        static void Main(string[] args)
        {
            FileExec file = new FileExec();
            file.WatchDirectory();

            Thread tr = new Thread(file.ProcessFiles);
            tr.Start();
            tr.Join();


            //Thread tr = new Thread(new FileExec().ReadFilesInDirectory);
            //tr.Start();

            
        }
    }
}
