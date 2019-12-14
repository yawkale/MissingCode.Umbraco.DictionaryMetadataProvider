using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace MissingCode.Umbraco.DictionaryMetadataProvider.Startup
{

    public class DictionaryMetadataProviderComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {

            composition.Register<Mvc.DictionaryMetadataProvider>();
            composition.Components().Append<DictionaryMetadataProviderComponent>();
        }
    }
    public class DictionaryMetadataProviderComponent : IComponent
    {

        public void Initialize()
        {
            ModelMetadataProviders.Current =new Mvc.DictionaryMetadataProvider();
            LocalizationService.SavedDictionaryItem += LocalizationService_SavedDictionaryItem;
        }

        public void Terminate()
        {
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
