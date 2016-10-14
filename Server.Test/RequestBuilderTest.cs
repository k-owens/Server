using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Core;

namespace Server.Test
{
    [TestClass]
    public class RequestBuilderTest
    {
        [TestMethod]
        public void ItCanSetMethodToGet()
        {
            var request = new RequestBuilder()
                .SetMethod(HttpMethod.Get)
                .Build();

            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        public void ItCanSetMethodToOptions()
        {
            var request = new RequestBuilder()
                .SetMethod(HttpMethod.Options)
                .Build();

            Assert.AreEqual(HttpMethod.Options, request.Method);
        }

        [TestMethod]
        public void ItCanSetMethodToPost()
        {
            var request = new RequestBuilder()
                .SetMethod(HttpMethod.Post)
                .Build();

            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        public void ItCanSetMethodToPut()
        {
            var request = new RequestBuilder()
                .SetMethod(HttpMethod.Put)
                .Build();

            Assert.AreEqual(HttpMethod.Put, request.Method);
        }

        [TestMethod]
        public void ItCanSetMethodToDelete()
        {
            var request = new RequestBuilder()
                .SetMethod(HttpMethod.Delete)
                .Build();

            Assert.AreEqual(HttpMethod.Delete, request.Method);
        }

        [TestMethod]
        public void ItCanSetMethodToHead()
        {
            var request = new RequestBuilder()
                .SetMethod(HttpMethod.Head)
                .Build();

            Assert.AreEqual(HttpMethod.Head, request.Method);
        }

        [TestMethod]
        public void ItCanSetThePath()
        {
            var request = new RequestBuilder()
                .SetUri("/")
                .Build();

            Assert.AreEqual("/", request.Uri);
        }

        [TestMethod]
        public void ItCanSetTheBody()
        {
            var request = new RequestBuilder()
                .SetBody("This is the body")
                .Build();

            Assert.AreEqual("This is the body", Encoding.UTF8.GetString(request.Body));
        }
    }
}