# NetAF.Blazor
A Blazor implementation of [NetAF](http://www.github.com/benpollarduk/netaf). This repo demonstrates how NetAF can be used in a Blazor app by making use of the various classes in NetAF.Targets.Html.  

## Getting Started

### Running the Example
Getting started running NetAF in Blazor is easy!

* Clone the repo.
* Build and run NetAF.Blazor.
* The app will be served at https://localhost:7295/. Navigate to https://localhost:7295/ in your broswer to start the game.

![image](https://github.com/user-attachments/assets/c2d482f4-6137-4f7f-80be-a6ef839fd973)

### How it Works
The Blazor app provides a single [page](NetAF.Blazor/Components/Pages/Home.razor) for rendering and interacting with the game. NetAF provides frame builders to generate the HTML for your game, which is then displayed directly in the page using:
```
<div>
    @((MarkupString)frameAsHtml)
</div>
```
The page itself is an implementation of *IFramePresenter*, when an update occurs in the game the *Present* method is called, which allows the page to receive the generated HTML content.

Besides the generated content the page displays a simple acknowledge button and a input box that allow the user to interact with the game. Based on the games current mode these elements are either visible or hidden.

The game itself is executed as a background task.

```
﻿@page "/"
@using NetAF.Assets
@using NetAF.Blazor.Components
@using NetAF.Rendering.FrameBuilders
@using NetAF.Targets.Html

<PageTitle>NetAF</PageTitle>

<GameComponent @ref="gameComponent" />

@code {
    private GameComponent? gameComponent;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        if (GameExecutor.IsExecuting)
            GameExecutor.CancelExecution();

        HtmlAdapter htmlAdapter = new(gameComponent);
        gameComponent?.SetAdapter(htmlAdapter);

        GameConfiguration configuration = new(htmlAdapter, FrameBuilderCollections.Html, new Size(50, 30));
        GameExecutor.Execute(ExampleGame.Create(configuration));

        await InvokeAsync(StateHasChanged);
    }
}
```

### Example Game
The [ExampleGame](NetAF.Blazor/ExampleGame.cs) is included in the repo.

## Documentation
Please visit [https://benpollarduk.github.io/NetAF-docs/](https://benpollarduk.github.io/NetAF-docs/) to view the NetAF documentation.

## For Open Questions
Visit https://github.com/benpollarduk/NetAF.Blazor/issues
