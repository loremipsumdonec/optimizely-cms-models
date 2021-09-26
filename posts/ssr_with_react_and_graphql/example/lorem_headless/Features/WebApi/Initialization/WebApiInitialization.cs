using EPiServer.Data;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace lorem_headless.Features.WebApi.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(DataInitialization))]
    public class WebApiInitialization
        : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            if (context.HostType != HostType.TestFramework)
            {
                GlobalConfiguration.Configure(config =>
                {
                    config.MapHttpAttributeRoutes();
                    UpdateSerializerSettings(config);
                    RemoveXmlFormatter(config);
                });
            }
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        private void RemoveXmlFormatter(HttpConfiguration config)
        {
            var mediaTypeForXml = config.Formatters
            .XmlFormatter
            .SupportedMediaTypes
            .FirstOrDefault(t => t.MediaType == "application/xml");

            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(mediaTypeForXml);
        }

        private void UpdateSerializerSettings(HttpConfiguration config)
        {
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}