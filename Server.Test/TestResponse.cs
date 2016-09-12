using System;
using Server.Core;
using System.Text;

namespace Server.Test
{
    public class TestResponse : IResponseHandler
    {
        public Reply Execute(Request request)
        {
            Reply reply = new Reply();
            reply.StartingLine = Encoding.UTF8.GetBytes("HTTP/1.1 200 OK\r\n");
            return reply;
        }
    }
}
