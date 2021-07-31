# Table of Contents

1. [Introduction](#introduction) 
1. [Technologies](#technologies)
1. [Development](#development)
1. [Basic](#basic)
   1. [Dark-Mode](#dark-mode)
   1. [Google-Auth](#google-auth)
   1. [Chat](#chat)
1. [Games](#games)
   1. [TicTacToe](#tictactoe)
      

# Introduction
This repository contains the website https://suadin.de/ which based on [Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) with aim to use it as sandbox to practise full-stack-developer topics outside of restricted environments.

# Technologies
* [Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor): project template, run .NET C# code on client, less server communication
* [SignalR](https://docs.microsoft.com/en-us/aspnet/signalr/overview/getting-started/introduction-to-signalr): bidirectional communication between client and server
* [Bootstrap](https://getbootstrap.com/): responsive design

# Development
* `choco install visualstudio2019community`
  * ensure .NET 5 is installed
  * ensure ASP.NET and .NET core features are enabled
* `choco install docker-for-windows`
  * ensure virtualization (in bios) is enabled
  * ensure `docker run -d -p 80:80 docker/getting-started` works
  * run docker
* `choco install gitkraken` (optional)
  * good tree-overview and diff-tool
* run this website
  * clone project
  * open visual studio
  * add credentials
    * `dotnet user-secrets set "Authentication:Google:ClientSecret" "client_secret"`
  * run as docker, start of container is already configured [[source](https://docs.microsoft.com/de-de/visualstudio/containers/container-launch-settings?view=vs-2019)]
  * expect browser opens https://localhost:8443/
* build & deploy
  * push changes on main branch
  * expect https://suadin.de/ gets changes after few minutes automatically [[details](https://github.com/suadin/infrastructure)]

# Basic

## Dark-Mode

1. create css variables for dark/light mode [[source](https://www.reddit.com/r/dotnet/comments/k9ryyw/blazor_webassembly_dark_mode_css_variables/)]
1. you can select theme on operating system, for example in windows 10 [[source](https://uk.pcmag.com/migrated-3765-windows-10/122487/how-to-enable-dark-mode-in-windows-10)]
1. use `@media(prefers-color-scheme): dark|light|no-preference` to use theme from operating systems settings in website [[source](https://www.timellenberger.com/blog/operating-system-dark-mode-in-your-css)]

## Google-Auth

1. Setup Server Project [[source](https://code-maze.com/google-authentication-in-blazor-webassembly-hosted-applications/)]
   1. Login to your google account
   1. Goto google [credentials](https://console.cloud.google.com/apis/credentials)
   1. create credentials for `OAuth-Client-ID`
      1. select website and choose website name
      1. add website url https://suadin.de (for development: https://localhost:8443)
      1. add callback url https://suadin.de/authentication/login-callback (for development: https://localhost:8443/authentication/login-callback)
   1. take `Client-ID` and add into `appsettings.json`
   1. take `Client-Secret` and add into `user secrets` for dev [source](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows) and into `docker environment varibales` for prod [[details](https://github.com/suadin/infrastructure/blob/main/README.md#deployment)]
   1. add google middleware
1. Setup Client Project [[source](https://www.learmoreseekmore.com/2021/04/part3-steps-for-implementing-google-authentication-into-existing-blazor-webassembly-standalone-application.html)]
   1. take `Client-ID` and other necessary properties and add into `appsettings.json`
   2. add oidc middleware
   3. follow guide in [[source](https://www.learmoreseekmore.com/2021/04/part3-steps-for-implementing-google-authentication-into-existing-blazor-webassembly-standalone-application.html)] to setup client with login/logout/checks/username
      1. HTML auth checks:
      ```
      <AuthorizeView>
          <Authorized>
              <p>only visible for authenticated user</p>
          </Authorized>
          <NotAuthorized>
              <p>only visible for anonymous user</p>
          </NotAuthorized>
      </AuthorizeView>
      ```
      2. HTML get username: `@context.User.Identity.Name`
      3. C# get username:
      ```
      [CascadingParameter]
      public Task<AuthenticationState> AuthState { get; set; }
      ...
      var authState = await AuthState;
      userName = authState.User.Identity.Name;
      ```

## Chat

TODO: needs refactoring! [[source](https://docs.microsoft.com/de-de/azure/azure-signalr/signalr-tutorial-build-blazor-server-chat-app)]

# Games

## TicTacToe

TODO
