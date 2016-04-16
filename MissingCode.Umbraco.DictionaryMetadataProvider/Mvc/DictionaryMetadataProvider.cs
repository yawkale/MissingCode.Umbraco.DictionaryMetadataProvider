using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using umbraco;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace MissingCode.Umbraco.DictionaryMetadataProvider.Mvc
{

    public class DictionaryMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        public UmbracoHelper UmbracoHelper
        {
            get
            {
                var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
                return umbracoHelper;
            }
        }
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
            Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            if (containerType != null && propertyName != null)
            {
                string key = string.Format("{0}.{1}", containerType.Name.Replace("Model", ""), propertyName);
                string altKey = string.Format("{0}", propertyName);
                //get model specific data
                var displayName = LookupDictionaryValue(key, metadata.DisplayName);


                //specific model is empty
                if (string.IsNullOrEmpty(displayName))
                {
                    displayName = LookupDictionaryValue(altKey, null);
                    if (string.IsNullOrEmpty(displayName))
                    {
                        return metadata;
                    }
                }


                metadata.DisplayName = displayName;
                metadata.Watermark = metadata.DisplayName;
                metadata.NullDisplayText = LookupDictionaryValue(key + ".NullDisplayText", metadata.NullDisplayText);


                var requiredFormat = LookupDictionaryValue("RequiredFormat", null);

                foreach (var attribute in attributes)
                {
                    if (attribute is ValidationAttribute)
                    {
                        string attributeName = attribute.GetType().Name;

                        if (attributeName.EndsWith("Attribute"))
                            attributeName = attributeName.Substring(0, attributeName.Length - 9);

                        string attributeDictKey = string.Format("{0}.{1}", key, attributeName);

                        var validationAttribute = (ValidationAttribute)attribute;

                        if (validationAttribute.ErrorMessageResourceName == null)
                        {
                            string errorMessage = LookupDictionaryValue(attributeDictKey, null);

                            if (errorMessage != null)
                            {
                                validationAttribute.ErrorMessage = errorMessage;
                            }
                            else
                            {
                                errorMessage = LookupDictionaryValue(string.Format("{0}.{1}", altKey, attributeName), null);
                                if (errorMessage != null)
                                {
                                    validationAttribute.ErrorMessage = errorMessage;
                                }
                            }


                            if (validationAttribute is RequiredAttribute && !string.IsNullOrEmpty(requiredFormat))
                            {

                                validationAttribute.ErrorMessage = string.Format(requiredFormat,
                                    metadata.DisplayName);

                            }


                        }
                    }

                }
            }

            return metadata;
        }


        protected virtual string LookupDictionaryValue(string dictKey, string defaultValue)
        {
            var value = UmbracoHelper.GetDictionaryValue(dictKey);

            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return defaultValue;

            //if (umbraco.cms.businesslogic.Dictionary.DictionaryItem.hasKey(dictKey))
            //{
            //    return library.GetDictionaryItem(dictKey);
            //}
            //return defaultValue;
        }
    }
}
