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

        public Request Build()
        {
            return request;
        }

        public RequestBuilder SetHttpVersion(string httpVersion)
        {
            request.HttpVersion = httpVersion;
            return this;
        }

        public RequestBuilder SetUri(string uri)
        {
            request.Uri = uri;
            return this;
        }

        public RequestBuilder SetMethod(HttpMethod method)
        {
            request.Method = method;
            return this;
        }

        public RequestBuilder AddHeader(string header)
        {
            request.Headers.Add(header);
            return this;
        }

        public RequestBuilder SetBody(byte[] body)
        {
            request.Body = body;
            return this;
        }

        public RequestBuilder SetBody(string bodyMessage)
        {
            request.Body = Encoding.UTF8.GetBytes(bodyMessage);
            return this;
        }
    }
}
