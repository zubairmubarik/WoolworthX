using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace MyDemoProject001.Application.Common.Extensions
{
    public static class ExtensionMethods
    {
        public static bool IsStringEmpty(this string source)
        {
            return string.IsNullOrEmpty(source) || string.IsNullOrWhiteSpace(source);
        }

        public static decimal AsDecimal(this object value)
        {
            switch (value)
            {
                case null:
                    return decimal.Zero;

                case decimal d:
                    return d;

                default:
                    try
                    {
                        return Convert.ToDecimal(value);
                    }
                    catch
                    {
                        // ignored
                    }
                    return decimal.Zero;
            }
        }

        public static T ToEnum<T>(this string text, bool ignoreCase = true)
        {
            if(typeof(T).GetTypeInfo().IsEnum)
            {
                return (T)Enum.Parse(typeof(T), text, ignoreCase);
            }
            throw new NotSupportedException($"Text must be of Enum type: {typeof(T).FullName}");
        }

        /// <summary>
        /// Deserialise a JSON string to type of T. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public static T FromJson<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static T ToJson<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string JsonFormat(this string source, bool returnError = false)
        {
            try
            {
                return JObject.Parse(source, new JsonLoadSettings { CommentHandling = CommentHandling.Load, LineInfoHandling = LineInfoHandling.Load }).ToString();

            }
            catch (Exception ex)
            {
                return returnError ? string.Format("Unable to format JSON. Reason: {0}", ex.Message) : string.Empty;
            }
        }

    }
}
