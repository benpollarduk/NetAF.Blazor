# NetAF.Blazor
A Blazor implementation of NetAF.

## Hello World
The following provides a basic interface to allow interaction with a NetAF game.

```csharp
@page "/"
@implements IFramePresenter
@rendermode InteractiveServer

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
    private string frameAsHtml = string.Empty;
    private HtmlAdapter? htmlAdapter;
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
        Task.Run(() =>
        {
            htmlAdapter = new HtmlAdapter(this);
            var configuration = new HtmlGameConfiguration(htmlAdapter, ExitMode.ReturnToTitleScreen);
            Game.Execute(ExampleGame.Create(configuration));
        });
    }

    public async void Present(string frame)
    {
        frameAsHtml = frame;
        showInput = htmlAdapter?.Game?.Mode?.Type == Logic.Modes.GameModeType.Interactive;
        showAcknowledge = htmlAdapter?.Game?.Mode?.Type == Logic.Modes.GameModeType.Information;

        await InvokeAsync(StateHasChanged);

        if (showInput && textInput.Context != null)
            await textInput.FocusAsync();

        if (showAcknowledge && acknowledgeButton.Context != null)
            await acknowledgeButton.FocusAsync();
    }
}
```

The example game:

```csharp
using NetAF.Assets.Characters;
using NetAF.Logic;
using NetAF.Logic.Callbacks;
using NetAF.Logic.Configuration;
using NetAF.Utilities;

namespace NetAF.Blazor
{
    internal class ExampleGame
    {
        internal static GameCreationCallback Create(IGameConfiguration configuration)
        {
            PlayableCharacter player = new("Dave", "A young boy on a quest to find the meaning of life.");

            RegionMaker regionMaker = new("Mountain", "An imposing volcano just East of town.")
            {
                [0, 0, 0] = new("Cavern", "A dark cavern set in to the base of the mountain.")
            };

            OverworldMaker overworldMaker = new("Daves World", "An ancient kingdom.", regionMaker);

            return Game.Create(
                new("The Life of Dave", "A very low budget adventure.", "Ben Pollard"),
                "Dave awakes to find himself in a cavern...",
                AssetGenerator.Retained(overworldMaker.Make(), player),
                GameEndConditions.NoEnd,
                configuration);
        }
    }
}
```