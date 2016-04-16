using System;
using System.Web.Mvc;

using Umbraco.Core;

namespace MissingCode.Umbraco.DictionaryMetadataProvider.Startup
{
    public class StartupApplicationEventHandler : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
         
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
          
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ModelMetadataProviders.Current = new Mvc.DictionaryMetadataProvider();
        }
    }
}
