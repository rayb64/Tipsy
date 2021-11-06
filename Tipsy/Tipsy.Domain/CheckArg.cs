// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>CheckArg.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/6/2021 8:01:47 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Performs simple argument checks and throws the appropriate exception
    /// </summary>
    public static class CheckArg
    {
        /// <summary>
        /// Checks that a value is not the default value for it's type.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to be checked.</param>
        public static void IsNotDefault<T>(
            T value,
            string paramName)
        {
            IsNotDefault(value, paramName, "Must not be default");
        }

        /// <summary>
        /// Checks that a value is not the default value for it's type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="errorMessage"></param>
        public static void IsNotDefault<T>(
            T value,
            string paramName,
            string errorMessage)
        {
            if (EqualityComparer<T>.Default.Equals(value, default))
            {
                if (typeof(T).IsClass || typeof(T).IsInterface)
                {
                    throw new ArgumentNullException(paramName, errorMessage);
                }
                else
                {
                    throw new ArgumentException(errorMessage, paramName);
                }
            }
        }
    }
}
