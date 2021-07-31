# Table of Contents

1. [Introduction](#introduction) 
2. [Technologies](#technologies)
3. [Development](#development)
4. [Features](#features)

## Introduction
[suadin.de](https://suadin.de/) is a website based on [Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) with aim to use it as sandbox to practise full-stack-developer topics outside of restricted environments.

## Technologies
* [Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor): project template, run .NET C# code on client, less server communication
* [SignalR](https://docs.microsoft.com/en-us/aspnet/signalr/overview/getting-started/introduction-to-signalr): bidirectional communication between client and server
* [Bootstrap](https://getbootstrap.com/): responsive design

## Development
* `choco install visualstudio2019community`
  * ensure .NET 5 is installed
  * ensure ASP.NET and .NET core features are enabled
* `choco install docker-for-windows`
  * ensure virtualization (in bios) is enabled
  * ensure `docker run -d -p 80:80 docker/getting-started` works
* `choco install gitkraken` (optional)
  * good tree-overview and diff-tool
* run this website
  * clone project
  * open visual studio
  * dotnet user-secrets set "Authentication:Google:ClientSecret" "client_secret"
  * run as docker (see [container start properties](https://docs.microsoft.com/de-de/visualstudio/containers/container-launch-settings?view=vs-2019))
  * expect browser opens with website
* build & deploy
  * push changes on main branch
  * expect [suadin.de](https://suadin.de/) website contains changes after few minutes automatically
  * details behind automatism on [suadin/infrastructure](https://github.com/suadin/infrastructure)

## Features

### Google Auth

Source documentation [server](https://code-maze.com/google-authentication-in-blazor-webassembly-hosted-applications/), [client](https://www.learmoreseekmore.com/2021/04/part3-steps-for-implementing-google-authentication-into-existing-blazor-webassembly-standalone-application.html), [client usage](https://code-maze.com/authenticationstateprovider-blazor-webassembly/):
1. Login to your google account
1. Goto google [credentials](https://console.cloud.google.com/apis/credentials)
1. create credentials for `OAuth-Client-ID`
   1. select website
   1. enter website name
   1. add website url https://suadin.de
   1. add callback url https://suadin.de/authentication/login-callback
1. take `Client-ID` and `Client-Secret`
   * add `Client-ID` into `appsettings.json` on client and server
   * add `Client-Secret` into `user secrets` [how to do that?](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows)
1. add google middleware on server
1. add oidc middleware with config on client
1. do [client setup](https://www.learmoreseekmore.com/2021/04/part3-steps-for-implementing-google-authentication-into-existing-blazor-webassembly-standalone-application.html) for login/logout
1. display user name with
   * HTML: @context.User.Identity.Name
   * C#:
   ```
   [CascadingParameter]
   public Task<AuthenticationState> AuthState { get; set; }
   ...
   var authState = await AuthState;
   userName = authState.User.Identity.Name;
   ```
1. show/hide html sections with
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

* [Chatroom](https://docs.microsoft.com/de-de/azure/azure-signalr/signalr-tutorial-build-blazor-server-chat-app) to chat with other website visitors
  * [Auto-Identity](?): allows guests to chat without set a name or explicite join | :warning: **still not implemented**
* [Dark Mode](https://www.reddit.com/r/dotnet/comments/k9ryyw/blazor_webassembly_dark_mode_css_variables/) to be able to switch between dark and light mode
  * [Default Settings](?): take default setting from operating system like [here](https://docs.microsoft.com/en-us/) | :warning: **still not implemented**
