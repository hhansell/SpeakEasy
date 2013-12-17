using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace SpeakEasy
{
    public abstract class HttpRequest : IHttpRequest
    {
        private readonly List<Header> headers = new List<Header>();

        protected HttpRequest(Resource resource, IRequestBody body)
        {
            Resource = resource;
            Body = body;

            AllowAutoRedirect = true;
        }

        public Resource Resource { get; private set; }

        public IRequestBody Body { get; private set; }

        public IUserAgent UserAgent { get; set; }

        public IWebProxy Proxy { get; set; }

        public X509CertificateCollection ClientCertificates { get; set; }

        public int? MaximumAutomaticRedirections { get; set; }

        public bool HasUserAgent
        {
            get { return UserAgent != null; }
        }

        public abstract string HttpMethod { get; }

        public int NumHeaders
        {
            get { return headers.Count; }
        }

        public IEnumerable<Header> Headers
        {
            get { return headers; }
        }

        public ICredentials Credentials { get; set; }

        public bool AllowAutoRedirect { get; set; }

        public CookieContainer CookieContainer { get; set; }

        public void AddHeader(string name, string value)
        {
            headers.Add(new Header(name, value));
        }

        public abstract string BuildRequestUrl(IArrayFormatter arrayFormatter);

        public abstract override string ToString();
    }
}