using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lorem_headless.Features.Render.Services
{
    public class Connector
        : IConnector
    {
        private ManualResetEventSlim _set;
        private string _content;

        public Connector()
        {
            _set = new ManualResetEventSlim();
        }

        public string WaitForContent()
        {
            _set.Wait(2000);
            return _content;
        }

        public async Task<string> Execute(string url, string query)
        {
            using (var client = new HttpClient())
            using (var content = new StringContent(query, Encoding.UTF8, "application/json"))
            {
                var message = await client.PostAsync(url, content);
                message.EnsureSuccessStatusCode();
                return await message.Content.ReadAsStringAsync();
            }
        }

        public void Send(string content)
        {
            _content = content;
            _set.Set();
        }
    }
}