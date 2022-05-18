MVC -> 
Model 
View 
Controller -> a C# class that inherits from controller class
			-> contains action methods
			https://localhost:7177/home/index

Views in MVC are called Razor viewwith cshtml extension (c# and html combined)

MovieCard view in multiple views
Home/index -> MovieCard
User/purchases -> MovieCard
User/favorites -> MovieCard


Partial View => MovieCartdPartail and then u can reuse it across multiple views

Design Pattern that enables you yo write loosley coupled code

### EF Core Code First Approach using Migrations

1. Create an Entity that you need based on Table requirement
2. Establish the connection string, where you want the database to be created
3. Install required libraries from NuGet
4. DbContext -> Represents your Database and DbSet -> Represents your table
5. Create custom DbContext class that ingerists from DbContext base class
6. Inject DbContextOptions from Program.cs with connection string into DbContext
7. Create DbSet<Entity> property in DbContext class
8. Create database using NuGet commands Add-Migration and Update-Database
9. Check database
10. If you want to change the schema, do so from code and not manually
11. 2 ways to model our Codew first design
	- Data Annotaions (used in movies for example)
	- Fluent API (takes precedence and used in genre for example)
			

Sync vs ASync

ASP.NET => when a reqiest comes in
GET => http://movieshop.com/movies/details/22

ASP.NET will have Threadpool => Collection of threads => 10 threads
T1, T2, T3... T10

20 requests at the same time for the same URL or different URl

t1 to t10 to process each request

11th request => gets dropped
prevent thread starvation scenario


I/O =>
async/await => go together, only await a method that returns a Task.

async modifier for method
always return a Task

sync			async
int				Task<int>	
ActionResult	Task<ActionResult>
void			Task

Server side Pagination
you wanna get Movies by genre, if you get all the movies in one page:
UI => not goot UI EXP
Takes lots of time => SQL server needs to get all the data
http://movieshop.com/movies/genre/1 => get all movies for genre 1

Movie, MovieGenre

Select m.id, m.posterurl, m.title
FROM Movie m join MovieGenre mg on m.id = mg.movieid
Where mg.genreid = 1
order by m.id desc
offset 0 rows fetch next 30 rows only


Authentication & Authorization

Public pages
1. Home page
2. Movie details
3. Cast details
4. Login page
5. Register page


User functionality
1. BUY movie
2. Favorite MOvie
3. Review Movie
4. Admin page
5. Get movies purchased by user
6. Get movies favorited by user


Asmin Funcitonality
1. Create movie
2. Create cast
3. Get popular movies from and to dates -> report data

### Create Register
1. Create links for register and login
2. Redirect to account controller

Model binding (case insensititve)
HttpContext -> it will give you all the information regarding the http request

Passwords should always be hashed with Salt

U1 -> abc@abc (Abc123!! + fdsjhfjksdfsfsdf) Hash1Alg -> adjjkfsjkfjsdfjklsdnjkfnwsdjnfjksd
U1 -> xyx@xyx (Abc123!! + fjdjkshfjkhsdjkf) Hash1Alg -> fhdjkashfjasdjkfhldsjkfhjksadfjksj

Encryption (two way) -> Credit Cards, Secret Answers, SSN, Dl

Hashing (one way) -> Passwords

Login
U1 -> abc@abc.com (Abc123!! + fdsjhfjksdfsfsdf) Hash1Alg - > adjjkfsjkfjsdfjklsdnjkfnwsdjnfjksd == has stored in database
Compare ahshes