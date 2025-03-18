using NetAF.Logic;

namespace NetAF.Blazor.Classes
{
    /// <summary>
    /// A callback to handle post-restoration of a game.
    /// </summary>
    /// <param name="game">The game that was restored.</param>
    public delegate void PostRestoreCallback(Game game);
}
