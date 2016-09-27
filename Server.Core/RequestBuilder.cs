using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core
{
    public class RequestBuilder
    {
        private Request request;

        public RequestBuilder()
        {
            request = new Request();
        }

        public Request BuildRequest()
        {
            return request;
        }

        public void SetHttpVersion(string httpVersion)
        {
            request.HttpVersion = httpVersion;
        }

        public void SetUri(string uri)
        {
            request.Uri = uri;
        }

        public void SetMethod(string method)
        {
            request.Method = method;
        }

        public void AddHeader(string header)
        {
            request.Headers.Add(header);
        }

        public void SetBody(byte[] body)
        {
            request.Body = body;
        }

        public void SetBody(string bodyMessage)
        {
            request.Body = Encoding.UTF8.GetBytes(bodyMessage);
        }
    }
}
