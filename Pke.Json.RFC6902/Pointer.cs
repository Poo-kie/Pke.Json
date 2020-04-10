using System;
namespace Pke.Json.RFC6902
{
    public class RFC6901Pointer
    {
        public RFC6901Pointer()
        {
        }

        
        private static string Unescape(string token)
        {
            return token.Replace("~1", "/").Replace("~0", "~");
        }
    }
}
