using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace CSharpMultiPartPostHandler.RestClient
{
    public class MethodParameterSet : SortedSet<IMethodParameter>
    {
        private class MethodParameterComparer : IComparer<IMethodParameter>
        {
            public int Compare(IMethodParameter x, IMethodParameter y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }

        public MethodParameterSet()
            : base(new MethodParameterComparer())
        {
        }

        public MethodParameterSet(IEnumerable<IMethodParameter> collection)
            : base(collection, new MethodParameterComparer())
        {
        }

        internal MethodParameterSet(string formUrlEncodedValue)
            : base(new MethodParameterComparer())
        {
            if (formUrlEncodedValue == null)
                throw new ArgumentNullException("formUrlEncodedValue");

            if (formUrlEncodedValue.Contains("&") || formUrlEncodedValue.Contains("="))
            {
                var pairs = formUrlEncodedValue.Split('&');
                foreach (var pair in pairs)
                {
                    var nameValue = pair.Split('=');
                    if (nameValue.Length < 2)
                        continue;

                    this.Add(nameValue[0], System.Net.WebUtility.UrlDecode(nameValue[1]));
                }
            }
        }

        public void Add(string name, long value, long? defaultValue = null)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (name.Trim().Length == 0)
                throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

            if (!defaultValue.HasValue || defaultValue.Value != value)
                Add(new StringMethodParameter(name, value));
        }

        public void Add(string name, double value, double? defaultValue = null)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (name.Trim().Length == 0)
                throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

            if (!defaultValue.HasValue || defaultValue.Value != value)
                Add(new StringMethodParameter(name, value));
        }

        public void Add(string name, int value, int? defaultValue = null)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (name.Trim().Length == 0)
                throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

            if (!defaultValue.HasValue || defaultValue.Value != value)
                Add(new StringMethodParameter(name, value));
        }

        public void Add(string name, bool value, bool? defaultValue = null)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (name.Trim().Length == 0)
                throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

            if (!defaultValue.HasValue || defaultValue.Value != value)
                Add(new StringMethodParameter(name, value));
        }

        public void Add(string name, string value, string defaultValue = null)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (name.Trim().Length == 0)
                throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

            if (defaultValue != value)
                Add(new StringMethodParameter(name, value));
        }

        public void Add(string name, byte[] value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (name.Trim().Length == 0)
                throw new ArgumentException("Parameter name cannot be empty or whitespace.", "name");

            if (value != null)
                Add(new BinaryMethodParameter(name, value));
        }

        internal string ToFormUrlEncoded()
        {
            return String.Join("&", this.Where(c => c is StringMethodParameter).Select(c => String.Format("{0}={1}", c.Name, Encode(c))));
        }

        internal string ToAuthorizationHeader()
        {
            return String.Join(",", this.Select(c => String.Format("{0}=\"{1}\"", c.Name, Encode(c))));
        }

        private string Encode(IMethodParameter p)
        {
            return UrlEncoder.Encode(((StringMethodParameter)p).Value);
        }
    }
}

