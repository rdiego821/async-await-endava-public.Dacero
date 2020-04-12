using System;
using System.Threading.Tasks;

namespace ProcessFileExample
{
    class Program
    {
        static void Main(string[] args)
        {
            FileExec file = new FileExec();
            file.WatchDirectory();

            Task<int> task = file.RunProcess();
            task.Wait();

            
        }
    }
}
