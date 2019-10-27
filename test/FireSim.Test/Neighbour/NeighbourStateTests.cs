namespace FireSim.Test.Neighbour
{
    using System;
    using System.Linq;
    using FireSim.Neighbour;
    using Shouldly;
    using Xunit;

    public class NeighbourStateTests
    {
        [Theory]
        [InlineData(new CardinalDirection[] { CardinalDirection.North })]
        [InlineData(new CardinalDirection[] { CardinalDirection.South })]
        [InlineData(new CardinalDirection[] { CardinalDirection.East })]
        [InlineData(new CardinalDirection[] { CardinalDirection.West })]
        [InlineData(new CardinalDirection[] { CardinalDirection.North, CardinalDirection.East })]
        [InlineData(new CardinalDirection[] { CardinalDirection.South, CardinalDirection.West })]
        [InlineData(new CardinalDirection[] { CardinalDirection.North, CardinalDirection.East, 
            CardinalDirection.South, CardinalDirection.West })]
        public void T01_CanFilter(CardinalDirection[] filter)
        {
            NeighbourState states = new NeighbourState(GetFullStates());

            NeighbourState filtered = states.FilterByCarinalDirections(filter);

            var filterHash = filtered.States.OrderBy(x => x.Direction).Select(x => x.Direction).ToHashSet();
            var testHash = filter.OrderBy(x => x).ToHashSet();
            filterHash.SequenceEqual(testHash).ShouldBe(true);
        }

        [Theory]
        [InlineData(CellState.Empty, CellState.Empty)]
        [InlineData(CellState.Tree, CellState.Tree)]
        [InlineData(CellState.Tree, CellState.Burning)]
        [InlineData(CellState.Burning, CellState.Burning)]
        [InlineData(CellState.Burning, CellState.Tree)]
        public void T02_CanDetectStates(CellState stateToCheck, CellState initState)
        {
            NeighbourState states = new NeighbourState(GetFullStates(initState));
            bool expectedResult = stateToCheck == initState;

            bool result = states.AnyInState(stateToCheck);

            result.ShouldBe(expectedResult);
        }

        private CellNeighbour[] GetFullStates(CellState initialisedStateForSouth = CellState.Empty)
        {
            return new CellNeighbour[]
            {
                new CellNeighbour(CardinalDirection.North, CellState.Empty),
                new CellNeighbour(CardinalDirection.NorthEast, CellState.Empty),
                new CellNeighbour(CardinalDirection.East, CellState.Empty),
                new CellNeighbour(CardinalDirection.SouthEast, CellState.Empty),
                new CellNeighbour(CardinalDirection.South, initialisedStateForSouth),
                new CellNeighbour(CardinalDirection.SouthWest, CellState.Empty),
                new CellNeighbour(CardinalDirection.West, CellState.Empty),
                new CellNeighbour(CardinalDirection.NorthWest, CellState.Empty)
            };
        }
    }
}
