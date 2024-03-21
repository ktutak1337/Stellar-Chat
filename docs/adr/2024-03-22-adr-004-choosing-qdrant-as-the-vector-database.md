# ADR-004: Choosing Qdrant as the Vector Database

Date: 2024-03-22

## Status

We need to make a decision regarding the selection of a vector database to support our project. After careful consideration, we have chosen Qdrant as our preferred vector database solution.

## Context

In our open-source project architecture, we require a vector database to handle vector-based data, such as embeddings from various machine learning models. The vector database will play a critical role in efficiently searching and retrieving similar vectors for tasks like recommendation systems and similarity searches.

## Decision

After evaluating several vector database options, including alternatives like Pinecone and others, we have decided to adopt Qdrant as our vector database solution. This decision is based on the following key factors:

1. **Open Source**: Our project operates under an open-source paradigm, and the choice of Qdrant aligns with our commitment to open-source principles and practices. This decision reinforces our dedication to community-driven development and transparency.

2. **Popularity**: Qdrant is a well-established and popular open-source vector database solution within the machine learning and data science community. Its active user base and contributors make it a reliable choice for our project.

3. **Self-Hosting**: Qdrant can be easily self-hosted using Docker, providing us with flexibility in deployment and scalability.

4. **Community Support**: The availability of community support and documentation for Qdrant ensures that we can effectively integrate and maintain this database in our project.

## Rationale

By selecting Qdrant as our vector database, we aim to leverage its popularity, self-hosting capabilities, and community support to efficiently manage vector-based data in our project.

## Consequences

Implementing Qdrant as our vector database will require us to set up and configure the database, ensuring that it aligns with our specific use cases. Additionally, our team will need to become familiar with Qdrant's features and capabilities to fully utilize its potential.

## Alternatives Considered

During the decision-making process, we considered alternative vector database solutions, including Pinecone and others. However, the combination of Qdrant's popularity, self-hosting options, and community support made it the most suitable choice for our project.

## Change History

- 2024-03-22: Added ADR-004, the decision to choose Qdrant as the vector database for our project.