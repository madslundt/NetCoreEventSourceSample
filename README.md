# API
This is an example of an API created with multiple micro services in order to manage movies with reviews.

Movies can be added and modified later on. 

One or more reviews can be added to a movie but does not appear until the status is approved.
Reviews can later be deleted.

# Code base
The current solution is heavily depending on Eventflow which gives the possibility to use CQRS with ES.

[ Image of solution ]

## Apps
Apps contain two projects:
 - EventConsumer
 - EventStore

**EventConsumer** being a project to run consumer persistance for RabbitMQ.

**EventStore** is a project running the event store database and making sure it is set up correctly.

## Gateway
Gateway is the API Gateway for the whole application.
This contains a ServicesGateway, which makes sure to redirect traffic to the correct micro service using Ocelot.

## Infrastructures
Infrastructures contain two projects:
 - EventStore middleware
 - Infrastructure

**Eventstore middleware**

**Infrastructure** is a projects containing helpers, configuration, etc. to ease integration of new services.

## MicroServices
Micro services directory contain all services needed to run in the API.

Each micro service contain two projects:
 - ReadStore
 - Service

**ReadStore** being the read model for the service. This is used to query through.

**Service** being the service API.

## Domain
Domain contain two root folders:
 - Business
 - Module

**Business** contain elements for each domain, such as:
 - Events
 - Queries
 - Commands
 - Snapshots
 - Aggregate
 - AggregateState
 - Entity

**Module** makes sure to bootstrap the domain in Eventflow.

# WIP
 - Authentication
 - GraphQL