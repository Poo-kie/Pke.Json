namespace Pke.Json.RFC6901
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>https://tools.ietf.org/html/rfc6901#section-4</remarks>
    internal static class ReferenceTokenTransforms
    {
        public static string FromReferenceToken(string token)
        {
            return token.Replace("~1", "/").Replace("~0", "~");
        }
    }
}
