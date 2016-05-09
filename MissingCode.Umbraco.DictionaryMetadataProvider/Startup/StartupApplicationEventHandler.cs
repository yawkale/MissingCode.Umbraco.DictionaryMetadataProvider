using System;
using System.Web.Mvc;

using Umbraco.Core;
using Umbraco.Core.Services;

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

            LocalizationService.SavedDictionaryItem += LocalizationService_SavedDictionaryItem;
        }

        private void LocalizationService_SavedDictionaryItem(ILocalizationService sender, global::Umbraco.Core.Events.SaveEventArgs<global::Umbraco.Core.Models.IDictionaryItem> e)
        {
            var provider = ModelMetadataProviders.Current as Mvc.DictionaryMetadataProvider;

            if (provider != null)
            {
                foreach (var item in e.SavedEntities)
                {
                    provider.UpdateCache(item);
                }
            }
        }
    }
}
