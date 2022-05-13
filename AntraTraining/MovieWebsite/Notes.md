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
			12. 