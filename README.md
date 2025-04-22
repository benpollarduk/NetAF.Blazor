<div align="center">

# NetAF.Blazor

A library of Blazor components for [NetAF](http://www.github.com/benpollarduk/netaf).

![icon](.nuget/Icon.bmp)

[![main-ci](https://github.com/benpollarduk/NetAF.Blazor/actions/workflows/main-ci.yml/badge.svg)](https://github.com/benpollarduk/NetAF.Blazor/actions/workflows/main-ci.yml)
[![GitHub release](https://img.shields.io/github/release/benpollarduk/NetAF.Blazor.svg)](https://github.com/benpollarduk/NetAF.Blazor/releases)
[![NuGet](https://img.shields.io/nuget/v/netaf.blazor.svg)](https://www.nuget.org/packages/netaf.blazor/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/netaf.blazor)](https://www.nuget.org/packages/netaf.blazor/)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_NetAF.Blazor&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=benpollarduk_NetAF.Blazor)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_NetAF.Blazor&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=benpollarduk_NetAF.Blazor)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_NetAF.Blazor&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=benpollarduk_NetAF.Blazor)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_NetAF.Blazor&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=benpollarduk_NetAF.Blazor)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_NetAF.Blazor&metric=bugs)](https://sonarcloud.io/summary/new_code?id=benpollarduk_NetAF.Blazor)
[![License](https://img.shields.io/github/license/benpollarduk/NetAF.Blazor.svg)](https://opensource.org/licenses/MIT)

[![Try Now](https://img.shields.io/badge/Try-Now-brightgreen?style=for-the-badge)](https://benpollarduk.github.io/NetAF/)

</div>

## Getting Started

### Running the Example
Getting started running [NetAF](https://github.com/benpollarduk/NetAF/) in Blazor is easy!

* Clone the repo.
* Build and run NetAF.Blazor.Example.
* The app will be served at https://localhost:7295/. Navigate to https://localhost:7295/ in your browser to start the game.

![image](https://github.com/user-attachments/assets/c2d482f4-6137-4f7f-80be-a6ef839fd973)

### How it Works
Simply add the **GameComponent** to your page and then set up and start a game.

```
@page "/"
@using NetAF.Assets
@using NetAF.Blazor.Components
@using NetAF.Rendering.FrameBuilders
@using NetAF.Targets.Html
@using NetAF.Targets.Html.Rendering
@using NetAF.Targets.Html.Rendering.FrameBuilders

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

        var htmlBuilder = new HtmlBuilder();

        FrameBuilderCollection frameBuilderCollection = new(
            new HtmlTitleFrameBuilder(htmlBuilder),
            new HtmlSceneFrameBuilder(htmlBuilder, new HtmlRoomMapBuilder(htmlBuilder)),
            new HtmlRegionMapFrameBuilder(htmlBuilder, new HtmlRegionMapBuilder(htmlBuilder) ),
            new HtmlCommandListFrameBuilder(htmlBuilder),
            new HtmlHelpFrameBuilder(htmlBuilder),
            new HtmlCompletionFrameBuilder(htmlBuilder),
            new HtmlGameOverFrameBuilder(htmlBuilder),
            new HtmlAboutFrameBuilder(htmlBuilder),
            new HtmlReactionFrameBuilder(htmlBuilder),
            new HtmlConversationFrameBuilder(htmlBuilder));

        GameConfiguration configuration = new(htmlAdapter, frameBuilderCollection, new Size(50, 30));
        GameExecutor.Execute(ExampleGame.Create(configuration));

        await InvokeAsync(StateHasChanged);
    }
}
```

### Example Game
The [ExampleGame](NetAF.Blazor.Example/ExampleGame.cs) is included in the repo.

## Documentation
Please visit [https://benpollarduk.github.io/NetAF-docs/](https://benpollarduk.github.io/NetAF-docs/) to view the NetAF documentation.

## For Open Questions
Visit https://github.com/benpollarduk/NetAF.Blazor/issues
