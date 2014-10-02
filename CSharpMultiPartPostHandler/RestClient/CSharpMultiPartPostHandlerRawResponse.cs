using Newtonsoft.Json;

namespace CSharpMultiPartPostHandler.RestClient
{
    /// <summary>
    /// much better status implementation by tumblr
    /// </summary>
    internal class PreCapricaRawResponse<TResponse>
    {
        [JsonProperty(PropertyName = "meta")]
        public PreCapricaResponseStatus Status { get; set; }

        [JsonProperty(PropertyName = "response")]
        public TResponse Response { get; set; }

    }
}

