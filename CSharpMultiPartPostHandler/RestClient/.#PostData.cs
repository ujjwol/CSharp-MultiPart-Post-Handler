using System;
using System.Collections.Generic;

namespace CSharpMultiPartPostHandler.RestClient
{
    public class PostData
    {
        private readonly MethodParameterSet parameters;

        internal PostData()
        {
            parameters = new MethodParameterSet();

        }

        public DateTimeOffset? Date { get; set; }

        public MethodParameterSet ToMethodParameterSet()
        {

            var result = new MethodParameterSet(parameters);

            if (Date != null)
                result.Add("date", Date.Value.ToUniversalTime().ToString("R"));

            return result;
        }


        public static PostData CreateStory(BinaryFile photo, string storyDesc, int likes,
                                           int dislikes, int nsfw, int spam, string apiKey)
        {
            if (photo == null)
                throw new ArgumentException("photo");

            var photos = new BinaryFile[] { photo };

            var postData = new PostData();

            postData.parameters.Add("story_description", storyDesc);
            postData.parameters.Add("story_likes", likes);
            postData.parameters.Add("story_dislikes", dislikes);
            postData.parameters.Add("story_nsfw", nsfw);
            postData.parameters.Add("story_spam", spam);
            postData.parameters.Add("api_key", apiKey);

            postData.parameters.Add(new BinaryMethodParameter("image1", photo.Data, photo.FileName, photo.MimeType));

            return postData;

        }

        public static PostData CreateNewRequestPostData(BinaryFile file, Dictionary<string, string> parameters)
        {
            return null;
        }


        public static PostData CreateNewRequestPostData(List<BinaryFile> files, Dictionary<string, string> parameters)
        {
            return null;
        }

    }
}

