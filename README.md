# NPBaseRepo

A simple repository generator for C# projects. NPBaseRepo creates a fully testable repository layer based on entity framework entities.
The good thing about NPBaseRepo is that it works with both database-first and code-first entities, without requiring an .edmx file.

## Getting Started

Download the NPBaseRepo.tt file and put it in your C# project. Open it and fill the following configuration values:

* **entitiesContext**: The namespace of the entity framework context class.
* **entitiesNamespace**: The namespace of the entities (models).
* **entitiesProject**: The name of the project where the entities reside.
* **entitiesFolder**: The path to the folder where the entities reside. The path is relative to the project.
* **fileNamespace**: The namespace used in the file that NPBaseRepo.tt generates.

Now, NPBaseRepo.tt generates the following classes:
* **IDataRepository**: The interface of the repository layer.
* **DataRepositoryBase**: The base implementation of the repository layer, which uses entity framework to communicate with the database.
* **FakeDataRepositoryBase**: The base fake implementation of the repository layer, which uses internal memory lists to store the data. It is used in unit tests.

The repository provides the following methods:
* **Save**: Saves the changes to the database. This is usable only if a database is used.
* **ResetDatabase**: Deletes all the data in the database. **CAUTION**: This must only be used for integration tests.
* For each model type:
 * **GetEntities**: Returns an IQueryable with the entities.
 * **SearchEntities**: Searches for entities and returns an IQueryable. It checks all string properties.
 * **GetEntity**: Returns an entity based on its primary key.
 * **Add**: Adds a new entity.
 * **Delete**: Deletes an entity.

### Prerequisites

You must use **Visual Studio** to build your project. Right-click on NPBaseRepo.tt and choose the **Run Custom Tool** option to generate the repository.

### Running the example

NPBaseRepo comes with an example application that contains an ASP.NET MVC 5 web project and a unit test project.

Download the application from the **Example** folder and follow the following steps:
* Open **web.config** and change the connection string to point to your desired database.
* Run the migrations to update the database.
* Run the project.

## Running the tests

The **NPBaseRepo.Tests** project contains a few sample unit tests, for the web project, that you can run.
If you want to run integration tests against a real test database, please follow the following steps:
* Set NPBaseRepo.Tests as the StartUp project.
* Open **app.config** and change the connection string to point to your desired test database.
* Change the value of **UseDatabase** to **True**.
* Run the migrations to update the test database.
* Run the tests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details