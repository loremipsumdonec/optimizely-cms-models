using Newtonsoft.Json;
using System.Collections.Generic;

namespace lorem_headless.Features.Web.Models
{
    public class WebModel
    {
        [JsonExtensionData]
        public Dictionary<string, object> Extensions { get; set; } = new Dictionary<string, object>();

        public void AddExtension(string key, object model)
        {
            Extensions.Add(key, model);
        }
    }
}