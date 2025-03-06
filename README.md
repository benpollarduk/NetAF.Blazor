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
@page "/"
@using NetAF.Logic.Configuration
@using NetAF.Logic.Modes
@using NetAF.Rendering.FrameBuilders
@implements IFramePresenter
@rendermode @(new InteractiveServerRenderMode(prerender:false))

<PageTitle>NetAF</PageTitle>

<div>
    @((MarkupString)frameAsHtml)
</div>

<br />

@if (showInput)
{
    <input type="text" @ref="textInput" @bind="text" @onkeyup="HandleInput" />
}

@if (showAcknowledge)
{
    <button class="btn btn-primary" @ref="acknowledgeButton" @onclick="Acknowledge">OK</button>
}

@code {
    private HtmlAdapter? htmlAdapter;
    private string frameAsHtml = string.Empty;
    private string text = string.Empty;
    private bool showInput = false;
    private bool showAcknowledge = false;
    private ElementReference acknowledgeButton;
    private ElementReference textInput;

    private void Acknowledge()
    {
        htmlAdapter?.AcknowledgeReceived();
    }

    private void HandleInput(KeyboardEventArgs e)
    {
        if (e.Key == "Enter" && !string.IsNullOrWhiteSpace(text))
        {
            htmlAdapter?.InputReceived(text);

            text = string.Empty;
        }
    }

    protected override void OnInitialized()
    {
        if (GameExecutor.IsExecuting)
            return;

        htmlAdapter = new HtmlAdapter(this);
        GameConfiguration configuration = new(htmlAdapter, FrameBuilderCollections.Html, new(80, 50));
        GameExecutor.Execute(ExampleGame.Create(configuration), GameExecutionMode.BackgroundAutomatic);
    }

    public async void Present(string frame)
    {
        frameAsHtml = frame;
        showInput = htmlAdapter?.Game?.Mode?.Type == GameModeType.Interactive;
        showAcknowledge = htmlAdapter?.Game?.Mode?.Type == GameModeType.Information;

        await InvokeAsync(StateHasChanged);

        if (showInput && textInput.Context != null)
            await textInput.FocusAsync();

        if (showAcknowledge && acknowledgeButton.Context != null)
            await acknowledgeButton.FocusAsync();
    }
}
```
### Example Game
The [ExampleGame](NetAF.Blazor/ExampleGame.cs) is included in the repo.

## Documentation
Please visit [https://benpollarduk.github.io/NetAF-docs/](https://benpollarduk.github.io/NetAF-docs/) to view the NetAF documentation.

## For Open Questions
Visit https://github.com/benpollarduk/NetAF.Blazor/issues
