using MimeSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMultiPartPostHandler.RestClient
{
    public class BinaryFile
    {
        private readonly byte[] data;
        private readonly string fileName;
        private readonly string mimeType;

        public BinaryFile(byte[] data, string fileName = null, string mimeType = null)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            this.data = data;
            this.fileName = fileName ?? "file";
            this.mimeType = mimeType ?? GetMimeType(this.fileName);
        }

        public BinaryFile(Stream stream, string fileName = null, string mimeType = null)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (!stream.CanRead || !stream.CanSeek)
                throw new ArgumentException("The stream must be readable and seekable.", "stream");

            using (stream)
            {
                data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
            }

            this.fileName = fileName ?? "file";
            this.mimeType = mimeType ?? GetMimeType(fileName);
        }

        public byte[] Data { get { return data; } }

        public string FileName { get { return fileName; } }

        public string MimeType { get { return mimeType; } }

        private static string GetMimeType(string fileName)
        {
            var mime = new Mime();
            return mime.Lookup(fileName);

        }
    }
}
