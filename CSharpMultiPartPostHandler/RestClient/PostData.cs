using System;
using System.Collections.Generic;
using System.Linq;

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

        public static PostData CreateNewRequestPostData(BinaryFile file, Dictionary<string, string> parameters)
        {
            var files = new BinaryFile[] { file };

            return CreateNewRequestPostData(files, parameters);
        }


        public static PostData CreateNewRequestPostData(IEnumerable<BinaryFile> files, Dictionary<string, string> parameters)
        {
            if (files == null)
                throw new ArgumentException("files");


            if (files.FirstOrDefault() == null)
                throw new ArgumentException("There must be at least one file to post.", "files");

            var postData = new PostData();

            foreach(KeyValuePair<string, string> parameter in parameters)
            {
                postData.parameters.Add(parameter.Key, parameter.Value);
            }

            if (files.Count() == 1)
            {
                var file = files.First();
                postData.parameters.Add(new BinaryMethodParameter(file.FileName, file.Data, file.FileName, file.MimeType));
            }
            else
            {
                int i = 0;
				foreach (var file in files)
					postData.parameters.Add(new BinaryMethodParameter(file.FileName, file.Data, file.FileName, file.MimeType));

            }

            return postData;
        }

    }
}

