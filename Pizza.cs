using Microsoft.EntityFrameworkCore;

// create a model made up of entity classes and context obj that reps session with db (allows query and save data)
namespace PizzaStore.Models{
    // class that represents a pizza
    public class Pizza{
        public int Id {get;set;}
        public string? Name {get;set;}
        public string? Description {get;set;}
    }
    // added EF sp mpwcan wire code to save and query, it exposes Pizza in DB and write in memory db storage
    // Dbcontext represents a connection used to query and save in a database
    class PizzaDb: DbContext{
        public PizzaDb(DbContextOptions options): base(options){}
        public DbSet<Pizza> Pizzas {get;set;} = null!;
    }
}