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
