using System;
namespace Pke.Json.RFC6901
{
    public class ReferenceToken : IEquatable<ReferenceToken>
    {
        private readonly string _token;

        public ReferenceToken(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            
            _token = key.Replace("~", "~0").Replace("/", "~1");
        }

        public string Unescape()
        {
            return _token.Replace("~1", "/").Replace("~0", "~");
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ReferenceToken);
        }

        public bool Equals(ReferenceToken other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return _token.Equals(other._token, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_token);
        }

        public override string ToString()
        {
            return _token;
        }
    }
}
