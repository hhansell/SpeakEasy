using System.Net;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(OptionsRequest))]
    class OptionsRequestSpecification
    {
        //public class when_building_web_request : with_options_request
        //{
        //    It should_have_options_method = () =>
        //        request.HttpMethod.ShouldEqual("OPTIONS");

        //    static WebRequest webRequest;
        //}

        //public class with_serializer : WithFakes
        //{
        //    Establish context = () =>
        //        transmissionSettings = An<ITransmissionSettings>();

        //    protected static ITransmissionSettings transmissionSettings;
        //}

        //public class with_options_request : with_serializer
        //{
        //    Establish context = () =>
        //        request = new OptionsRequest(new Resource("http://example.com/companies"));

        //    internal static OptionsRequest request;
        //}
    }
}
