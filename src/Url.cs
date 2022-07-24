using System;

namespace PilotAppLib.Http
{
    /// <summary>
    /// Provides an object representation of an uniform resource locator (URL)
    /// </summary>
    public class Url
    {
        private readonly string _content;


        /// <summary>Initializes a new instance of the <see cref="Url"/> class</summary>
        /// <param name="url">The URL as a string</param>
        public Url(string url)
        {
            CheckIfEmptyOrThrow(url, nameof(url));
            _content = url;
        }

        /// <summary>Initializes a new instance of the <see cref="Url"/> class</summary>
        /// <param name="url">The URL as a string</param>
        public static implicit operator Url(string url)
        {
            if (url == null)
                return null;

            return new Url(url);
        }


        /// <summary>
        /// Returns the URL as string
        /// </summary>
        public override string ToString()
        {
            return _content.ToString();
        }


        private void CheckIfEmptyOrThrow(string url, string paramName)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Invalid URL", paramName);
            }
        }
    }
}
