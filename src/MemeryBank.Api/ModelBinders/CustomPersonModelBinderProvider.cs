using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MemeryBank.Api.Models;


// Using this means that whenever the person class is used iny action method the custompersonModelbinder will be used automatically
namespace MemeryBank.Api.ModelBinders
{
    public class CustomPersonModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if(context.Metadata.ModelType == typeof(Person))
            {
                return new BinderTypeModelBinder(typeof(CustomPersonModelBinder));  
            }
            return null;
        }
    }
}
