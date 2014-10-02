using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMultiPartPostHandler.RestClient
{
    public class ApiMethod
    {
        private readonly string methodUrl;
        private readonly HttpMethod httpMethod;
        private readonly MethodParameterSet parameters;

        public ApiMethod(string methodUrl, HttpMethod httpMethod, MethodParameterSet parameters = null)
        {
            if (methodUrl == null)
                throw new ArgumentNullException("methodUrl");

            if (methodUrl.Length == 0)
                throw new ArgumentException("Method URL cannot be empty.", "methodUrl");

            if (httpMethod != HttpMethod.Get && httpMethod != HttpMethod.Post)
                throw new ArgumentException("The http method must be either GET or POST.", "httpMethod");

            this.methodUrl = methodUrl;
            this.httpMethod = httpMethod;
            this.parameters = parameters ?? new MethodParameterSet();
        }

        public string Url { get { return methodUrl; } }

        public HttpMethod HttpMethod { get { return httpMethod; } }

        public MethodParameterSet Parameters { get { return parameters; } }

    }
}
