﻿namespace Server.Core
{
    public interface IHttpHandler
    {
        Reply Execute(Request request);
    }
}
