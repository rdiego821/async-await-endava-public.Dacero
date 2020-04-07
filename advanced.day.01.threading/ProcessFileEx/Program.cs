using System;
using System.Threading;

namespace ProcessFileEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread tr = new Thread(new FileExec().ReadFilesInDirectory);
            tr.Start();

        }
    }
}
