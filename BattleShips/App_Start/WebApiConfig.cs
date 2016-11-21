namespace BattleShips
{
    using System.Web.Http;
    using Newtonsoft.Json;

    /// <summary>
    /// WebAPI configuration options registration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers WebAPI configuration options
        /// </summary>
        /// <param name="config">The configuration options</param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}");

            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }
    }
}
