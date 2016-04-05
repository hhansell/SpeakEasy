using System;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class ResourceMergerSpecification
    {
        [Subject(typeof(ResourceMerger))]
        public class when_merging_segments : with_resource_with_parameter
        {
            Because of = () =>
                merged = Subject.Merge(resource, new { name = "company-name" });

            It should_merge_values_into_resource = () =>
                merged.Path.ShouldEqual("company/company-name");

            static Resource merged;
        }

        [Subject(typeof(ResourceMerger))]
        public class when_merging_multiple_segments : with_multiple_resource_with_parameters
        {
            Because of = () =>
                merged = Subject.Merge(resource, new { name = "company-name", companyType = "public" });

            It should_merge_values_into_resource = () =>
                merged.Path.ShouldEqual("company/company-name/public");

            static Resource merged;
        }

        [Subject(typeof(ResourceMerger))]
        public class when_merging_segments_with_null_value : with_resource_with_parameter
        {
            Because of = () =>
                exception = Catch.Exception(() => Subject.Merge(resource, new { name = (string)null }));

            It should_throw_exception = () =>
                exception.ShouldBeOfExactType<ArgumentException>();

            static Exception exception;
        }

        [Subject(typeof(ResourceMerger))]
        public class when_merging_segments_of_different_case : with_resource_with_parameter
        {
            Because of = () =>
                merged = Subject.Merge(resource, new { Name = "company-name" });

            It should_merge_values_into_resource = () =>
                merged.Path.ShouldEqual("company/company-name");

            static Resource merged;
        }

        [Subject(typeof(ResourceMerger))]
        public class when_merging_null_segments : WithSubject<ResourceMerger>
        {
            Establish context = () =>
                resource = new Resource("company");

            Because of = () =>
                merged = Subject.Merge(resource, null);

            It should_return_resource = () =>
                merged.Path.ShouldEqual("company");

            static Resource resource;

            static Resource merged;
        }

        [Subject(typeof(ResourceMerger))]
        public class when_merging_null_segments_when_resource_has_segments : WithSubject<ResourceMerger>
        {
            Establish context = () =>
                resource = new Resource("company/:id");

            Because of = () =>
                exception = Catch.Exception(() => Subject.Merge(resource, null));

            It should_throw_exception = () =>
                exception.ShouldBeOfExactType<ArgumentException>();

            static Resource resource;

            static Exception exception;
        }

        [Subject(typeof(ResourceMerger))]
        public class when_merging_segments_as_parameters_when_no_segment_names_in_path : WithSubject<ResourceMerger>
        {
            Establish context = () =>
            {
                Subject.NamingConvention = new DefaultNamingConvention();
                resource = new Resource("companies");
            };

            Because of = () =>
                merged = Subject.Merge(resource, new { Filter = "nasdaq" });

            It should_add_parameters = () =>
                merged.HasParameter("Filter").ShouldBeTrue();

            static Resource resource;

            static Resource merged;
        }

        [Subject(typeof(ResourceMerger))]
        public class when_merging_extra_segments_add_as_parameters : WithSubject<ResourceMerger>
        {
            Establish context = () =>
            {
                Subject.NamingConvention = new DefaultNamingConvention();
                resource = new Resource("company/:id");
            };

            Because of = () =>
                merged = Subject.Merge(resource, new { id = 5, Filter = "ftse" });

            It should_merge_url_segments = () =>
                merged.Path.ShouldEqual("company/5");

            It should_have_parameter_with_original_casing = () =>
                merged.HasParameter("Filter").ShouldBeTrue();

            It should_only_merge_in_given_parameters = () =>
                merged.NumParameters.ShouldEqual(1);

            static Resource resource;

            static Resource merged;
        }

        public class with_resource_with_parameter : WithSubject<ResourceMerger>
        {
            Establish context = () =>
            {
                resource = new Resource("company/:name");
                Subject.NamingConvention = new DefaultNamingConvention();
            };

            protected static Resource resource;
        }

        public class with_multiple_resource_with_parameters : WithSubject<ResourceMerger>
        {
            Establish context = () =>
            {
                resource = new Resource("company/:name/:companyType");
                Subject.NamingConvention = new DefaultNamingConvention();
            };

            protected static Resource resource;
        }
    }
}