using System;
using System.Net.Http;

namespace CSharpMultiPartPostHandler.RestClient
{
    public class PostApiMethod : ApiMethod
    {
        public PostApiMethod(string apiUrl, string methodName, HttpMethod httpMethod, MethodParameterSet parameters = null)
            : base(String.Format("{0}/{1}", apiUrl, methodName), httpMethod, parameters)
        {
            if (apiUrl == null)
                throw new ArgumentNullException("apiUrl");

            if (methodName == null)
                throw new ArgumentNullException("methodName");

        }
    }
}

