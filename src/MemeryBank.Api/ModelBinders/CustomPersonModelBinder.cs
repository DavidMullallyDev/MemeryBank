using MemeryBank.Api.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MemeryBank.Api.ModelBinders
{

    // Custom model binders are rarely used in real world projects
    public class CustomPersonModelBinder : IModelBinder
    {
        bool hasFirstName, hasLastName;
        private Person person = new();
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {  
            if (bindingContext.ValueProvider.GetValue("FirstName").Length > 0)
            {
                person.FirstName = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;
                person.FullName = person.FirstName;
                hasFirstName = true;
            } 
            if (bindingContext.ValueProvider.GetValue("LastName").Length > 0)
            {
                person.LastName = bindingContext.ValueProvider.GetValue("LastName").FirstValue;
                person.FullName += " " + person.LastName;
                hasLastName = true;
            }

            if(hasFirstName && hasLastName)
            {
                bindingContext.Result = ModelBindingResult.Success(person);
            }
            if (bindingContext.ValueProvider.GetValue("Phone").Length > 0) person.Phone = bindingContext.ValueProvider.GetValue("Phone").FirstValue;
            if (bindingContext.ValueProvider.GetValue("Email").Length > 0) person.Email = bindingContext.ValueProvider.GetValue("Email").FirstValue;
            if (bindingContext.ValueProvider.GetValue("Password").Length > 0) person.Password = bindingContext.ValueProvider.GetValue("Password").FirstValue;
            if (bindingContext.ValueProvider.GetValue("UserName").Length > 0) person.UserName = bindingContext.ValueProvider.GetValue("UserName").FirstValue;
            if (bindingContext.ValueProvider.GetValue("Id").Length > 0) person.Id = Guid.Parse(bindingContext.ValueProvider.GetValue("Id").FirstValue);


            return Task.CompletedTask;
        }
    }
}
