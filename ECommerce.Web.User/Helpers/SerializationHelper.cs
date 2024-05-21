using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ECommerce.Web.User.Helpers
{
    public static class SerializationHelper
    {
        public static ActionResult SerializeObject(this object model)
        {
            var serialization = JsonConvert.SerializeObject(model, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return new ContentResult
            {
                Content = serialization,
                ContentType = "application/json"
            };
        }
    }
}