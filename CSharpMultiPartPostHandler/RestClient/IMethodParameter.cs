using System;
using System.Net.Http;

namespace CSharpMultiPartPostHandler.RestClient
{
    public interface IMethodParameter : IEquatable<IMethodParameter>
    {
        string Name { get; }
        HttpContent AsHttpContent();

    }
}

