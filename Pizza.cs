using Microsoft.EntityFrameworkCore;

// create a model made up of entity classes and context obj that reps session with db (allows query and save data)
namespace PizzaStore.Models{
    // class that represents a pizza
    public class Pizza{
        public int Id {get;set;}
        public string? Name {get;set;}
        public string? Description {get;set;}
    }
}