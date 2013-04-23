using System;
using System.Globalization;
using System.Linq;

namespace SpeakEasy
{
    public class Parameter
    {
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }

        public object Value { get; private set; }

        public bool HasValue
        {
            get { return Value != null; }
        }

        public string ToQueryString()
        {
            if (!HasValue)
            {
                var message = string.Format(
                    "Could not convert the parameter {0} to a query string because it did not have a value", Name);

                throw new NotSupportedException(message);
            }

            var enumerable = Value as Array;

            var value = enumerable != null
                ? string.Join(",", enumerable.Cast<object>().Select(ToQueryStringValue))
                : ToQueryStringValue(Value);

            return string.Concat(Name, "=", Uri.EscapeUriString(value));
        }

        private string ToQueryStringValue(object value)
        {
            if (value is DateTime)
            {
                return ((DateTime)value).ToString("s", CultureInfo.InvariantCulture);
            }

            return value.ToString();
        }
    }
}