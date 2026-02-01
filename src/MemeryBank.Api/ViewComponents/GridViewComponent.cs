using Microsoft.AspNetCore.Mvc;
using MemeryBank.Api.Models;

namespace MemeryBank.Api.ViewComponents
{
    // must either have ViewComponent suffix in className of have [ViewComponent] aS attribute
    //[ViewComponent] 
    public class GridViewComponent : ViewComponent
    {
        //every viewcomponent must have this method
        //public async Task<IViewComponentResult> InvokeAsync(List<Person>? people)
        public async Task<IViewComponentResult> InvokeAsync(List<Person>? people)
        {
            //List<Person> people = new List<Person>() { new() { FirstName = "dengo", LastName = "mullally" }, new() {FirstName="doodles", LastName="de brito pimentel" } };
            //logic to get data from database object etc can placed here and sent to the view
            //ViewData etc
            ViewData["GridTitle"] = "People";
            return View(people); //invokes a partial view which must be at View/Shared/Grid/Default.cshtml 
            //return View("Default"); //if you use a diiferent name for the view you must pass it as an argument
        }
    }
}
