## Synopsis

This project will show all countries and their cities and temperature. 
I feel it will tell more by using database instead of json file. 
I only inserted some dummy data (e.g 5 countries). The temperature is created by inserting some random 
numbers between 253 to 334 K, so they are not from the json file, thus some cities may have weird temperature.

## Thoughts

I believe Ado.net can tell whether one has a strong and solid background rather than Entity Framework and other ORMs.
I'm using MSSqlServer in this demo, without considering the database change.

I use basic three-tier architecture. No dependency injection. No consider about model validation (both server and client side)

My naming convention is like this: MvcController--CityController, while ApiController--CitiesController.
All these controllers are not taking care of validation, but I think it's enough for a demo.

I have admin page available. One should log in as an admin to see that page. I just write a simple sign in page, when you
click "signin", it will redirect to admin page. Of course in a real project this part is more complex.

## Installation and how to review this demo:

I use Visual Studio 2015. You may change project files if using other versions.

1. Run the script "GuanghuiWeather.sql" in Guanghui.Weather.Webapp/APP_Data. I use MSSQL 2012. If there are some errors please make some changes like changing db size, etc.
2. Make sure the connection string in web.config works fine since I use local db. Data source=. 
3. Set Guanghui.Weather.Webapp as startup project.
4. Restore nuget packages.
3. Run the project. You should see /Country/Index. If not, please type and see this page.
4. Reviewing the first page. And then you will see a link to navigate to the second page.
5. Please see details about usage in every page.
6. Clicking "Guanghui world weather demo" in the nav bar will always navigate to the first page.
