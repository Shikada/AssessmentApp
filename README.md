# AssessmentApp
App created as an exercise for technical assessment

## General design
Project was done as a modular monolith, where each module is a DDD bounded context. Domains modeled are:
- Customer, as in the customer facing part of the app (alternate name could have been Store, Seller, etc.)
- Manufacturer, the side that produces parts

The spec states that there are three independant facilities  that produce parts, and this app can serve as a backend
for all of them. As the solution required additional scalling, before separating this modular monolith into microservices,
the app could scale by deploying multiple instances of the monolith. By configuring the messages that each instance responds to,
we can get good scalling properties. Once that is not enough to meet scalling needs, then the additional cost of microservice
architecture is justified.

The entire project is done in hexagonal architecture pattern.

## Project structure
Both modules are in the same solution with one ASP.NET API project serving them both. The projects are standardized as follows:
- Core - domain models, no dependencies
- Application - applicative logic, organized around use cases for high cohesion and ports to not rely on infrastructure, depends on Core
- Infrastructure - handles all external services so the rest of the projects are isolated from them, implements ports and relies on both
Core and Application projects
- Infrastructure.Db - can be combined with Infrastructure, but there's usually enough DB stuff that it's easier to seperate it out. implements
all EF needs (DbContext and Migrations) as well as all repositories.
- Messages - all commands and events from this domain are here. When a module is separated into a microservice, this project can become a NuGet package.

## Major tech choices
- MassTransit - good free message bus that is production grade
- SQLite - convinient choice for exercise app, but in production I would pick an externally hosted Postgres instance
- Serilog - structured logging with many useful integrations
- EF Core - default choice until performance becomes an issue

## Future improvements
- Improve simplistic handling of money (currently only *Decimal* is used,
better to use some variation of the [money](https://martinfowler.com/eaaCatalog/money.html) pattern)
- The payment system used was just a very high level simplification, a proper generic implementation and integrations would need to be designed
- Commands are just published via the MassTransit bus, but in production I would tend to sync solutions, either REST or gRPC
- Implement authentication and authorization
- HTTPS