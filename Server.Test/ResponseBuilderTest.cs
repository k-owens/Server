using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Core;

namespace Server.Test
{
    [TestClass]
    public class ResponseBuilderTest
    {

        [TestMethod]
        public void CanSetStatusCode()
        {
            var response = new ResponseBuilder()
                .SetStatusCode(404)
                .Build();

            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public async Task CanSetBody()
        {
            var response = new ResponseBuilder()
                .SetBody(Encoding.UTF8.GetBytes("Body text"))
                .Build();

            Assert.IsNotNull(response.Body);
            Assert.AreEqual("Body text", await response.GetBodyAsStringAsync());
        }

        [TestMethod]
        public void ItCanSetTheContentType()
        {
            var response = new ResponseBuilder()
                .SetContentType("text/html")
                .Build();

            Assert.AreEqual("text/html", response.ContentType);

        }
    }
}