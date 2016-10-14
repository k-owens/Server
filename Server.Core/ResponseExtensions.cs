using System.IO;
using System.Threading.Tasks;

namespace Server.Core
{
    public static class ResponseExtensions
    {
        public static Task<string> GetBodyAsStringAsync(this Response response)
        {
            response.Body.Position = 0;
            var streamReader = new StreamReader(response.Body ?? new MemoryStream());
            return streamReader.ReadToEndAsync();
        }
    }
}