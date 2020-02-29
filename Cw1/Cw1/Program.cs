using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        public static async Task<HttpResponseMessage> getPageContent(string url) {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            return response;
        }

        public static async Task Main(string[] args)
        {
            try
            {
                var task = await getPageContent(args[0]);
                var str = getEmails(task.Content.ReadAsStringAsync());
                Console.WriteLine("==== Result ==========");
                Console.WriteLine(str);
                Console.WriteLine("==== End of result ===");
            } catch(Exception e)
            {
                Console.WriteLine("Address Incorrect", e);
            }
        }

        public static string getEmails(Task<string> content)
        {
            Regex reg = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
            Match match;
            string result = "";


            for (match = reg.Match(content.Result); match.Success; match = match.NextMatch())
            {
                if (!result.Contains(match.Value))
                {
                    result += match.Value + " ";
                }
            }

            return result;
        } 
    }
}
