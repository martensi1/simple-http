using PilotAppLib.Http;
using System;
using Xunit;

namespace PilotAppLib.SimpleHttp.Tests
{
    public class UrlTests
    {
        [Fact]
        public void Constructor()
        {
            string value = "https://google.com";
            var url = new Url(value);

            Assert.Equal(value, url.ToString());
        }

        [Fact]
        public void ImplicitConstructor()
        {
            string value = "https://google.com";
            Url url = value;

            Assert.Equal(value, url.ToString());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NullOrEmptyUrl(string urlValue)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                new Url(urlValue);
            });

            Assert.Equal("url", ex.ParamName);
            Assert.Equal("Invalid URL (Parameter 'url')", ex.Message);
        }
    }
}
