using Microsoft.EntityFrameworkCore;

/*
* provides EntityFramework a database context so that it knows to build
* a table using our TomatoModel
*/

namespace TomatoAPI
{
    /*
    * We define a database context by creating a class that extends the
    * DbContext class from EntityFramework.
    */
    public class TomatoDb : DbContext
    {
        /*
        * Reference our tomato table using this:
        * A DbSet is a generic collection which is treated as the database table
        * which relates to our model. Its identifier is how we will be
        * retrieving data from the database using EF.
        *
        * "Database Context" - the link between your code and the database
        */
        public DbSet<Tomato> Tomatos { get; set; }

        /*
        * Tell EntityFramework to use SQLite as the database provider and save
        * all changes to a file named Tomatos.db
        */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Tomatos.db");
        }
    }
}
