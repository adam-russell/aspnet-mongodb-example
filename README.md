# aspnet-mongodb-example
Example ASP.NET (.NET 6) application that accesses MongoDB. Uses simple API with a Vue front-end.

There's also a [post on this application that goes into more detail](https://www.adamrussell.com/aspnet-mongodb-example).

The main prerequisite for succesfully running the application is that the configuration in appsettings.json/appsettings.Development.json point to MongoDB running on localhost with no authentication and no replicaset. If you have MongoDB running elsewhere, just adjust the connection string in appsettings.json/appsettings.Development.json.

A screenshot of the application (though the code will be more interesting to anyone):

<img src="https://raw.githubusercontent.com/adam-russell/aspnet-mongodb-example/main/screenshots/aspnet-mongodb-example.png" width="580" height="366" alt="Screenshot of ASP.NET MongoDB Example">
