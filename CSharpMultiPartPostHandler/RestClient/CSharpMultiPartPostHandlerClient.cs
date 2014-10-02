using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpMultiPartPostHandler.RestClient
{
    //This is Client
    public class CSharpMultiPartPostHandlerClient : CSharpMultiPartPostHandlerClientBase
    {
        private bool disposed;
        private readonly string apiKey;


        public CSharpMultiPartPostHandlerClient()
            : base()
        {

        }

        public Task<dynamic> CreateStoryAsync(string apiUrl, string methodName, PostData postData, CancellationToken cancellationToken)
        {
            if (disposed)
                throw new ObjectDisposedException("PreCapricaClient");

            if (postData == null)
                throw new ArgumentNullException("postData");

            return CallApiMethodAsync<dynamic>(new PostApiMethod(apiUrl, methodName, HttpMethod.Post, postData.ToMethodParameterSet()), cancellationToken, null);
            
        }


    }
}