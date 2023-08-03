# newme-catalog

#### Migration:
- To create a new migration run the command below in the terminal in the path "src/Newme.Catalog.Infrastructure".

1. dotnet ef --startup-project ../Newme.Catalog.API/  migrations add initial -c CatalogCommandContext --msbuildprojectextensionspath local/obj -v
2. dotnet ef --startup-project ../Newme.Catalog.API/  database update

#### Rabbitmq
- To enable rabbitmq run the command below on terminal.

1. rabbitmq-server

- Queues and TrackingsExchanges must be previously created to run the project.

#### Swagger
- https://localhost:7154/swagger/index.html

