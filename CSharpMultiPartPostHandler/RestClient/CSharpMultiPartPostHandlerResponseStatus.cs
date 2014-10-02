using Newtonsoft.Json;

namespace CSharpMultiPartPostHandler.RestClient
{
    /// <summary>
    /// much better response status
    /// 
    /// </summary>
    internal class PreCapricaResponseStatus
    {
        public PreCapricaResponseStatus()
            : this(0, null)
        {
        }

        public PreCapricaResponseStatus(int code, string message)
        {
            Code = code;
            Message = message;
        }

        [JsonProperty(PropertyName = "status")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set; }
    }

    //    Example of Tumblr response
    //    {
    //        "meta": {
    //            "status": 200,
    //            "msg": "OK"
    //        },
    //        "response": {
    //            "user": {
    //                "name": "ujjwol",
    //                "likes": 21,
    //                "following": 18,
    //                "default_post_format": "html",
    //                "blogs": [
    //                        {
    //                            "title": "“LIKHĀMI” Ujjwol’s Tumblr",
    //                            "name": "ujjwol",
    //                            "posts": 142,
    //                            "url": "http://ujjwol.tumblr.com/",
    //                            "updated": 1382708843,
    //                            "description": "Namaste , I'm Ujjwol.",
    //                            "is_nsfw": false,
    //                            "ask": true,
    //                            "ask_page_title": "Ask me anything",
    //                            "ask_anon": true,
    //                            "submission_page_title": "Submit",
    //                            "can_submit": true,
    //                            "followed": false,
    //                            "can_send_fan_mail": true,
    //                            "share_likes": true,
    //                            "likes": 21,
    //                            "twitter_enabled": true,
    //                            "twitter_send": false,
    //                            "facebook_opengraph_enabled": "N",
    //                            "tweet": "Y",
    //                            "facebook": "N",
    //                            "followers": 26,
    //                            "primary": true,
    //                            "admin": true,
    //                            "messages": 2,
    //                            "queue": 0,
    //                            "drafts": 0,
    //                            "type": "public"
    //                        }
    //                ]
    //            }
    //        }
    //    }

}

