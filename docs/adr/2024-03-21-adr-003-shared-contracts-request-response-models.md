# ADR-003: Creating Request and Response Models in Shared project

Date: 2024-03-21

## Status

We need to make a decision regarding the creation of `Request` and `Response` models in our Shared project. These models will be accessible to client applications developed in C#.

## Context

In our architecture, we have client applications that communicate with our backend services using HTTP endpoints. To ensure a consistent and well-defined contract between clients and services, we have decided to introduce Request and Response models in the `Shared` project. These models will facilitate communication between clients and services.

`Request` models will replace the use of `commands/queries` (CQRS) in HTTP endpoints, representing actions or commands that clients want to perform. `Response` models will be used to convey data in response to client requests. The naming convention for these models will include a verb in the name to indicate the action.

## Decision

We have decided to create `Request` and `Response` models in the `Shared.Contracts` project. These models will follow the naming convention of including a verb in their names to represent the action or command. For example:

- CreateActionRequest
- GetActionRequest
- UpdateActionRequest
- DeleteActionRequest
- ActionResponse

Key considerations for this decision include:

1. **Consistency**: Introducing standardized `Request` and `Response` models ensures a consistent API contract across client applications and backend services.

2. **Clarity**: Naming models with verbs provides clarity about the intended action, making it easier for developers to understand the purpose of each model.

3. **Transition to Commands/queries**: Response models will initially be used to handle HTTP endpoints but will later be mapped to `commands` and `queries` in our CQRS architecture, streamlining the transition.

4. **Shared Contracts**: This approach allows API contracts, represented by Request and Response models, to be shared between client applications and backend services. By sharing these contracts, both the API and client applications can maintain a common understanding of data structures and interactions, promoting consistency and reducing potential issues related to contract mismatches.

## Rationale

Through the introduction of `Request` and `Response` models in the `Shared` project, our goal is to enhance the manageability and transparency of our API agreement with client applications. This strategy enables the sharing of API agreements, represented by Request and Response models, between client applications and backend services. This collaborative sharing ensures a shared comprehension of contracts and interactions between both the API and client applications, fostering uniformity and minimizing potential challenges stemming from discrepancies in agreements.

## Consequences

The introduction of `Request` and `Response` models will require additional development effort to create and maintain these models. However, this effort is expected to pay off in terms of improved clarity, maintainability, and long-term architectural flexibility.

Developers working on client applications will need to be aware of and use these standardized models when making HTTP requests to our services.

## Alternatives Considered

We considered alternatives such as using DTOs (Data Transfer Objects) for response handling and the use of CQRS `commands` and `queries` within our endpoints. However, we concluded that the introduction of `Request` and `Response` models within `Shared` project provides a more structured and sustainable approach, aligning with established best practices for API design.

## Change History

- 2024-03-21: Added ADR-003, the decision to create `Request` and `Response` models in `Shared` project.