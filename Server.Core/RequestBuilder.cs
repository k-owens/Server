using System.Collections.Generic;
using System.Text;

namespace Server.Core
{
    public class RequestBuilder
    {
        private string _httpVersion;
        private string _uri;
        private HttpMethod _method;
        private List<string> _headers;
        private byte[] _body;

        public RequestBuilder()
        {
            _httpVersion = "HHTP/1.1";
            _uri = "/";
            _method = HttpMethod.Get;
            _headers = new List<string>();
            _body = new byte[0];
        }

        public Request Build()
        {
            return new Request(_httpVersion, _uri, _method, _headers, _body);
        }

        public RequestBuilder SetHttpVersion(string httpVersion)
        {
            _httpVersion = httpVersion;
            return this;
        }

        public RequestBuilder SetUri(string uri)
        {
            _uri = uri;
            return this;
        }

        public RequestBuilder SetMethod(HttpMethod method)
        {
            _method = method;
            return this;
        }

        public RequestBuilder AddHeader(string header)
        {
            _headers.Add(header);
            return this;
        }

        public RequestBuilder SetBody(byte[] body)
        {
            _body = body;
            return this;
        }

        public RequestBuilder SetBody(string bodyMessage)
        {
            _body = Encoding.UTF8.GetBytes(bodyMessage);
            return this;
        }
    }
}
