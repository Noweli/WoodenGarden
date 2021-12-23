using System.Text;
using Newtonsoft.Json;

namespace WoodenGardenFront.Helpers.ServiceHelpers;

internal static class HttpContentHelper
{
    internal static StringContent GetStringContentAppJson(this object @object)
    {
        return new StringContent(JsonConvert.SerializeObject(@object), Encoding.UTF8, "application/json");
    }
}