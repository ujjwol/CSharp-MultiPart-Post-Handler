using System;

namespace CSharpMultiPartPostHandler.RestClient
{
    public interface ICSharpMultiPartPostHandlerClientFactory
    {
        TClient Create<TClient>(string apiKey) 
            where TClient : CSharpMultiPartPostHandlerClientBase;
    }
}

