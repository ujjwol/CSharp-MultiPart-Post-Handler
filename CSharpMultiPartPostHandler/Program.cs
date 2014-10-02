using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CSharpMultiPartPostHandler.RestClient;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json;

namespace CSharpMultiPartPostHandler
{
    class Program
    {
        public static void Main(string[] args)
        {
            string filePath = Path.Combine(@"Z:\Downloads\10693810_288385031346286_591728158_n.jpg");

            byte[] fileBytes = File.ReadAllBytes(filePath);

            var fileName = Path.GetFileName(filePath);

            var uploadFile = new BinaryFile(fileBytes, fileName);

            var postParameters = new Dictionary<string, string>();

            postParameters.Add("story_description", "CLient test story description");
            postParameters.Add("story_likes", "15");
            postParameters.Add("story_dislikes", "1");
            postParameters.Add("story_nsfw", "0");
            postParameters.Add("story_spam", "0");
            postParameters.Add("api_key", "");


            var postData = PostData.CreateNewRequestPostData(uploadFile, postParameters);

            var restClient = new CSharpMultiPartPostHandlerClient();

            var response = restClient.CallApiMethodAsync<dynamic>(new PostApiMethod("...", "stories", HttpMethod.Post, postData.ToMethodParameterSet()), new CancellationToken(false), null);

        }
    }
}
