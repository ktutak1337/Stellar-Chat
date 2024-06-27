<div align="center">

<img src="./docs/assets/logo-large.jpg" alt="logo-large" width="400" height="400">

<h1>Stellar Chat</h3>

A powerful multi-modal chat application that empowers users to create custom agents, generate images, utilize visual recognition, and engage in voice conversations. It seamlessly integrates with local LLMs and commercial models like OpenAI, Gemini, Perplexity, and Claude, while also offering the capability to converse with uploaded documents and websites.

  <p align="center">
    <a href="https://docs.stellar-chat.com/"><strong>Documentation</strong></a>
    |
    <a href="https://github.com/ktutak1337/Stellar-Chat/issues/new?assignees=&labels=%F0%9F%90%9B+Bug&projects=&template=bug_report.yml&title=%5BBug%5D+"><strong>Report Bug</strong></a>
    |
    <a href="https://github.com/ktutak1337/Stellar-Chat/issues/new?assignees=&labels=%F0%9F%A4%A9+Feature+Request&projects=&template=feature_request.yml&title=%5BRequest%5D+"><strong>Request Feature</strong></a>
  </p>

[![Build & Tests](https://github.com/ktutak1337/Stellar-Chat/actions/workflows/github-actions.yaml/badge.svg?branch=main)](https://github.com/ktutak1337/Stellar-Chat/actions/workflows/github-actions.yaml)
[![NuGet Package](https://img.shields.io/badge/.NET%20-8.0-blue.svg)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
[![GitHub license](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ktutak1337/Stellar-Chat/blob/main/LICENSE.md)
[![100 - commitow](https://img.shields.io/badge/100%20-commitow-lightgreen.svg)](https://100commitow.pl)

<h3>⭐️ Your star motivates me greatly! ⭐️</h3>  

> \[!NOTE]
>
> This project is part of the ["100 Commits"](https://100commitow.pl/) competition, which challenges participants to commit to their projects by making at least one meaningful commit every day for 100 consecutive days.
>

</div>

<details>
<summary><kbd>Table of Contents</kbd></summary>

1. [✨ Features](#-features)
   - [`1.` Support for Local Open Source Models](#1-support-for-local-open-source-models)
   - [`2.` Support for Commercial Models](#2-support-for-commercial-models)
   - [`3.` Visual Recognition](#3-visual-recognition)
   - [`4.` Support for TTS & STT](#4-support-for-tts--stt)
   - [`5.` Text to Image Generation](#5-text-to-image-generation)
   - [`6.` Multimodal Chat](#6-multimodal-chat)
   - [`7.` Prompt Store](#7-prompt-store)
   - [`8.` Custom Agent Creation (GPTs)](#8-custom-agent-creation-gpts)
   - [`9.` Message and Conversation Search](#9-message-and-conversation-search)
   - [`10.` Custom Action Creation for App Integration](#10-custom-action-creation-for-app-integration)
   - [`11.` Multi-Agent Chat Capability](#11-multi-agent-chat-capability)
2. [🚀 Self-Hosted](#-self-hosted)
3. [⌨️ Local Development](#-local-development)
4. [⭐ Enjoying the Project?](#-enjoying-the-project)
5. [🚧 Issues](#-issues)
6. [📝 License](#-license)

</details>

## 🎥 Demo
https://github.com/ktutak1337/Stellar-Chat/assets/49451143/3482d401-70cb-4ce8-bf2e-ec69f5859367

## ✨ Features


> \[!IMPORTANT]
>
> **Planned Features**
>
> This is a list of planned features to be implemented in the future. Please note that the list may change over time as the project progresses and new priorities emerge.
>

`1.` Support for Local Open Source Models
Integrate and utilize local open source models through the OLLAMA platform.

`2.` Support for Commercial Models
Easily use commercial models like OpenAI, Gemini, Perplexity, and Claude.

`3.` Visual Recognition
Utilize the powerful visual recognition capabilities of the GPT-4-Vision model and Gemini Vision.

`4.` Support for TTS & STT
Enable text-to-speech (TTS) and speech-to-text (STT) functionalities within the application.

`5.` Text to Image Generation
Generate images from text inputs using advanced models such as Stable Diffusion and DALL-E 3.

`6.` Multimodal Chat
Analyze text, image, and audio files and engage in conversations with uploaded files.

`7.` Prompt Store
Create and manage your own repository of predefined prompts to easily use, modify, and enhance interactions with the models.

`8.` Custom Agent Creation (GPTs)
Easily create and customize your own agents to tailor the interactions and responses according to your specific needs.

`9.` Message and Conversation Search
Easily search through all messages and conversations to quickly find relevant information or previous interactions.

`10.` Custom Action Creation for App Integration
Create custom actions to seamlessly integrate with your favorite applications such as Gmail, Todoist, Spotify, and more, enhancing productivity and workflow efficiency.

`11.` Multi-Agent Chat Capability
Engage in conversations with multiple agents simultaneously within a single chat interface, enabling diverse interactions and enhanced collaboration.

## 🚀 Self-Hosted

Choose the deployment method that best suits your needs and get started with Stellar Chat today!

Explore our deployment options to get started quickly:

<a href="https://docs.stellar-chat.com/deployment/deploy-with-docker/">
  <img src="docs\assets\deploy\btn-deploy-with-docker.jpg" alt="Deploy on Docker">
</a>

## ⌨️ Local Development

You have the option to utilize `GitHub Codespaces` for online development:

<a href="https://codespaces.new/ktutak1337/Stellar-Chat">
  <img src="https://github.com/codespaces/badge.svg" alt="Github Codespaces">
</a>
&nbsp;

Or clone it for local development:

```bash
git clone https://github.com/ktutak1337/Stellar-Chat.git

# It is recommended to use Docker to run the infrastructure components (MongoDB, Qdrant, Seq):
cd src
docker compose up -d

# configrure API:
cd src/Server/StellarChat.Server.Api

# set all api keys (more details in docs):
dotnet user-secrets init
dotnet user-secrets set openAI:api_key [your API KEY]

# Run API:
dotnet run watch

# Run web app:
cd src/Client/StellarChat.Client.Web
dotnet run watch
```

If you want to delve deeper into setting up your local development environment, please feel free to consult our [📘 Development Guide](https://docs.stellar-chat.com/guides/local-development/).

## ⭐ Enjoying the Project?

If you find this project helpful, learned something new, or using it to kickstart your own solution, consider showing your appreciation by giving it a star! Your support means a lot. Thank you! 🚀

## 🚧 Issues

If you have discovered a bug or having some issues, please let me know by [reporting a new issue](https://github.com/ktutak1337/Stellar-Chat/issues?state=open).

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/ktutak1337/Stellar-Chat/blob/main/LICENSE.md) file for details.
