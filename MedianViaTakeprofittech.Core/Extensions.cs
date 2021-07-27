using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MedianViaTakeprofittech.Application {
    public static class Extensions {
        /// <summary>
        /// returns bytes array in ASCII encoding
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static byte[] ToByteArray<T>(this T t) => (t is null) ?
            Array.Empty<byte>() : Encoding.ASCII.GetBytes($"{t}\n");
        /// <summary>
        /// decorates call to function with try catch operator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static byte[] SafeToByteArray<T>(this T t) {
            try {
                return ToByteArray<T>(t);
            }
            catch (Exception) {
                return Array.Empty<byte>(); ;
            }
        }
        /// <summary>
        /// returns string with integers characters
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToIntegers(this string s) => Regex.Replace(s, @"[^0-9-]+", string.Empty);
        /// <summary>
        /// decorates call to function with try catch operator
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string SafeToIntegers(this string s) {
            try {
                return Regex.Replace(s, @"[^0-9-]+", string.Empty);
            }
            catch (Exception) {
                return string.Empty;
            }
        }
        /// <summary>
        /// converts raw string to integer value from integers characters
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s) => int.TryParse(s.SafeToIntegers(), out int result) ? 
            result : 
            throw new Exception($"error on parsing {s}");
    }
}