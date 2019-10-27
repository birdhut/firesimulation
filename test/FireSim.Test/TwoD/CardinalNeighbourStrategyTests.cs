namespace FireSim.Test.TwoD
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FireSim.Neighbour;
    using FireSim.TwoD;
    using Shouldly;
    using Xunit;

    public class CardinalNeighbourStrategyTests
    {
        [Fact]
        public void T01_Finds_All_Neighbours()
        {
            CardinalNeighbourStrategy strategy = new CardinalNeighbourStrategy();
            var grid = new CardinalFireTestGrid();
            var cellState = grid.GetCells(out var currentCell);
            var directions = Enum.GetValues(typeof(CardinalDirection)).Cast<CardinalDirection>();

            NeighbourState state = strategy.GetNeighbours(grid, cellState, currentCell);

            state.States.Length.ShouldBe(8);
            foreach (var direction in directions)
            {
                var item = state.States.Where(x => x.Direction == direction).SingleOrDefault();
                item.State.ShouldBe(CellState.Tree);
            }
        }

        [Fact]
        public void T02_Finds_Only_Cardinal_Neighbours()
        {
            CardinalNeighbourStrategy strategy = new CardinalNeighbourStrategy(false);
            var grid = new CardinalFireTestGrid();
            var cellState = grid.GetCells(out var currentCell);
            var directions = new CardinalDirection[]
            {
                CardinalDirection.North,
                CardinalDirection.South,
                CardinalDirection.East,
                CardinalDirection.West
            };

            NeighbourState state = strategy.GetNeighbours(grid, cellState, currentCell);

            state.States.Length.ShouldBe(4);
            foreach (var direction in directions)
            {
                var item = state.States.Where(x => x.Direction == direction).SingleOrDefault();
                item.State.ShouldBe(CellState.Tree);
            }
        }

        [Fact]
        public void T02_Finds_Specific_Neighbours()
        {
            var directions = new CardinalDirection[]
            {
                CardinalDirection.NorthWest,
                CardinalDirection.SouthWest,
                CardinalDirection.East,
                CardinalDirection.West,
                CardinalDirection.NorthEast,
            };
            CardinalNeighbourStrategy strategy = new CardinalNeighbourStrategy(directions);
            var grid = new CardinalFireTestGrid();
            var cellState = grid.GetCells(out var currentCell);

            NeighbourState state = strategy.GetNeighbours(grid, cellState, currentCell);

            state.States.Length.ShouldBe(directions.Length);
            foreach (var direction in directions)
            {
                var item = state.States.Where(x => x.Direction == direction).SingleOrDefault();
                item.State.ShouldBe(CellState.Tree);
            }
        }

        private class CardinalFireTestGrid : IFireGrid
        {
            public int Height => 7;

            public bool IsCompleted => false;

            public int Width => 7;

            public List<Cell> GetCells(out Cell center)
            {
                center = null;
                int size = Width * Height;
                var cells = new List<Cell>(size);
                int row = 0, col = 0;
                int cRow = Height / 2;
                int cCol = Width / 2;
                for (int i = 0; i < size; i++)
                {
                    bool cent = row == cRow && col == cCol;
                    bool edge = row == 0 || row == Height - 1 || col == 0 || col == Width - 1;
                    var state = edge ? CellState.Empty : cent ? CellState.Burning : CellState.Tree;
                    var cell = new Cell(i, new GridLocation(row, col), state, edge);
                    cells.Add(cell);
                    if (cent)
                    {
                        center = cell;
                    }
                    row++;
                    col++;
                };

                return cells;
            }

            public void Render(IRenderer renderer)
            {
                throw new NotImplementedException();
            }

            public void Spread(ISpreadStrategy newSpreadStrategy = null)
            {
                throw new NotImplementedException();
            }
        }

    }
    
}
