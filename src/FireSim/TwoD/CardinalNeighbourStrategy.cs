namespace FireSim.TwoD
{
    using System.Collections.Generic;
    using System.Linq;
    using FireSim.Neighbour;

    /// <summary>
    /// Provides a neighbour strategy based on <see cref="CardinalDirection"/>s
    /// </summary>
    public sealed class CardinalNeighbourStrategy : INeighbourStrategy
    {
        /// <summary>
        /// Collection of cardinal directions
        /// </summary>
        private static readonly CardinalDirection[] CARDINALS = new CardinalDirection[]
        {
            CardinalDirection.North,
            CardinalDirection.East,
            CardinalDirection.South,
            CardinalDirection.West
        };

        /// <summary>
        /// Collection of sub cardinal directions
        /// </summary>
        private static readonly CardinalDirection[] SUB_CARDINALS = new CardinalDirection[]
        {
            CardinalDirection.NorthEast,
            CardinalDirection.NorthWest,
            CardinalDirection.SouthEast,
            CardinalDirection.SouthWest
        };

        /// <summary>
        /// Provides a single instance of each direction
        /// </summary>
        private readonly HashSet<CardinalDirection> directionsToFindNeighbours;

        /// <summary>
        /// Initialises the object with all cardinal and sub cardinal directions
        /// </summary>
        public CardinalNeighbourStrategy() : this(true)
        {

        }

        /// <summary>
        /// Initialises the object with either cardinals or cardinals and sub cardinals
        /// </summary>
        /// <param name="includeSubCardinals">True to include sub cardinals, false to only include cardinals</param>
        public CardinalNeighbourStrategy(bool includeSubCardinals) 
            : this(includeSubCardinals ? CARDINALS.Union(SUB_CARDINALS).ToArray() : CARDINALS)
        {
            
        }

        /// <summary>
        /// Initialises the object with specific directions
        /// </summary>
        /// <param name="directionsToFindNeighbours"></param>
        public CardinalNeighbourStrategy(params CardinalDirection[] directionsToFindNeighbours)
        {
            this.directionsToFindNeighbours = new HashSet<CardinalDirection>(directionsToFindNeighbours);
        }

        /// <summary>
        /// Gets all neighbours from the cell list based upon the configured <see cref="CardinalDirection"/>s
        /// </summary>
        /// <param name="grid">The <see cref="IFireGrid"/> containing the cells</param>
        /// <param name="lastState">The cells at their last state</param>
        /// <param name="currentCell">The current cell to process neighbours for</param>
        /// <returns><see cref="NeighbourState"/></returns>
        public NeighbourState GetNeighbours(IFireGrid grid, List<Cell> lastState, Cell currentCell)
        {
            var row = currentCell.Location.Row;
            var col = currentCell.Location.Column;

            // Construct coordinates for neighbours
            var cellNeighbours = new List<CellNeighbour>(8);

            bool canGoNorth = row > 0;
            bool canGoSouth = row + 1 < grid.Height;
            bool canGoEast = col > 0;
            bool canGoWest = col + 1 < grid.Width;

            if (canGoNorth) 
            {
                AddNorthToNeighbours(grid, lastState, row, col, cellNeighbours, canGoEast, canGoWest);
            }

            if (canGoWest && HasAnyOf(CardinalDirection.West))
            {
                var westCell = lastState[grid.GetIndex(row, col + 1)];
                cellNeighbours.Add(new CellNeighbour(CardinalDirection.West, westCell.State));
            }

            if (canGoSouth)
            {
                AddSouthToNeighbours(grid, lastState, row, col, cellNeighbours, canGoEast, canGoWest);
            }

            if (canGoEast && HasAnyOf(CardinalDirection.East))
            {
                var eastCell = lastState[grid.GetIndex(row, col - 1)];
                cellNeighbours.Add(new CellNeighbour(CardinalDirection.East, eastCell.State));
            }

            return new NeighbourState(cellNeighbours.ToArray());
        }

        /// <summary>
        /// Gets a value indicating whether the configured cardinals contain any of the passed directions
        /// </summary>
        /// <param name="directions">The <see cref="CardinalDirection"/>s to check</param>
        /// <returns>True or false</returns>
        private bool HasAnyOf(params CardinalDirection[] directions)
        {
            return this.directionsToFindNeighbours.Any(x => directions.Contains(x));
        }

        /// <summary>
        /// Applies the northern directions based on the configured <see cref="CardinalDirection"/>s
        /// </summary>
        /// <param name="grid">The grid to act on</param>
        /// <param name="lastState">The cells in the grid</param>
        /// <param name="row">The row of the current cell</param>
        /// <param name="col">The column of the current cell</param>
        /// <param name="cellNeighbours">The collection of neightbours to populate</param>
        /// <param name="canGoEast">Indicates whether east direction is possible</param>
        /// <param name="canGoWest">Indicates whether west direction is possible</param>
        private void AddNorthToNeighbours(IFireGrid grid, List<Cell> lastState, int row, int col,
            List<CellNeighbour> cellNeighbours, 
            bool canGoEast, bool canGoWest)
        {
            if (HasAnyOf(CardinalDirection.NorthEast, CardinalDirection.North, CardinalDirection.NorthWest))
            {
                if (HasAnyOf(CardinalDirection.North))
                {
                    var northCell = lastState[grid.GetIndex(row - 1, col)];
                    cellNeighbours.Add(new CellNeighbour(CardinalDirection.North, northCell.State));
                }

                if (canGoEast && HasAnyOf(CardinalDirection.NorthEast))
                {
                    var northEastCell = lastState[grid.GetIndex(row - 1, col - 1)];
                    cellNeighbours.Add(new CellNeighbour(CardinalDirection.NorthEast, northEastCell.State));
                }

                if (canGoWest && HasAnyOf(CardinalDirection.NorthWest))
                {
                    var northWestCell = lastState[grid.GetIndex(row - 1, col + 1)];
                    cellNeighbours.Add(new CellNeighbour(CardinalDirection.NorthWest, northWestCell.State));
                }
            }
        }

        /// <summary>
        /// Applies the southern directions based on the configured <see cref="CardinalDirection"/>s
        /// </summary>
        /// <param name="grid">The grid to act on</param>
        /// <param name="lastState">The cells in the grid</param>
        /// <param name="row">The row of the current cell</param>
        /// <param name="col">The column of the current cell</param>
        /// <param name="cellNeighbours">The collection of neightbours to populate</param>
        /// <param name="canGoEast">Indicates whether east direction is possible</param>
        /// <param name="canGoWest">Indicates whether west direction is possible</param>
        private void AddSouthToNeighbours(IFireGrid grid, List<Cell> lastState, int row, int col,
           List<CellNeighbour> cellNeighbours,
           bool canGoEast, bool canGoWest)
        {
            if (HasAnyOf(CardinalDirection.South))
            {
                var southCell = lastState[grid.GetIndex(row + 1, col)];
                cellNeighbours.Add(new CellNeighbour(CardinalDirection.South, southCell.State));
            }

            if (canGoEast && HasAnyOf(CardinalDirection.SouthEast))
            {
                var southEastCell = lastState[grid.GetIndex(row + 1, col - 1)];
                cellNeighbours.Add(new CellNeighbour(CardinalDirection.SouthEast, southEastCell.State));
            }

            if (canGoWest && HasAnyOf(CardinalDirection.SouthWest))
            {
                var southWestCell = lastState[grid.GetIndex(row + 1, col + 1)];
                cellNeighbours.Add(new CellNeighbour(CardinalDirection.SouthWest, southWestCell.State));
            }
        }
    }
}
