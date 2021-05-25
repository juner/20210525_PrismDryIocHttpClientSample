using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrismDryIocHttpClientSample.Models
{
    public class ExampleRequestService
    {
        readonly IHttpClientFactory HttpClientFactory;
        public ExampleRequestService(IHttpClientFactory HttpClientFactory)
            => this.HttpClientFactory = HttpClientFactory;
        public async Task<string> GetAsync()
        {
            var Client = HttpClientFactory.CreateClient("example");
            var Message = await Client.GetAsync("/");
            using var Stream = await Message.Content.ReadAsStreamAsync();
            using var Reader = new StreamReader(Stream);
            return await Reader.ReadToEndAsync();
        }
    }
}
