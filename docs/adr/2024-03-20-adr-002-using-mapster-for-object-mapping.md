# ADR-002: Using Mapster for Object Mapping

Date: 2024-03-20

## Status

We need to make a decision regarding the choice of a library for object mapping in our project.

## Context

In our project, we often need to map objects from one type to another. This includes mapping DTOs to domain objects, domain objects to database entities, and vice versa. Efficient and maintainable object mapping is crucial for the project's success.

## Decision

After evaluating several options, we have decided to use the Mapster library for object mapping in our project. Mapster provides a simple and efficient way to handle object mapping with minimal configuration and a clean, fluent API.

Key reasons for choosing Mapster include:

1. **Simplicity**: Mapster offers a straightforward and easy-to-understand API, making it accessible to all team members.

2. **Performance**: Mapster is known for its performance optimization, reducing overhead in object mapping operations.

3. **Flexibility**: It allows customization when needed, but also provides sensible defaults for most mapping scenarios.

4. **Community Support**: Mapster has an active community, which means access to documentation, tutorials, and support.

## Rationale

While there are other object mapping libraries available, such as AutoMapper, we believe that Mapster's combination of simplicity, performance, and flexibility aligns well with our project's requirements. It allows us to focus on our domain logic rather than spending excessive time configuring mapping rules.

## Consequences

The adoption of Mapster for object mapping will require team members to familiarize themselves with its usage and best practices. However, the learning curve is expected to be minimal due to Mapster's intuitive API.

We will need to integrate Mapster into our existing codebase and update our mapping code to use Mapster's syntax. This may require some refactoring of existing mapping code if we were using a different library or manual mapping.

## Alternatives Considered

We considered other object mapping libraries like AutoMapper and manually writing mapping code. However, we believe Mapster offers the best balance of simplicity, performance, and flexibility for our project's needs.

## Change History

- 2024-03-20: Added ADR-002, the decision to use Mapster for object mapping.