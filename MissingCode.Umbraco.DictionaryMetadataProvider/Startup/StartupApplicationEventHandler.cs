using System.Web.Mvc;

using Umbraco.Core;

namespace MissingCode.Umbraco.DictionaryMetadataProvider.Startup
{
    public class StartupApplicationEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationInitialized(UmbracoApplicationBase umbracoApplication,
           ApplicationContext applicationContext)
        {
            ModelMetadataProviders.Current = new Mvc.DictionaryMetadataProvider();
        }
    }
}
