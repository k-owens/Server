using System.Collections.Generic;

namespace Server.Core
{
    public class Router : IRequestHandler
    {
        private List<RoutingInfo> _routingInfo = new List<RoutingInfo>();
        private IRequestHandler _defaultHandler;

        public Router Route(HttpMethod method, string uri, IRequestHandler handlerToRun)
        {
            var routingInfo = new RoutingInfo(method, uri, handlerToRun);
            _routingInfo.Add(routingInfo);
            return this;
        }

        public Router Default(IRequestHandler defaultHandler)
        {

            _defaultHandler = defaultHandler;
            return this;
        }

        public Response HandleRequest(Request request)
        {
            foreach (var route in _routingInfo)
            {
                if (route.Method.Equals(request.Method) && route.Uri.Equals(request.Uri))
                    return route.HandlerToRun.HandleRequest(request);
            }
            return _defaultHandler.HandleRequest(request);
        }
    }
}
