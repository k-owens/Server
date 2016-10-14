using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core
{
    public class RequestParser
    {
        private RequestBuilder requestBuilder;

        public RequestParser()
        {
            requestBuilder = new RequestBuilder();
        }

        public Request BuildRequestFromClientData(byte[] requestMessage)
        {
            if (requestMessage.Length == 0)
            {
                SetRequestToNoValues();
            }
            else
            {
                ParseMessageBytes(requestMessage);
            }
            return requestBuilder.Build();
        }

        private void SetRequestToNoValues()
        {
            requestBuilder.SetMethod(HttpMethod.Get);
            requestBuilder.SetUri("");
            requestBuilder.SetHttpVersion("");
            requestBuilder.SetBody(new byte[0]);
        }

        private void ParseMessageBytes(byte[] requestMessage)
        {
            string[] messageLines = Encoding.UTF8.GetString(requestMessage).Split('\n');
            var firstLine = messageLines[0].Substring(0, messageLines[0].Length - 1);
            ParseHeaders(messageLines);
            ParseStartingLine(firstLine);
            ParseMessageBody(requestMessage);
        }

        private void ParseHeaders(string[] messageLines)
        {
            var headersList = DivideHeaders(messageLines);
            foreach (var header in headersList)
            {
                requestBuilder.AddHeader(header);
            }
        }

        private void ParseMessageBody(byte[] requestMessage)
        {
            var index = Encoding.UTF8.GetString(requestMessage).IndexOf("\r\n\r\n");
            if (index == -1)
                requestBuilder.SetBody(new byte[0]);
            else
                requestBuilder.SetBody(BodyOfMessageFromClientData(index + 4, requestMessage));
        }

        private void ParseStartingLine(string firstLine)
        {
            var requestLine = firstLine.Split(' ', ' ');
            requestBuilder.SetMethod(ConvertStringToHttpMethod(requestLine[0]));
            requestBuilder.SetUri(requestLine[1]);
            requestBuilder.SetHttpVersion(requestLine[2]);
        }

        private HttpMethod ConvertStringToHttpMethod(string method)
        {
            switch (method)
            {
                case "GET":
                    return HttpMethod.Get;

                case "OPTIONS":
                    return HttpMethod.Options;

                case "POST":
                    return HttpMethod.Post;

                case "PUT":
                    return HttpMethod.Put;

                case "DELETE":
                    return HttpMethod.Delete;

                case "HEAD":
                    return HttpMethod.Head;

                default:
                    throw new Exception("No mapping implemented for http method.");
            }
        }

        private List<string> DivideHeaders(string[] allLines)
        {
            if (allLines.Length < 3)
                return new List<string>();

            var headers = new List<string>();
            for (int i = 0; i < allLines.Length - 3; i++)
            {
                if (allLines[i + 1].Length > 0)
                    headers.Add(allLines[i + 1].Substring(0, allLines[i + 1].Length - 1));
            }
            return headers;
        }

        private byte[] BodyOfMessageFromClientData(int startIndex, byte[] message)
        {
            var body = new byte[message.Length - startIndex];
            for (var index = startIndex; index < message.Length; index++)
            {
                body[index - startIndex] = message[index];
            }
            return body;
        }
    }
}
