This is the basic framework for a server.  An application layer can be added to this to add functionality.  This server is designed to take in data from a client, make a Request, and send that Request to the application layer, which needs to include a ResponseHandler.  The handler will then return a Reply to the server, which will send the data of the reply back to the client.

The public classes that can be used are IResponseHandler, Reply, Request, Server, and ServerInfo.

IResponseHandler - Interface that is needed to run the server.  Will take in a Request and return a Reply.
	Reply Execute(Request)

Reply - Represents a reply from the server.  Holds all three parts of an HTTP reply.
	byte[] StartingLine
	byte[] Headers
	byte[] Body
	Reply()
	byte ReplyMessage()

Server - Represents a server and contains the logic for it.
	Server Start(ServerInfo)
	void HandleClients()
	bool Stop()

ServerInfo - Contains all of the information needed to start the server.
	int Port
	IResponseHandler HTTPHandler
	int Timeout
	ServerInfo(int, IResponseHandler, int)