using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;

namespace NetAF.Blazor.Example
{
    internal static class ExampleGame
    {
        internal static GameCreator Create(GameConfiguration configuration)
        {
            PlayableCharacter player = new("Dave", "A young boy on a quest to find the meaning of life.");

            RegionMaker regionMaker = new("Mountain", "An imposing volcano just East of town.")
            {
                [1, 0, 0] = new("Cavern", "A dark cavern set in to the base of the mountain.", [new Exit(Direction.North)], [new Item("Torch", "A wooden torch", true)]),
                [1, 1, 0] = new("Outside", "Outside the dark cavern.", [new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West)]),
                [0, 1, 0] = new("Ledge", "A ledge, overlooking the town.", [new Exit(Direction.East)]),
                [2, 1, 0] = new("Field", "A field.", [new Exit(Direction.West)])
            };

            OverworldMaker overworldMaker = new("Dave's World", "An ancient kingdom.", regionMaker);

            return Game.Create(
                new("The Life of Dave", "A very low budget adventure.", "Ben Pollard"),
                "Dave awakes to find himself in a cavern...",
                AssetGenerator.Retained(overworldMaker.Make(), player),
                GameEndConditions.NoEnd,
                configuration);
        }
    }
}
