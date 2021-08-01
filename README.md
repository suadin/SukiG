# Table of Contents

1. [Introduction](#introduction) 
1. [Technologies](#technologies)
1. [Development](#development)
1. [Basic](#basic) -> [Dark-Mode](#dark-mode) | [Google-Auth](#google-auth) | [Chat](#chat)
1. [Games](#games) -> [TicTacToe](#tictactoe)
      
# Introduction
This repository contains the website https://suadin.de/ which based on [Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) with aim to use it as sandbox to practise full-stack-developer topics outside of restricted environments.

# Technologies
* [Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor): project template, run .NET C# code on client, less server communication
* [SignalR](https://docs.microsoft.com/en-us/aspnet/signalr/overview/getting-started/introduction-to-signalr): bidirectional communication between client and server
* [Bootstrap](https://getbootstrap.com/): responsive design
* [Open Iconic](https://useiconic.com/open): icon set

# Development
* `choco install visualstudio2019community`, ensure .NET 5 is installed, ensure ASP.NET and .NET core features are enabled
* `choco install docker-for-windows`, ensure virtualization (in bios) is enabled, ensure `docker run -d -p 80:80 docker/getting-started` works, run docker
* `choco install postgresql`, `choco install pgadmin4`, run pgadmin4, enter password, execute commands:
  * `create database suadin;`, `CREATE USER suadin WITH PASSWORD 'jw8s0F4';`, `GRANT CREATE ON DATABASE suadin TO suadin`
  * `dotnet tool install --global dotnet-ef` [[source](https://docs.microsoft.com/de-de/ef/core/cli/dotnet)]
* `choco install gitkraken` (optional), good tree-overview and diff-tool
* run website: clone project, open visual studio, add credentials
  * `dotnet user-secrets set "Authentication:Google:ClientSecret" "client_secret"`
  * `dotnet user-secrets set "ConnectionStrings:DefaultConnectionPassword" "jw8s0F4"`
* run as docker, start of container is already configured [[source](https://docs.microsoft.com/de-de/visualstudio/containers/container-launch-settings?view=vs-2019)]
* expect browser opens https://localhost:8443/
* build & deploy: push changes on main branch and expect https://suadin.de/ gets changes [[details](https://github.com/suadin/infrastructure)]

# Basic

## Dark-Mode

1. create css variables for dark/light mode [[source](https://www.reddit.com/r/dotnet/comments/k9ryyw/blazor_webassembly_dark_mode_css_variables/)]
1. you can select theme on operating system, for example in windows 10 [[source](https://uk.pcmag.com/migrated-3765-windows-10/122487/how-to-enable-dark-mode-in-windows-10)]
1. use `@media(prefers-color-scheme): dark|light|no-preference` to use theme from operating systems settings in website [[source](https://www.timellenberger.com/blog/operating-system-dark-mode-in-your-css)]

## Google-Auth

1. Setup Server Project [[source](https://code-maze.com/google-authentication-in-blazor-webassembly-hosted-applications/)]
   * add website url https://suadin.de for prod, https://localhost:8443 for development
   * add callback url https://suadin.de/authentication/login-callback for prod, https://localhost:8443/authentication/login-callback for development
   * set `Client-ID` into `appsettings.json`
   * set `Client-Secret` into `user secrets` for development [[source](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows)], into `docker environment varibales` for prod [[details](https://github.com/suadin/infrastructure/blob/main/README.md#deployment-script)]
1. Setup Client Project [[source](https://www.learmoreseekmore.com/2021/04/part3-steps-for-implementing-google-authentication-into-existing-blazor-webassembly-standalone-application.html)]
1. Use auth feature by example [[source](https://www.learmoreseekmore.com/2021/04/part3-steps-for-implementing-google-authentication-into-existing-blazor-webassembly-standalone-application.html)]
   * HTML keywords: `<AuthorizeView>` (parent), `<Authorized>` (only authenticated), `<NotAuthorized>` (only anonymous)
   * HTML user name: `@context.User.Identity.Name`
   * C# user name: `[CascadingParameter] public Task<AuthenticationState> AuthState { get; set; }` -> `(await AuthState).User.Identity.Name;`

## Chat

TODO: needs refactoring! [[source](https://docs.microsoft.com/de-de/azure/azure-signalr/signalr-tutorial-build-blazor-server-chat-app)]

# Games

## TicTacToe

TODO
