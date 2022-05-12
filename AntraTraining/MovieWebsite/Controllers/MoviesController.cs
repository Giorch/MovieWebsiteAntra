using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MovieWebsite.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Details(int id)
        {
            // go to movies table and get the movie details by ID
            // connect to SQL server and execute the SQL query
            // select * from Movie where id =2;
            // get the movies entity (object)
            // Repositories => Data access Logic
            // Services => Business Logic
            // Controllers action methods => Services methods => Repository methods => SQL database
            // get the mode data from the services and send the data to the views (M)
            // Onion architecure or N-Layer architetcure
            return View();
        }
    }
}
