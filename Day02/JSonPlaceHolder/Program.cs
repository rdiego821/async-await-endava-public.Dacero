using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JSonPlaceHolder
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Task.Run(() => GetURI(new Uri("https://jsonplaceholder.typicode.com/posts")));
            t.Wait();

            var t1 = Task.Run(() => GetURI(new Uri("https://jsonplaceholder.typicode.com/comments?postId=1")));
            t1.Wait();

            Console.WriteLine(t.Result);
            Console.WriteLine(t1.Result);
            Console.ReadLine();
        }

        static async Task<string> GetURI(Uri u)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(u);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            }
            return response;
        }
    }
}
