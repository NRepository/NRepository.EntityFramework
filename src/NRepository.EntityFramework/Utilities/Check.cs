namespace NRepository.EntityFramework.Utilities
{
    using System;

    internal class Check
    {
        public static T NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static T? NotNull<T>(T? value, string parameterName) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static string NotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                var msg = string.Format("The argument '{0}' cannot be null, empty or contain only white space.", parameterName);
                throw new ArgumentException(msg, parameterName);
            }

            return value;
        }
    }

}
