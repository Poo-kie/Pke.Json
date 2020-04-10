using Newtonsoft.Json.Linq;

namespace Pke.Json.RFC6902
{
    public interface IPatch
    {
        JToken Apply(JToken to);
    }
}
