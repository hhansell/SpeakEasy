namespace SpeakEasy
{
    public class RequestRunner : IRequestRunner
    {
        private readonly ITransmissionSettings transmissionSettings;

        private readonly IWebRequestGateway webRequestGateway;

        private readonly IAuthenticator authenticator;

        public RequestRunner(
            ITransmissionSettings transmissionSettings,
            IWebRequestGateway webRequestGateway,
            IAuthenticator authenticator)
        {
            this.transmissionSettings = transmissionSettings;
            this.webRequestGateway = webRequestGateway;
            this.authenticator = authenticator;
        }

        public IHttpResponse Run(IHttpRequest request)
        {
            authenticator.Authenticate(request);

            var webRequest = request.BuildWebRequest(transmissionSettings);
            return webRequestGateway.Send(webRequest, CreateHttpResponse);
        }

        public IHttpResponse CreateHttpResponse(IHttpWebResponse webResponse)
        {
            var deserializer = transmissionSettings.FindSerializer(webResponse.ContentType);
            var body = webResponse.ReadBody();

            return new HttpResponse(
                deserializer,
                body,
                webResponse.StatusCode,
                webResponse.ResponseUri,
                webResponse.Headers,
                webResponse.ContentType);
        }
    }
}