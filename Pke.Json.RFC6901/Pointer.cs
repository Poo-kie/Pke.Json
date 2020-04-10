using System;
namespace Pke.Json.RFC6901
{
    /// <summary>
    /// An RFC6901 JSON Pointer object
    /// </summary>
    /// <remarks>https://tools.ietf.org/html/rfc6901</remarks>
    public class Pointer
    {
        public Pointer()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>https://tools.ietf.org/html/rfc6901#section-4</remarks>
        /// <param name="token"></param>
        /// <returns></returns>
        private static string Unescape(string token)
        {
            return token.Replace("~1", "/").Replace("~0", "~");
        }
    }
}
