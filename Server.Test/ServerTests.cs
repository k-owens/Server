using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Core;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace Server.Test
{
    [TestClass]
    public class ServerTests
    {
        private byte[] _bytesReturned;
        private IPAddress _ipAddress;
        private IPEndPoint _ipEndPoint;

        [TestMethod]
        public void ServerCanStart()
        {
            var server = new Core.Server();
            IRequestHandler requestRouter = new TestResponse();
            ServerInfo info = new ServerInfo(8080, requestRouter, 0);
            Assert.IsTrue(server.Start(info) != null);
            server.Stop();
        }

        [TestMethod]
        public void ServerCanStop()
        {
            var server = new Core.Server();
            IRequestHandler requestRouter = new TestResponse();
            ServerInfo info = new ServerInfo(8080, requestRouter, 0);
            server.Start(info);
            Assert.IsTrue(server.Stop());
        }

        [TestMethod]
        public void IntegrationTest()
        {
            var socket = SetUpClient();
            var server = new Core.Server();
            var message = "";
            Action<object> action1 = (object obj) => { ConnectClientToServer(socket, _ipEndPoint, server); };

            Action<object> action2 =
                (object obj) => { message = CommunicateWithServer(socket, _bytesReturned, "GET / HTTP/1.1\r\n\r\n"); };

            Task t1 = new Task(action1, "");
            Task t2 = new Task(action2, "");

            t1.Start();
            System.Threading.Thread.Sleep(100);
            t2.Start();
            t2.Wait();
            CloseConnectionWithServer(socket, server);
            Assert.AreEqual("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: 11\r\n\r\nHello World", message);
        }

        private void ConnectClientToServer(Socket socket, IPEndPoint ipEndPoint, Core.Server server)
        {
            IRequestHandler requestRouter = new TestResponse();
            ServerInfo info = new ServerInfo(8080, requestRouter, 0);
            server.Start(info);
            socket.Connect(ipEndPoint);
            server.HandleClients();
        }

        private string CommunicateWithServer(Socket socket, byte[] bytesReturned, string incomingMessage)
        {
            byte[] bytesToSend = Encoding.UTF8.GetBytes(incomingMessage);
            socket.Send(bytesToSend);
            var bytesReceived = socket.Receive(bytesReturned);
            var message = Encoding.UTF8.GetString(bytesReturned).Substring(0, bytesReceived);
            return message;
        }

        private void CloseConnectionWithServer(Socket socket, Core.Server server)
        {
            server.Stop();
            socket.Close();
        }

        private Socket SetUpClient()
        {
            _bytesReturned = new byte[1024];
            _ipAddress = IPAddress.Parse("127.0.0.1");
            _ipEndPoint = new IPEndPoint(_ipAddress, 8080);
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
