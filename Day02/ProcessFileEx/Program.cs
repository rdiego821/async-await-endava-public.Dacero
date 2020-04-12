using System;
using System.Threading.Tasks;

namespace ProcessFileEx
{
    class Program
    {
        static void Main(string[] args)
        {
            FileExec file = new FileExec();
            file.WatchDirectory();

            Task t1 = new Task(file.ProcessFiles);
            t1.Start();
            t1.Wait();
        }
    }
}
