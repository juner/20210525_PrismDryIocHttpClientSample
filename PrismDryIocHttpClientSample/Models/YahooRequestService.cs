using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrismDryIocHttpClientSample.Models
{
    public class YahooRequestService
    {
        readonly IHttpClientFactory HttpClientFactory;
        public YahooRequestService(IHttpClientFactory HttpClientFactory)
            => this.HttpClientFactory = HttpClientFactory;
        public async Task<string> GetAsync()
        {
            var Client = HttpClientFactory.CreateClient("yahoo");
            var Message = await Client.GetAsync("/");
            using var Stream = await Message.Content.ReadAsStreamAsync();
            using var Reader = new StreamReader(Stream);
            return await Reader.ReadToEndAsync();
        }
    }
}
